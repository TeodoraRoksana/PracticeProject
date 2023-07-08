using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Practice.Controllers;
using Practice.Models;
using Practice.Models.DTO;
using Practice.Services.DBService;
using Practice.Services.EmailService;
using System;

namespace TestProject
{
    public class PairControllersTests
    {
        private GeneritePairController generiteController;
        private PairController pairController;
        private Mock<PracticeContext> mockDBContext;
        private Mock<IDBService> mockContextService;
        private Mock<IEmailService> mockEmailService;
        private People testPerson;
        private People testPerson2;
        private Pair pair;
        private EmailDTO request;

        [SetUp]
        public void Setup()
        {
            testPerson = new People()
            {
                PersonId = 1,
                FirstName = "Test",
                LastName = "Test",
                Email = "Test@gmail.com",
                Password = "password",
                Sex = 1,
                Age = 1,
                Birthday = DateTime.Now,
                AboutMe = "Test",
                Likes = 1,
                Dislikes = 1,
                PairFirstPeople = null,
                PairSecondPeople = null
            };
            testPerson2 = new People()
            {
                PersonId = 2,
                FirstName = "Test2",
                LastName = "Test2",
                Email = "Test2@gmail.com",
                Password = "password2",
                Sex = 0,
                Age = 2,
                Birthday = DateTime.Now,
                AboutMe = "Test2",
                Likes = 1,
                Dislikes = 1,
                PairFirstPeople = null,
                PairSecondPeople = null
            };
            pair = new Pair()
            {
                PairsId = 1,
                FirstPerson = testPerson,
                SecondPerson = testPerson,
                FirstPersonComment = "",
                SecondPersonComment = "",
                FirstPersonId = 1,
                SecondPersonId = 1,
                Data = DateTime.Now
            };

            request = new EmailDTO()
            {
                To = "a@a",
                Subject = "Test",
                Body = "Test"
            };

            var mockSet = new Mock<DbSet<People>>(); ;
            mockDBContext = new Mock<PracticeContext>(new DbContextOptions<PracticeContext>());
            mockDBContext.Setup(m => m.People).Returns(mockSet.Object);
            mockEmailService = new Mock<IEmailService>();
            mockEmailService.Setup(m => m.SendEmail(request)).Verifiable();

            mockContextService = new Mock<IDBService>();
            mockContextService.Setup(m => m.saveChengesInDB()).Verifiable();
            mockContextService.Setup(m => m.addPairToDB(default)).Verifiable();
            mockContextService.Setup(m => m.removePairFromDB(pair)).Verifiable();
            mockContextService.Setup(m => m.removePairFromDB(new Pair())).Verifiable();
            mockContextService.Setup(m => m.searchPairByID(1)).Returns(pair).Verifiable();
            mockContextService.Setup(m => m.searchPairByID(default)).Verifiable();
            mockContextService.Setup(m => m.getPairWithIncludesToList()).Returns(new List<Pair>()).Verifiable();
            mockContextService.Setup(m => m.getPeopleToList()).Returns(new List<People>()).Verifiable();

            generiteController = new GeneritePairController(mockContextService.Object, mockEmailService.Object);
            pairController = new PairController(mockContextService.Object);
        }

        [Test]
        public void DeletePairsTest()
        {
            var result = (ViewResult)pairController.Delete(1);

            mockContextService.Verify(m => m.searchPairByID(1), Times.Once());
            mockContextService.Verify(m => m.searchPairByID(default), Times.Never());
            mockContextService.Verify(m => m.removePairFromDB(pair), Times.Once());
            mockContextService.Verify(m => m.removePairFromDB(new Pair()), Times.Never());
            mockContextService.Verify(m => m.saveChengesInDB(), Times.Once());
            mockContextService.Verify(m => m.getPairWithIncludesToList(), Times.Once());
            Assert.AreEqual("Index", result.ViewName);
        }

        [Test]
        public void GeneritePairsTest()
        {
            generiteController.Post();

            mockContextService.Verify(m => m.getPeopleToList(), Times.Once());
            mockContextService.Verify(m => m.addPairToDB(default), Times.Never());
            mockEmailService.Verify(m => m.SendEmail(request), Times.Never);
            mockContextService.Verify(m => m.saveChengesInDB(), Times.Once());
        }

    }
}