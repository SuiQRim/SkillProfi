using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using SkillProfi;
using SkillProfiRequestsToAPI.Projects;
using SkillProfiTelegramBot;
using Telegram.Bot.Types.InputFiles;
using SkillProfiRequestsToAPI.Images;
using System.Net;
using SkillProfiRequestsToAPI.Blogs;
using SkillProfiRequestsToAPI.Services;
using SkillProfiRequestsToAPI.Contacts;
using SkillProfiRequestsToAPI.Consultations;

namespace TelegramBotExperiments
{

    class Program
    {
        private readonly static List<ClientState> _clientStates = new();

        static ITelegramBotClient bot = new TelegramBotClient("5886273004:AAEn3t4X5jNlm8ztLN70D1JbDd0iRWwXECQ");

        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            // Некоторые действия
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update));
            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {
                var message = update.Message;

                ClientState? cs = _clientStates.FirstOrDefault(c => c.Chat.Id == message.Chat.Id);

                if (cs != null)
                {
                    switch (cs.State)
                    {
                        case ClientStatusesEnum.WriteConsulEmail:
                            cs.SetConsultation(email: message.Text);
                            await botClient.SendTextMessageAsync(message.Chat, "Введите описание");
                            cs.State = ClientStatusesEnum.WriteConsulDescription;
                            break;

                        case ClientStatusesEnum.WriteConsulDescription:

                            cs.SetConsultation(description: message.Text, name: message.Chat.FirstName);
                            await botClient.SendTextMessageAsync(message.Chat,
                                "Подтвердите отправку\n\n" +
                                $"Отправитель\n{cs.Consultation.Name}\n\n" +
                                $"Почта\n{cs.Consultation.EMail}\n\n" +
                                $"Описание\n{cs.Consultation.Description}");
                            await botClient.SendTextMessageAsync(message.Chat,
                                $"Да - Подтвердить\nНет - Отменить");
                            cs.State = ClientStatusesEnum.ConfirmConultationSending;
                            break;

                        case ClientStatusesEnum.ConfirmConultationSending:
                            switch (message.Text.ToLower())
                            {
                                case "да":
                                    await ConsultationsRequests.AddConsultationAsync(cs.Consultation);
                                    await botClient.SendTextMessageAsync(message.Chat, "Заявка отправлена");
                                    _clientStates.Remove(cs);
                                    break;

                                case "нет":
                                    await botClient.SendTextMessageAsync(message.Chat, "Заявка сброшена");
                                    _clientStates.Remove(cs);
                                    break;
                                default:
                                    await botClient.SendTextMessageAsync(message.Chat, "Закончите действие с консультацией");
                                    break;
                            }
                            break;
                    }

                }

                else
                {
                    string lowText = message.Text.ToLower();

                    switch (lowText) {

                        case "/start":
                            await botClient.SendTextMessageAsync(message.Chat, "Добро пожаловать на борт, добрый путник!");
                            break;


                        case "/consultation":

                            await botClient.SendTextMessageAsync(message.Chat, "Оставьте заявку на консультацию");
                            await botClient.SendTextMessageAsync(message.Chat, "Введине Email ");
                            _clientStates.Add(new(message.Chat, ClientStatusesEnum.WriteConsulEmail));
                            break;


                        case "/projects":

                            List<Project> proj = (await ProjectsRequests.GetProjectsAsync()).LoadImage();
                            foreach (var p in proj)
                                await SendImage(botClient, message.Chat, p, p.Title + $"\n\n{p.Description}");
                            break;


                        case "/blogs":
                            List<Blog> blogs = (await BlogsRequests.GetBlogsAsync()).LoadImage();
                            foreach (var p in blogs)
                                await SendImage(botClient, message.Chat, p, p.Title + $"\n\n{p.Description}");
                            break;


                        case "/services":

                            List<Service> services = await ServicesRequests.GetServicesAsync();
                            foreach (var s in services)
                                await botClient.SendTextMessageAsync(message.Chat, s.Title + $"\n\n{s.Description}");
                            break;


                        case "/contacts":

                            Contacts contacts = ContactsRequests.GetContacts();
                            string socialNetworsText = "Мы в социальных сетях:\n\n";

                            foreach (var sn in contacts.SocialNetworks)
                                socialNetworsText += $"   {sn.Link}";

                            await botClient.SendTextMessageAsync(message.Chat,
                                $"Контакты\n\n" +
                                $"   Тел.    {contacts.PhoneNumber}\n\n" +
                                $"   Почта.  {contacts.Email}\n\n" +
                                $"   Адресс  {contacts.Adress}\n\n" +
                                $"\n" +
                                $"{socialNetworsText}");
                            break;


                        default: 
                            await botClient.SendTextMessageAsync(message.Chat, "Непредвиденная команда");
                            break;

                    }
                }

            }
        }

        private static async Task SendImage<T>(ITelegramBotClient botClient, ChatId chatId, T item, string? caption = null)
            where T : IPicture
        {
            try
            {
                using var httpClient = new HttpClient();
                using var stream = await httpClient.GetStreamAsync(ImagesRequests.GetURL(item.PictureName));
                InputOnlineFile iof = new(stream);
                await botClient.SendPhotoAsync(chatId, iof, caption);

            }
            catch (HttpRequestException e) when (e.StatusCode == HttpStatusCode.NotFound)
            {
                using var stream = new FileStream("./Assets/ImageError.png", FileMode.Open);
                InputOnlineFile iof = new(stream);
                await botClient.SendPhotoAsync(chatId, iof, caption);
            }
        }

        public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            // Некоторые действия
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
        }


        static void Main(string[] args)
        {
            Console.WriteLine("Запущен бот " + bot.GetMeAsync().Result.FirstName);

            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { }, // receive all update types
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