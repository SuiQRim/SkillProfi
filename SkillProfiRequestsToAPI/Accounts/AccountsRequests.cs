using Newtonsoft.Json.Linq;
using SkillProfi;

namespace SkillProfiRequestsToAPI.Accounts
{
    public class AccountsRequests : RequestController
    {
        public AccountsRequests(Func<string> getBaseUrl) : base(getBaseUrl, "Auth") { }

        public async Task<string> LoginAsycnc(Account account) =>
            await Request.AddAsync(account, Url);
        

    }
}
