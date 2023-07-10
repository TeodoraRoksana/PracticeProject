using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Practice.Controllers;
using Practice.Models;
using Practice.Services.DBService;
using Practice.Services.HashService;
using System;

namespace TestProject
{
    public class PeopleControllersTests
    {
        private AddPersonController addController;
        private EditPersonController editController;
        private PeopleController peopleController;
        private Mock<PracticeContext> mockDBContext;
        private Mock<IDBService> mockContextService;
        private Mock<IHashService> mockHashService;
        private People testPerson;
        private People testPerson2;

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

            var mockSet = new Mock<DbSet<People>>(); ;
            mockDBContext = new Mock<PracticeContext>(new DbContextOptions<PracticeContext>());
            mockDBContext.Setup(m => m.People).Returns(mockSet.Object);
            mockContextService = new Mock<IDBService>();
            mockContextService.Setup(m => m.saveChengesInDB()).Verifiable();
            mockContextService.Setup(m => m.addPersonToDB(testPerson)).Verifiable();
            mockContextService.Setup(m => m.removePersonFromDB(testPerson)).Verifiable();
            mockContextService.Setup(m => m.removePersonFromDB(testPerson2)).Verifiable();
            mockContextService.Setup(m => m.searchPersonByID(1)).Returns(testPerson).Verifiable();
            mockContextService.Setup(m => m.searchPersonByID(default)).Returns(testPerson2).Verifiable();
            mockContextService.Setup(m => m.getPeopleToList()).Returns(new List<People>()).Verifiable();
            mockContextService.Setup(m => m.getPeopleWithIncludesToList()).Returns(new List<People>()).Verifiable();

            mockHashService = new Mock<IHashService>();
            mockHashService.Setup(m => m.HashPassword(testPerson.Password)).Verifiable();

            addController = new AddPersonController(mockContextService.Object, mockHashService.Object);
            editController = new EditPersonController(mockContextService.Object);
            peopleController = new PeopleController(mockContextService.Object);
        }

        [Test]
        public void AddNewPersonTest()
        {
            addController.Post(testPerson);
            mockHashService.Verify(m => m.HashPassword("password"), Times.Once());
            mockContextService.Verify(m => m.addPersonToDB(testPerson), Times.Once());
            mockContextService.Verify(m => m.saveChengesInDB(), Times.Once());
        }

        [Test]
        public void AddNewPersonInvalidModelTest()
        {
            addController.ModelState.AddModelError("email", "email is absent");
            addController.Post(testPerson);
            mockHashService.Verify(m => m.HashPassword(testPerson.Password), Times.Never());
            mockContextService.Verify(m => m.addPersonToDB(testPerson), Times.Never());
            mockContextService.Verify(m => m.saveChengesInDB(), Times.Never());
        }

        [Test]
        public void AddViewTest()
        {
            var result = (ViewResult)addController.Add();
            Assert.AreEqual("Add", result.ViewName);
        }

        [Test]
        public void EditPersonTest()
        {
            editController.Put(1, testPerson);

            mockContextService.Verify(m => m.searchPersonByID(1), Times.Once());
            mockContextService.Verify(m => m.searchPersonByID(default), Times.Never());
            mockContextService.Verify(m => m.saveChengesInDB(), Times.Once());
        }

        [Test]
        public void EditPersonInvalidModelTest()
        {
            editController.ModelState.AddModelError("email", "email is absent");
            editController.Put(1, testPerson);

            mockContextService.Verify(m => m.searchPersonByID(1), Times.Never());
            mockContextService.Verify(m => m.searchPersonByID(default), Times.Never());
            mockContextService.Verify(m => m.saveChengesInDB(), Times.Never());
        }
    }
}