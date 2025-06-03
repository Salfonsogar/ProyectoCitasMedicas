using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Service;
using Entity;
using DAL;
using System.Globalization;

namespace ServiceBot
{
    public class BotService
    {
        private readonly TelegramBotClient _botClient;
        private readonly Dictionary<long, EstadoUsuario> _usuario;
        private readonly PacienteService _pacienteService;
        private readonly MedicoService _medicoService;
        private readonly EspecialidadesService _especialidadesService;
        private readonly CitaMedicaService _citaMedicaService;

        public BotService(string token)
        {
            _botClient = new TelegramBotClient(token);
            _usuario = new Dictionary<long, EstadoUsuario>();
            _pacienteService = new PacienteService(new PacienteRepository());
            _medicoService = new MedicoService(new MedicoRepository());
            _especialidadesService = new EspecialidadesService(new EspecialidadesRepository());
            _citaMedicaService = new CitaMedicaService(new CitaMedicaRepository());
        }
        public async Task StartBotAsync()
        {
            var me = await _botClient.GetMeAsync();
            Console.WriteLine($"Bot id: {me.Id}, Name: {me.FirstName}");

            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = Array.Empty<UpdateType>()
            };

            _botClient.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                receiverOptions
            );
        }
        private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            try
            {
                switch (update.Type)
                {
                    case UpdateType.Message:
                        if (update.Message?.Text != null)
                            await ManejarMensajeAsync(update.Message);
                        break;

                    case UpdateType.CallbackQuery:
                        if (update.CallbackQuery != null)
                            await ManejarCallbackQueryAsync(update.CallbackQuery);
                        break;

                    default:
                        Console.WriteLine($"Tipo de update no manejado: {update.Type}");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en HandleUpdateAsync: {ex.Message}");
            }
        }

        private async Task ManejarMensajeAsync(Message message)
        {
            var chatId = message.Chat.Id;
            var texto = LimpiarTexto(message.Text.ToLower());

            if (texto == "/start" || texto == "hola")
            {
                await EnviarMensajeBienvenidaAsync(chatId);

                if (!_usuario.ContainsKey(chatId))
                    _usuario[chatId] = new EstadoUsuario { Estado = "Esperando_Identificacion" };

                return;
            }
            if (_usuario.TryGetValue(chatId, out var estadoUsuario))
            {
                if (estadoUsuario.Estado == "Esperando_Identificacion" && message.Text.All(char.IsDigit))
                {
                    bool esValida = await ConfirmarIdentificacionAsync(chatId, message.Text);
                    if (esValida)
                    {
                        estadoUsuario.Identificacion = message.Text;
                        estadoUsuario.Estado = "menu_principal";
                        await MostrarMenuPrincipalASync(chatId);
                    }
                    return;
                }
                else if (estadoUsuario.Estado == "menu_principal")
                {
                    await MostrarMenuPrincipalASync(chatId);
                    return;
                }
                await _botClient.SendTextMessageAsync(chatId, "⚠️ Por favor, ingresa primero tu número de identificación.");
            }
            else if (texto =="cancelar")
            {
                await CancelarConversacionAsync(chatId);
            }
            else
            {
                await EnviarMensajeErrorAsync(chatId);
            }
        }
        private async Task ManejarCallbackQueryAsync(CallbackQuery callbackQuery)
        {
            var chatId = callbackQuery.Message.Chat.Id;
            var data = callbackQuery.Data;
            var estado = _usuario.ContainsKey(chatId) ? _usuario[chatId].Estado : "";

            if (data.StartsWith("opcion_")) 
            {
                await GestionarMenuPrincipalASync(data, chatId);
                return;
            }

            if (data.StartsWith("especialidad_") && _especialidadesService.EsUnaEspecialidad(int.Parse(data.Split('_')[1])))
            {
                int idEspecialidad = int.Parse(data.Split('_')[1]);
                _usuario[chatId].Estado = "eligiendo_especialidad";
                await MostrarDisponibilidadAsync(chatId, idEspecialidad);
                return;
            }

            if (data.StartsWith("fecha_") && estado is "eligiendo_fecha_cita" || estado is "modificar_cita_fecha")
            {
                var partes = data.Split('_');
                var fechaTexto = partes[1];
                var idMedico = int.Parse(partes[2]);
                var fecha = DateTime.ParseExact(fechaTexto, "yyyyMMddHHmm", CultureInfo.InvariantCulture);

                _usuario[chatId].FechaSeleccionada = fecha;
                _usuario[chatId].IdMedico = idMedico.ToString();

                if (estado == "eligiendo_fecha_cita")
                {
                    await AgendarCitaAsync(chatId);
                }
                else
                {
                    await ConfirmarNuevaFechaModificacion(chatId);
                }
                return;
            }
            if(estado == "eligiendo_cita_para_cancelar")
            {
                int idCita = int.Parse(data.Split('_')[1]);
                var cita = _citaMedicaService.ObtenerCitaPorId(idCita);
                if (cita != null)
                {
                    await MostrarDatosCita(chatId, cita);
                    //await CancelarCitaAsync(chatId, cita);
                }
                return;
            }
            if (data == "confirmar")
            {
                await ConfirmarCita(chatId);
                return;
            }

            if (data == "cancelar")
            {
                await CancelarConversacionAsync(chatId);
                return;
            }

            if (data.StartsWith("cita_"))
            {
                int idCita = int.Parse(data.Split('_')[1]);
                var cita = _citaMedicaService.ObtenerCitaPorId(idCita);
                if (cita != null)
                {
                    _usuario[chatId].Estado = "modificar_cita_fecha";
                    _usuario[chatId].IdCita = idCita.ToString();
                    _usuario[chatId].FechaSeleccionada = cita.Fecha;
                    _usuario[chatId].IdMedico = cita.medico.IdMedico.ToString();

                    await MostrarDatosCita(chatId, cita);
                    await ModificarCitaAsync(chatId, cita.medico.IdEspecialidad);
                }
                return;
            }

            await EnviarMensajeErrorAsync(chatId);
            await _botClient.AnswerCallbackQueryAsync(callbackQuery.Id);
        }
        private async Task ConfirmarNuevaFechaModificacion(long chatId)
        {
            await _botClient.SendTextMessageAsync(chatId,
                $"🌟Has seleccionado la nueva fecha {_usuario[chatId].FechaSeleccionada:dd/MM/yyyy HH:mm} para tu cita médica.🌟\n");

            await _botClient.SendTextMessageAsync(chatId, "¿Qué deseas hacer a continuación?", replyMarkup: new InlineKeyboardMarkup(new[]
            {
                new[] { InlineKeyboardButton.WithCallbackData("✅ Confirmar", "confirmar") },
                new[] { InlineKeyboardButton.WithCallbackData("❌ Cancelar", "cancelar") }
            }));
            _usuario[chatId].Estado = "confirmar_modificar";
        }
        private async Task ConfirmarCita(long chatId)
        {
            var usuario = _usuario[chatId];
            var idMedico = int.Parse(usuario.IdMedico);
            var idPaciente = _pacienteService.ObtenerIdPaciente(int.Parse(usuario.Identificacion));

            var citaMedica = new CitaMedica
            {
                Id = string.IsNullOrEmpty(usuario.IdCita) ? 0 : int.Parse(usuario.IdCita),
                medico = new Medico { IdMedico = idMedico },
                paciente = new Paciente { IdPaciente = idPaciente },
                Fecha = usuario.FechaSeleccionada,
                Estado = "Pendiente"
            };

            if (citaMedica.Id > 0)
            {
                await _citaMedicaService.Modificar(citaMedica);
            }
            else
            {
                await _citaMedicaService.Agregar(citaMedica);
            }

            await MostrarDatosCita(chatId, citaMedica);

            usuario.Estado = "menu_principal";
            usuario.IdCita = null;
            usuario.FechaSeleccionada = default;
            usuario.IdMedico = null;
        }
        private async Task MostrarDatosCita(long chatId, CitaMedica cita)
        {
            await _botClient.SendTextMessageAsync(chatId,
                $"📋 Detalles de la Cita:\n" +
                $"🗓️ Fecha: {cita.Fecha:dd/MM/yyyy HH:mm}\n" +
                $"👨‍⚕️ Médico: {cita.medico.NombreCompleto}\n" +
                $"👤 Paciente: {cita.paciente.NombreCompleto}");
        }
        private Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Ocurrió un error en el bot: {exception.Message}");
            return Task.CompletedTask;
        }

        public async Task EnviarMensajeBienvenidaAsync(long chatId)

        {
            var mensajeBienvenida = "🌟 ¡Bienvenid@ a MedApp! 🌟\n" +
                                    "👩‍⚕️ *Mi nombre es Ana y seré tu asistente virtual.*\n" +
                                    "A través de este canal seguiremos cuidando tu salud y la de tu familia. 🏥❤️\n\n" +
                                    "📌 Para comenzar, por favor indícame tu **número de identificación**.\n" +
                                    "🧾 *Ejemplo: 8743293384*";

            await _botClient.SendTextMessageAsync(chatId, mensajeBienvenida, parseMode: ParseMode.Markdown);
        }
        public async Task EnviarMensajeErrorAsync(long chatId)
        {
            var mensajeError = "Lo siento, no entiendo ese comando. Por favor, envíame tu número de identificación.";
            await _botClient.SendTextMessageAsync(chatId, mensajeError, parseMode: ParseMode.Markdown);
        }

        public async Task CancelarConversacionAsync(long chatId)
        {
            var mensajeCancelacion = "La conversación ha sido cancelada. Si necesitas ayuda, no dudes en escribirme nuevamente.";
            await _botClient.SendTextMessageAsync(chatId, mensajeCancelacion, parseMode: ParseMode.Markdown);
            _usuario.Remove(chatId);
        }

        public async Task<bool> ConfirmarIdentificacionAsync(long chatId, string identificacion)
        {
            string nombreUsuario = _pacienteService.ConsultarNombre(int.Parse(identificacion));

            if (string.IsNullOrEmpty(nombreUsuario))
            {
                await _botClient.SendTextMessageAsync(
                    chatId,
                    $"❌ No encontramos ningún usuario con la identificación *{identificacion}*. Por favor, intenta de nuevo.",
                    parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown
                );
                return false;
            }

            await _botClient.SendTextMessageAsync(
                chatId,
                $"✅ ¡Hola {nombreUsuario}! Hemos verificado tu número de identificación: *{identificacion}*.",
                parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown
            );

            return true;
        }

        private async Task GestionarMenuPrincipalASync(string data, long chatId)
        {
            if (!_usuario.ContainsKey(chatId)) return;

            switch (data)
            {
                case "opcion_agendar":
                    _usuario[chatId].Estado = "eligiendo_especialidad";
                    await GestionAgendarCitaAsync(chatId);
                    break;

                case "opcion_modificar":
                    _usuario[chatId].Estado = "eligiendo_cita_para_modificar";
                    await GestionModificarCitaAsync(chatId);
                    break;

                case "opcion_cancelar":
                    _usuario[chatId].Estado = "eligiendo_cita_para_cancelar";
                    await GestionCancelarCitaAsync(chatId);
                    break;

                case "opcion_salir":
                    _usuario.Remove(chatId);
                    await _botClient.SendTextMessageAsync(chatId, "👋 Has salido del sistema.");
                    break;

                default:
                    await _botClient.SendTextMessageAsync(chatId, "❓ Opción no reconocida.");
                    break;
            }
        }


        public async Task MostrarMenuPrincipalASync(long chatId)
        {
            var inlineKeyboard = new InlineKeyboardMarkup(new[]
            {
                new[] { InlineKeyboardButton.WithCallbackData("📅 Agendar cita", "opcion_agendar") },
                new[] { InlineKeyboardButton.WithCallbackData("📋 Modificar citas", "opcion_modificar") },
                new[] { InlineKeyboardButton.WithCallbackData("❌ Cancelar cita", "opcion_cancelar") },
                new[] { InlineKeyboardButton.WithCallbackData("🔚 Finalizar", "opcion_salir") }
            });

            _usuario[chatId].Estado = "menu_principal_opcion";

            await _botClient.SendTextMessageAsync(
                chatId: chatId,
                text: "¿Qué deseas hacer a continuación? Por favor, selecciona una opción 👇",
                replyMarkup: inlineKeyboard
            );
        }

        public static string LimpiarTexto(string texto)
        {
            var textolimpio = new string(texto
                .Where(c => char.IsLetterOrDigit(c) || char.IsWhiteSpace(c))
                .ToArray());
            return textolimpio.Trim().ToLower();
        }
        public async Task EnviarMensajeConfirmacionAsync(long chatId)
        {
            var mensajeConfirmacion = "✅ Tu cita ha sido confirmada. ¡Nos vemos pronto!";
            await _botClient.SendTextMessageAsync(chatId, mensajeConfirmacion, parseMode: ParseMode.Markdown);
        }
        public async Task GestionAgendarCitaAsync(long chatId)
        {
            await _botClient.SendTextMessageAsync(chatId, "🌟 ¡Bienvenid@ a la sección agendar cita!🌟\n" +
                "¡Aquí podrás ver nuestras especialidad disponibles!");
            await MostrarMenuEspecialidadesAsync(chatId);
        }
        public async Task MostrarMenuEspecialidadesAsync(long chatId)
        {
            var especialidades = _especialidadesService.Consultar();

            if (especialidades == null || !especialidades.Any())
            {
                await _botClient.SendTextMessageAsync(chatId, "⚠️ No se encontraron especialidades disponibles.");
                return;
            }

            var botones = especialidades
                .Select(e => InlineKeyboardButton.WithCallbackData($"🩺 {e.NombreCompleto}", $"especialidad_{e.Id}"))
                .ToArray();

            var inlineKeyboard = new InlineKeyboardMarkup(botones.Select(b => new[] { b }));
            _usuario[chatId].Estado = "eligiendo_especialidad";
            try
            {
                await _botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: "¿Qué especialidad médica deseas agendar? Por favor, selecciona una opción 👇",
                    replyMarkup: inlineKeyboard
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al enviar mensaje: {ex.Message}");
            }
        }

        public async Task MostrarDisponibilidadAsync(long chatId, int idEspecialidad)
        {
            var nombre = _especialidadesService.ObtenerNombre(idEspecialidad);
            await _botClient.SendTextMessageAsync(chatId, $"🌟Perfecto Has seleccionado la especialidad {nombre}.🌟");

            var fechasDisponibles = _citaMedicaService.ObtenerFechasDisponibles(idEspecialidad);

            if (fechasDisponibles.Count == 0)
            {
                await _botClient.SendTextMessageAsync(chatId, "⚠️Lo siento, no hay fechas disponibles para esta especialidad.");
                return;
            }
            _usuario[chatId].Estado = "eligiendo_fecha_cita";

            await CallBackFechas(chatId, fechasDisponibles);
        }
        public async Task CallBackFechas(long chatId,List<FechaDisponibleDto> fechaDisponibleDto)
        {
            var botones = fechaDisponibleDto
                .Select(f => new[]
                {
            InlineKeyboardButton.WithCallbackData(
                $"🗓️ {f.Fecha:dd/MM/yyyy HH:mm}",
                $"fecha_{f.Fecha:yyyyMMddHHmm}_{f.IdMedico}")
                })
                .ToList();

            var inlineKeyboard = new InlineKeyboardMarkup(botones);

            await _botClient.SendTextMessageAsync(
                chatId: chatId,
                text: "¿Qué fecha deseas? Por favor, selecciona una opción 👇",
                replyMarkup: inlineKeyboard
            );
        }
        public async Task AgendarCitaAsync(long chatId)
        {
            var id = _pacienteService.ObtenerIdPaciente(int.Parse(_usuario[chatId].Identificacion));
            await _botClient.SendTextMessageAsync(chatId, $"🌟Has seleccionado la fecha {_usuario[chatId].FechaSeleccionada} para tu cita médica.🌟\n");
            var inlineKeyboard = new InlineKeyboardMarkup(new[]
            {
                new[] { InlineKeyboardButton.WithCallbackData("✅ Confirmar", "confirmar") },
                new[] { InlineKeyboardButton.WithCallbackData("❌ Cancelar", "cancelar") }
            });
            await _botClient.SendTextMessageAsync(
                chatId: chatId,
                text: "¿Qué deseas hacer a continuación? Por favor, selecciona una opción 👇",
                replyMarkup: inlineKeyboard
            );
            _usuario[chatId].Estado = "confirmar_cita";
        }
        public async Task GestionModificarCitaAsync(long chatId)
        {
            var mensajeModificar = "Por favor, selecciona una cita agendada de la lista a continuación:";
            await _botClient.SendTextMessageAsync(chatId, mensajeModificar, parseMode: ParseMode.Markdown);

            var idPaciente = _pacienteService.ObtenerIdPaciente(int.Parse(_usuario[chatId].Identificacion));
            var citasAgendadas = _citaMedicaService.ObtenerCitasPorPaciente(idPaciente);

            if (citasAgendadas == null || !citasAgendadas.Any())
            {
                await _botClient.SendTextMessageAsync(chatId, "No tienes citas agendadas.");
                return;
            }

            var botones = citasAgendadas
                .Select(c => new[] { InlineKeyboardButton.WithCallbackData($"🗓️ {c.Fecha:dd/MM/yyyy HH:mm}", $"cita_{c.Id}") })
                .ToArray();

            var inlineKeyboard = new InlineKeyboardMarkup(botones);

            await _botClient.SendTextMessageAsync(
                chatId: chatId,
                text: "¿Qué cita deseas modificar? Por favor, selecciona una opción 👇",
                replyMarkup: inlineKeyboard
            );

            _usuario[chatId].Estado = "modificar_cita";
        }

        public async Task ModificarCitaAsync(long chatId,int idEspecialidad)
        {
            var fechasDisponibles = _citaMedicaService.ObtenerFechasDisponibles(idEspecialidad);
            await CallBackFechas(chatId, fechasDisponibles);
        }
        public async Task GestionCancelarCitaAsync(long chatId)
        {
            var mensajeCancelar = "Por favor, selecciona una cita agendada de la lista a continuación:";
            await _botClient.SendTextMessageAsync(chatId, mensajeCancelar, parseMode: ParseMode.Markdown);
            //mostrar un teclado con las citas agendadas
            //preguntar si desea cancelar la cita
            //si desea cancelar, eliminar de la base de datos
            //si no desea cancelar, preguntar si desea modificar la cita
            //si desea modificar, preguntar por la nueva fecha
            //validar si la fecha esta disponible
            //si no esta disponible preguntar si desea otra fecha
            //si no desea otra fecha, cancelar la cita
        }
    }
}