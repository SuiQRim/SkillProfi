using SkillProfi;
using SkillProfiTelegramBot;
using Telegram.Bot.Types;

namespace TelegramBotExperiments
{
    internal class ClientState
    {
        public ClientState(Chat chat, ClientStatusesEnum state)
        {
            Chat = chat;
            State = state;
            Consultation = new ();
            LastRequest = DateTime.UtcNow;
        }

        public Chat Chat { get; init; }

        private ClientStatusesEnum _state;
        public ClientStatusesEnum State
        {
            get => _state;
            set
            {
                LastRequest = DateTime.UtcNow;
                _state = value;
            }
        }

        public Consultation Consultation { get; private set; }

        public DateTime LastRequest { get; private set; }
        

        public void SetConsultation(string? email = null, string? description = null, string? name = null)
        {
            Consultation.EMail = email ?? Consultation.EMail;
            Consultation.Name = name ?? Consultation.Name;
            Consultation.Description = description ?? Consultation.Description;
            LastRequest = DateTime.UtcNow;
        }
    }
}