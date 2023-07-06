using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using Practice.Models;
using Practice.Models.DTO;
using Practice.Services.EmailService;

namespace Practice.Controllers
{
    public class GeneriteController : Controller
    {
        private readonly PracticeContext dbContext;
        private readonly IEmailService emailService;

        public GeneriteController(PracticeContext dbContext, IEmailService emailService)
        {
            this.dbContext = dbContext;
            this.emailService = emailService;
        }

        public IActionResult Generite()
        {
            return View("Index");
        }

        
        [HttpPost]
        public IActionResult Post()
        {
            var listPeople = dbContext.People.ToList();

            Random rnd = new Random();
            while (listPeople.Count > 1) 
            {
                int firstPerson = rnd.Next(0, listPeople.Count);
                var FirstP = listPeople[firstPerson];
                listPeople.RemoveAt(firstPerson);

                int secondPerson = rnd.Next(0, listPeople.Count);
                var SecondP = listPeople[secondPerson];
                listPeople.RemoveAt(secondPerson);

                var newPair = new Pair
                {
                    PairsId = 0,
                    FirstPersonId = FirstP.PersonId,
                    SecondPersonId = SecondP.PersonId,
                    Data = DateTime.Now,
                    FirstPersonComment = "Nothing yet",
                    SecondPersonComment = "Nothing yet",
                    FirstPerson = null, 
                    SecondPerson = null
                };

                dbContext.Pairs.Add(newPair);

                EmailDTO request = new EmailDTO() {
                    To = FirstP.Email,
                    Subject = "New Pair for you " + FirstP.FirstName + "!",
                    Body = "<p> Hi " + FirstP.FirstName + "! You have a new pair with " + SecondP.FirstName + " " + SecondP.LastName + ". Log in with url to learn more: <a href='https://localhost:7001'>URL1</a> or <a href='http://localhost:5184'>URL2</a></p>"
                };
                emailService.SendEmail(request);

                EmailDTO request2 = new EmailDTO()
                {
                    To = SecondP.Email,
                    Subject = "New Pair for you " + SecondP.FirstName + "!",
                    Body = "<p> Hi " + SecondP.FirstName + "! You have a new pair with " + FirstP.FirstName + " " + FirstP.LastName + ". Log in with url to learn more: <a href='https://localhost:7001'>URL1</a> or <a href='http://localhost:5184'>URL2</a></p>"
                };
                emailService.SendEmail(request2);
            }

            dbContext.SaveChanges();

            return Redirect("/Pair");
        }

    }
}
