using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Polling;
using TelegramBot;
using Telegram.Bot.Types.ReplyMarkups;

namespace MyFirstBot
{

    class Program
    {
        static ITelegramBotClient bot = new TelegramBotClient("6072998643:AAGGOiAB5P8XtMCbLY0-FCE-2Y0de9BzlaA");
               public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {
                var message = update.Message;


                if (message.Text.ToLower() == "/старт")
                {
                    var inlineKeyboard = new InlineKeyboardMarkup(new[]
                    {
                     new []{InlineKeyboardButton.WithCallbackData("🏟 Последний матч Спартака 🏟", "/getdata")},
                     new []{InlineKeyboardButton.WithCallbackData("⌚ Дата и время ⌚", "/date")},
                     new []{InlineKeyboardButton.WithCallbackData("🌤 Температура в Баре 🌤", "/watertemperature") },
                     
                });

                    await botClient.SendTextMessageAsync(message.Chat, "Бот активирован", replyMarkup: inlineKeyboard);
                }
                else
                {
                    await botClient.SendTextMessageAsync(message.Chat, "Нет такой команды.");
                }
            }
            else if (update.Type == Telegram.Bot.Types.Enums.UpdateType.CallbackQuery)
            {
                var callbackQuery = update.CallbackQuery;

                if (callbackQuery.Data == "/date")
                {
                    DateTime currentTime = DateTime.Now;
                    System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("ru-RU");
                    string currentTimeString = currentTime.ToString("dd'-'MMMM'-'yyyy', ' HH:mm");

                    DateTime mneTime = currentTime.AddHours(-1);
                    

                    await botClient.SendTextMessageAsync(callbackQuery.Message.Chat, "Дата и время московское: " + currentTimeString + Environment.NewLine + "Черногорское время: " + mneTime.ToString("HH:mm"));
                }
                else if (callbackQuery.Data == "/getdata")
                {
                    ParsingSiteSpartak getData = new ParsingSiteSpartak();
                    await botClient.SendTextMessageAsync(callbackQuery.Message.Chat, getData.Result);
                }
                else if (callbackQuery.Data == "/watertemperature")
                {
                    ParsingSiteWeather getData = new ParsingSiteWeather();
                    await botClient.SendTextMessageAsync(callbackQuery.Message.Chat, getData.Result);
                }
                
                return;
            }
        }

        public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            await Task.Delay(1000);
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
        }
        static void Main()
        {
            Console.WriteLine("Запущен бот " + bot.GetMeAsync().Result.FirstName);
            bot.DeleteWebhookAsync().Wait();

            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { }, 
            };
            bot.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                receiverOptions,
                cancellationToken
            );
            
            Console.ReadLine();
        }
    }
}