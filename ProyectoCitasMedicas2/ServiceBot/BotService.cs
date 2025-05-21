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

namespace ServiceBot
{
    public class BotService
    {
        private readonly TelegramBotClient _botClient;
        private readonly Dictionary<long, EstadoUsuario> _usuario;
        private readonly ServicePaciente _servicePaciente;
        public BotService(string token)
        {
            _botClient = new TelegramBotClient(token);
            _usuario = new Dictionary<long, EstadoUsuario>();
            _servicePaciente = new ServicePaciente(new PacienteRepository());
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
                    await GestionarMenuPrincipalASync(message.Text, chatId);
                    return;
                }
                await _botClient.SendTextMessageAsync(chatId, "⚠️ Por favor, ingresa primero tu número de identificación.");
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

            if (data == "agendar" || data == "cancelar" || data == "modificar" || data == "salir")
            {
                await GestionarMenuPrincipalASync(data, chatId);
            }
            else if (data == "medicina_general" || data == "pediatria" || data == "ginecologia" || data == "odontologia" || data == "psicologia")
            {
                await MostrarDisponibilidadAsync(chatId, data);
            }
            else
            {
                await EnviarMensajeErrorAsync(chatId);
            }
            await _botClient.AnswerCallbackQueryAsync(callbackQuery.Id);
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
            string nombreUsuario = _servicePaciente.ConsultarNombre(int.Parse(identificacion));

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

        public async Task GestionarMenuPrincipalASync(string textolimpio, long chatId)
        {
            switch (textolimpio)
            {
                case "agendar":
                    await GestionAgendarCitaAsync(chatId);
                    break;
                case "cancelar":
                    await GestionCancelarCitaAsync(chatId);
                    break;
                case "modificar":
                    await GestionModificarCitaAsync(chatId);
                    break;
                case "salir":
                    await CancelarConversacionAsync(chatId);
                    break;
                default:
                    await EnviarMensajeErrorAsync(chatId);
                    break;
            }
        }

        public async Task MostrarMenuPrincipalASync(long chatId)
        {
            var inlineKeyboard = new InlineKeyboardMarkup(new[]
            {
                new[] { InlineKeyboardButton.WithCallbackData("📅 Agendar cita", "agendar") },
                new[] { InlineKeyboardButton.WithCallbackData("📋 Modificar citas", "modificar") },
                new[] { InlineKeyboardButton.WithCallbackData("❌ Cancelar cita", "cancelar") },
                new[] { InlineKeyboardButton.WithCallbackData("🔚 Finalizar", "salir") }
            });
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
            await _botClient.SendTextMessageAsync(chatId, "🌟 ¡Bienvenid@ a la sección agendar cita! 🌟");
            await MostrarMenuEspecialidadesAsync(chatId);
        }
        public async Task MostrarMenuEspecialidadesAsync(long chatId)
        {
            //llamar a EspecialidadService para obtener la lista de especialidades
            var inlineKeyboard = new InlineKeyboardMarkup(new[]
            {
                new[] { InlineKeyboardButton.WithCallbackData("👩‍⚕️ Medicina General", "medicina_general") },
                new[] { InlineKeyboardButton.WithCallbackData("👨‍⚕️ Pediatría", "pediatria") },
                new[] { InlineKeyboardButton.WithCallbackData("👩‍⚕️ Ginecología", "ginecologia") },
                new[] { InlineKeyboardButton.WithCallbackData("👨‍⚕️ Odontología", "odontologia") },
                new[] { InlineKeyboardButton.WithCallbackData("👩‍⚕️ Psicología", "psicologia") }
            });
            await _botClient.SendTextMessageAsync(
                chatId: chatId,
                text: "¿Qué especialidad médica deseas agendar? Por favor, selecciona una opción 👇",
                replyMarkup: inlineKeyboard
            );
        }
        public async Task MostrarDisponibilidadAsync(long chatId, string especialidad)
        {
            await _botClient.SendTextMessageAsync(chatId, $"Has seleccionado la especialidad: {especialidad}.");
            //llamar a DisponibilidadService para obtener la disponibilidad segun la especialidad
        }
        public async Task GestionModificarCitaAsync(long chatId)
        {
            var mensajeModificar = "Por favor, selecciona una cita agendada de la lista a continuación:";
            await _botClient.SendTextMessageAsync(chatId, mensajeModificar, parseMode: ParseMode.Markdown);
            //mostrar un teclado con las citas agendadas
            //preguntar si desea modificar la cita
            //si desea modificar, preguntar por la nueva fecha
            //validar si la fecha esta disponible
            //si no esta disponible preguntar si desea otra fecha
            //si no desea otra fecha, cancelar la cita
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