using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace SkillProfiApi
{
    public class AuthOptions
    {
        public const string ISSUER = "SkillProfiServer"; // издатель токена
        public const string AUDIENCE = "SkillProfiClient"; // потребитель токена
        const string KEY = "mysupersecret_secretkey!123";   // ключ для шифрации
        public const int LIFETIME = 15; // время жизни токена 
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
