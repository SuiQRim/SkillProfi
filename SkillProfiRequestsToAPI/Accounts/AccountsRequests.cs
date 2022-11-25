using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SkillProfi;
using static System.Net.Mime.MediaTypeNames;

namespace SkillProfiRequestsToAPI.Accounts
{
    public static class AccountsRequests
    {
        private const string _mainUrl = "https://localhost:7120/api/Auth";
        public static bool Login(Account account)
        {
            string data = Request.Add(account, _mainUrl);
            JObject jsonResponce = JObject.Parse(data);

            int statusCode = jsonResponce.Value<int>("statusCode");

            if (statusCode != 0) return false;
            
            return true;
        }

    }
}
