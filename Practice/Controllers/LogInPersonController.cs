using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Practice.Models;
using Practice.Models.DTO;
using Practice.Services.DBService;
using Practice.Services.HashService;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Practice.Controllers
{
    public class LogInPersonController : Controller
    {
        private IDBService dBService;
        private IHashService hashService;

        public LogInPersonController(IDBService dBService, IHashService hashService) 
        {
            this.dBService = dBService;
            this.hashService = hashService;
        }

        public IActionResult LogIn()
        {
            return View();
        }

        [HttpGet]
        //[ValidateAntiForgeryToken]
        public IActionResult LogInPerson(PeopleLogInDTO personLogIn)
        {
            if (!ModelState.IsValid) { return View("LogIn", personLogIn); }

            //admin
            if(personLogIn.Email == "admin@admin" && personLogIn.Password == "admin")
            {
                var secretKeyAdmin = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("JWTTokenSecretKey_JWTTokenSecretKey"));
                var signinCredentialsAdmin = new SigningCredentials(secretKeyAdmin, SecurityAlgorithms.HmacSha256);
                var tokeOptionsAdmin = new JwtSecurityToken(
                    issuer: "http://*:7001",
                    audience: "http://*:7001",
                    claims: new List<Claim>()
                    {
                    new Claim(ClaimTypes.NameIdentifier, "1"),
                    new Claim(ClaimTypes.Email, "admin@admin"),
                    new Claim(ClaimTypes.Role, "admin")
                    },
                    expires: DateTime.Now.AddMinutes(15),
                    signingCredentials: signinCredentialsAdmin);

                var tokenStringAdmin = new JwtSecurityTokenHandler().WriteToken(tokeOptionsAdmin);

                HttpContext.Session.SetString("Token", tokenStringAdmin);
                return Redirect("/People");
            }

            if (!dBService.getPeopleToList().Any(p => p.Email == personLogIn.Email))
            {
                ModelState.AddModelError("email", "Email is not found!");
                return View("LogIn", personLogIn);
            }

            var person = dBService.getPeopleToList().Where(p => p.Email == personLogIn.Email).FirstOrDefault();

            if(person.Password != hashService.HashPassword(personLogIn.Password))
            {
                ModelState.AddModelError("password", "Wrong password!");
                return View("LogIn", personLogIn);
            }

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("JWTTokenSecretKey_JWTTokenSecretKey"));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokeOptions = new JwtSecurityToken(
                issuer: "http://*:7001",
                audience: "http://*:7001",
                claims: new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, person.PersonId.ToString()),
                    new Claim(ClaimTypes.Email, person.Email),
                    new Claim(ClaimTypes.Role, "user")
                },
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: signinCredentials);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);

            HttpContext.Session.SetString("Token", tokenString);

            return Redirect("/People");

            
        }

        public IActionResult LogOutPerson()
        {
            HttpContext.Session.Remove("Token");

            return Redirect("/");
        }
    }
}
