using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Server.API.Models;
using Server.API.Models.Dtos;
using Server.API.Services.Interfaces;

namespace Server.API.Controllers;

[ApiController]
[Route("")]
public class HomeController : Controller
{
    private readonly IServiceContainer _container = null;

    private readonly IEmployeeService service = null;
    
    public HomeController(IServiceContainer container)
    {
        this._container = container;
        service = container.Resolve<IEmployeeService>();
    }
    
    
    [HttpGet]
    [AllowAnonymous]
    [Route("api/ping")]
    public IActionResult Ping()
    {
        System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
        System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);
        var apiAssembly = new AssemblyDto
        {
            ProductName = fvi.ProductName,
            VersionDatabase = fvi.ProductVersion,//bd version(расчетная версия БД)
            VersionAPI = fvi.FileVersion,//API(версия АПИ)
            CompanyName = fvi.CompanyName,
            Comments = fvi.Comments
        };

        return Ok(apiAssembly);
    }

    [HttpGet]
    [AllowAnonymous]
    [Route("api/auth")]
    public IActionResult Auth(string email, string password)
    {

        var user = this.service.AuthUser(email, password);

        if (user == null)
        {
            return BadRequest("Користувача з такмим данними не знайдено!");
        }

        user.Password = string.Empty;
        
        var now = DateTime.UtcNow;
        // создаем JWT-токен
        var jwt = new JwtSecurityToken(
            issuer: AuthOptions.ISSUER,
            audience: AuthOptions.AUDIENCE,
            notBefore: now,
            claims: new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email)
            },
            expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
        );
        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

      
        var result = new AuthorizationDto()
        {
            Email = email,
            Employee = user,
            Token = encodedJwt,
            TokenLifeTime = AuthOptions.LIFETIME,
            Shop = this._container.Resolve<IShopService>().Read(user.ShopId),
        };
        
        return Ok(result);
    }
    
    
}