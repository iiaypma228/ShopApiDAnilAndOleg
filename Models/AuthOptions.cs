using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Server.API.Models;

public class AuthOptions
{
    public const string ISSUER = "Retail.Server.API"; // издатель токена
    public const string AUDIENCE = "Retail.Client"; // потребитель токена
    const string KEY = "Retail.Server.API.OlegAndDanilProjectKurs";   // ключ для шифрации
    public const int LIFETIME = 60*24; // время жизни токена в минутах

    public static SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
    }
}