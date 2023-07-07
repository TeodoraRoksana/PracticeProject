using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Practice.Controllers;
using Practice.Models;
using System;

namespace TestProject
{
    public class PeopleControllersTests
    {
        private AddController addController;
        private EditController editController;
        private PeopleController peopleController;
        private Mock<PracticeContext> mockContext;
        private People testPerson;

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

            var mockSet = new Mock<DbSet<People>>(); ;
            mockContext = new Mock<PracticeContext>(new DbContextOptions<PracticeContext>());
            mockContext.Setup(m => m.People).Returns(mockSet.Object);
            //mockContext.Setup(c => c.SaveChanges(default)).Returns(() => Task.Run(() => 1)).Verifiable();
            
            addController = new AddController(mockContext.Object);
            editController = new EditController(mockContext.Object);
            peopleController = new PeopleController(mockContext.Object);
        }

        [Test]
        public void AddNewPersonTest()
        {
            addController.Post(testPerson);
            mockContext.Verify(x => x.People.Add(testPerson), Times.Once());
            mockContext.Verify(x => x.SaveChanges(), Times.Once());
        }

        [Test]
        public void AddNewPersonInvalidModelTest()
        {
            addController.ModelState.AddModelError("email", "email is absent");
            addController.Post(testPerson);
            mockContext.Verify(x => x.People.Add(testPerson), Times.Never());
            mockContext.Verify(x => x.SaveChanges(), Times.Never());
        }

        [Test]
        public void AddViewTest()
        {
            var result = (ViewResult)addController.Add();
            Assert.AreEqual("Add", result.ViewName);
        }
    }
}