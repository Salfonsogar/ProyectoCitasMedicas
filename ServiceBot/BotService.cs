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

namespace ServiceBot
{
    public class BotService
    {
        private readonly TelegramBotClient _botClient;
        private readonly Dictionary<long, EstadoUsuario> _usuario;
        //private readonly PostgresDbRepository _postgressDbRepository;
        //private readonly ServiceCita _serviceCita;  
        //private readonly ServicePaciete _servicePaciete; 
        public BotService(string token)
        {
            _botClient = new TelegramBotClient(token);
            _usuario = new Dictionary<long, EstadoUsuario>();
            //_ServiceCita = new serviceCita();
            //_ServicePaciete = new servicePaciete();
            //_postgressDbRepository = new PostgresDbRepository();
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
                    estadoUsuario.Identificacion = message.Text;
                    estadoUsuario.Estado = "menu_principal";

                    await ConfirmarIdentificacionAsync(chatId);
                    await MostrarMenuPrincipalASync(chatId);
                    return;
                }
                await GestionarMenuPrincipalASync(message.Text, chatId);
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

        public async Task ConfirmarIdentificacionAsync(long chatId)
        {
            var identificacion = _usuario[chatId].Identificacion;
            string nombreUsuario = "Santiago"; 
            await _botClient.SendTextMessageAsync(
                chatId,
                $"✅ ¡Hola {nombreUsuario}! Hemos verificado tu número de identificación: *{identificacion}*.",
                parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown
            );

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
        public async Task MostrarMenuPrincipalSoloCelularASync(long chatId)
        {
            var replyKeyboard = new ReplyKeyboardMarkup(new[]
            {
                new[] { new KeyboardButton("📅 Agendar cita") },
                new[] { new KeyboardButton("📋 Modificar citas") },
                new[] { new KeyboardButton("❌ Cancelar cita") },
                new[] { new KeyboardButton("🔚 Finalizar") }
            })
            {
                ResizeKeyboard = true,
                OneTimeKeyboard = false 
            };

            await _botClient.SendTextMessageAsync(
                chatId,
                "¿Qué deseas hacer a continuación? Por favor, selecciona una opción del menú 👇",
                replyMarkup: replyKeyboard);
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
            var mensajeAgendar = "Por favor, selecciona una especialidad médica de la lista a continuación:";
            await _botClient.SendTextMessageAsync(chatId, mensajeAgendar, parseMode: ParseMode.Markdown);
            //mostrar un teclado con las especialidades
            //buscar fecha proxima disponible y preguntar si la desea
            //si la desea, guardar en la base de datos
            //si no preguntar si desea otra fecha
            //validar si la fecha esta disponible
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
        private async Task PrimerHandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Type != UpdateType.Message || update.Message is null || update.Message.Text is null)
                return;

            var message = update.Message;
            var chatId = message.Chat.Id;
            var texto = LimpiarTexto(message.Text.ToLower());

            if (texto == "/start" || texto == "hola")
            {
                await EnviarMensajeBienvenidaAsync(chatId);

                if (!_usuario.ContainsKey(chatId))
                {
                    _usuario[chatId] = new EstadoUsuario { Estado = "Esperando_Identificacion" };
                }
            }

            if (_usuario.TryGetValue(chatId, out var estadoUsuario))
            {
                if (estadoUsuario.Estado == "Esperando_Identificacion" && message.Text.All(char.IsDigit))
                {
                    estadoUsuario.Identificacion = message.Text;
                    estadoUsuario.Estado = "menu_principal";

                    await ConfirmarIdentificacionAsync(chatId);
                    await MostrarMenuPrincipalASync(chatId);
                    if (update.Type == UpdateType.CallbackQuery)
                    {
                        var callbackQuery = update.CallbackQuery;
                        var callbackData = callbackQuery.Data;
                        if (callbackData == "agendar" || callbackData == "cancelar" || callbackData == "modificar" || callbackData == "salir")
                        {
                            await GestionarMenuPrincipalASync(callbackData, chatId);
                        }
                        else
                        {
                            await EnviarMensajeErrorAsync(chatId);
                        }
                    }
                    return;
                }

            }
            else
            {
                await EnviarMensajeErrorAsync(chatId);
            }
        }
    }
}
