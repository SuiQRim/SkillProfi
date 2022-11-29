using Newtonsoft.Json.Linq;
using SkillProfi;

namespace SkillProfiRequestsToAPI.Accounts
{
    public class AccountsRequests : RequestController
    {
        public AccountsRequests(Func<string> getBaseUrl) : base(getBaseUrl, "Auth") { }

        public AuthParameters? Login(Account account)
        {
            string data = Request.Add(account, Url);
            JObject jsonResponce = JObject.Parse(data);

            int statusCode = jsonResponce.Value<int>("statusCode");

            if (statusCode != 0) return new AuthParameters { IsLogin = false};

            return jsonResponce["data"].ToObject<AuthParameters>();
        }

    }
}
