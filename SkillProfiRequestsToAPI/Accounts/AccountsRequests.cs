using Newtonsoft.Json.Linq;
using SkillProfi;

namespace SkillProfiRequestsToAPI.Accounts
{
    public class AccountsRequests : RequestController
    {
        public AccountsRequests(Func<string> getBaseUrl) : base(getBaseUrl, "Auth") { }

        public string? Login(Account account)
        {
            return Request.Add(account, Url);
        }

    }
}
