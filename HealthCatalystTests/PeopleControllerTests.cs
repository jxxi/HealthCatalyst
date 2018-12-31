using NUnit.Framework;
using System;
using System.Linq;
using HealthCatalyst.Controllers;
using HealthCatalyst.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace HealthCatalystTests
{
    [TestFixture]
    public class PeopleControllerTests
    {
        [Test, RequiresThread]
        public void GetAllPeopleTest()
        {
            // In-memory database only exists while the connection is open
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            try
            {
                var options = new DbContextOptionsBuilder<PeopleContext>()
                    .UseSqlite(connection)
                    .Options;

                // Create the schema in the database
                using (var context = new PeopleContext())
                {
                    context.Database.EnsureCreated();
                }

                // Insert seed data into the database using one instance of the context
                using (var context = new PeopleContext())
                {
                    context.People.Add(new Person { FirstName = "Jasmine", LastName = "Wiggins", Address = "1111 Fannin", Birthday = new DateTime(1992, 7, 21), Interests = "aerial, dogs, traveling", Photo = "fakepath" });
                    context.People.Add(new Person { FirstName = "Hill", LastName = "Billy", Address = "1121 Fannin", Birthday = new DateTime(1950, 10, 30), Interests = "horses, chickens, texas", Photo = "fakepath" });
                    context.People.Add(new Person { FirstName = "Mr", LastName = "Test", Address = "1101 Fannin", Birthday = new DateTime(1970, 7, 20), Interests = "testing code, running", Photo = "fakepath" });
                    context.SaveChanges();
                }

                // Use a clean instance of the context to run the test
                using (var context = new PeopleContext())
                {
                    var service = new PeopleController(context);
                    var result = service.GetPeople();
                    Assert.GreaterOrEqual(result.Count(), 3);
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [Test, RequiresThread]
        public void GetPeopleByNameTest()
        {
            // In-memory database only exists while the connection is open
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            try
            {
                var options = new DbContextOptionsBuilder<PeopleContext>()
                    .UseSqlite(connection)
                    .Options;

                // Create the schema in the database
                using (var context = new PeopleContext())
                {
                    context.Database.EnsureCreated();
                }

                // Insert seed data into the database using one instance of the context
                using (var context = new PeopleContext())
                {
                    context.People.Add(new Person { FirstName = "Jasmine", LastName = "Wiggins", Address = "1111 Fannin", Birthday = new DateTime(1992, 7, 21), Interests = "aerial, dogs, traveling", Photo = "fakepath" });
                    context.People.Add(new Person { FirstName = "Hill", LastName = "Billy", Address = "1121 Fannin", Birthday = new DateTime(1950, 10, 30), Interests = "horses, chickens, texas", Photo = "fakepath" });
                    context.People.Add(new Person { FirstName = "Mr", LastName = "Test", Address = "1101 Fannin", Birthday = new DateTime(1970, 7, 20), Interests = "testing code, running", Photo = "fakepath" });
                    context.SaveChanges();
                }

                // Use a clean instance of the context to run the test
                using (var context = new PeopleContext())
                {
                    var service = new PeopleController(context);
                    var result = service.GetPeopleByName("Jasmine");
                    var status = result.Result as OkObjectResult;

                    Assert.IsNotNull(result);
                    Assert.AreEqual(200, status.StatusCode);

                }
            }
            finally
            {
                connection.Close();
            }
        }

        [Test, RequiresThread]
        public void GetPeopleByNameWithSpecialCharacterTest()
        {
            // In-memory database only exists while the connection is open
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            try
            {
                var options = new DbContextOptionsBuilder<PeopleContext>()
                    .UseSqlite(connection)
                    .Options;

                // Create the schema in the database
                using (var context = new PeopleContext())
                {
                    context.Database.EnsureCreated();
                }

                // Use a clean instance of the context to run the test
                using (var context = new PeopleContext())
                {
                    var service = new PeopleController(context);
                    var result = service.GetPeopleByName("//");
                    var status = result.Result as StatusCodeResult;

                    Assert.AreEqual(400, status.StatusCode);
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [Test, RequiresThread]
        public void GetPeopleByNameWithNoMatchTest()
        {
            // In-memory database only exists while the connection is open
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            try
            {
                var options = new DbContextOptionsBuilder<PeopleContext>()
                    .UseSqlite(connection)
                    .Options;

                // Create the schema in the database
                using (var context = new PeopleContext())
                {
                    context.Database.EnsureCreated();
                }

                // Insert seed data into the database using one instance of the context
                using (var context = new PeopleContext())
                {
                    context.People.Add(new Person { FirstName = "Jasmine", LastName = "Wiggins", Address = "1111 Fannin", Birthday = new DateTime(1992, 7, 21), Interests = "aerial, dogs, traveling", Photo = "fakepath" });
                    context.People.Add(new Person { FirstName = "Hill", LastName = "Billy", Address = "1121 Fannin", Birthday = new DateTime(1950, 10, 30), Interests = "horses, chickens, texas", Photo = "fakepath" });
                    context.People.Add(new Person { FirstName = "Mr", LastName = "Test", Address = "1101 Fannin", Birthday = new DateTime(1970, 7, 20), Interests = "testing code, running", Photo = "fakepath" });
                    context.SaveChanges();
                }

                // Use a clean instance of the context to run the test
                using (var context = new PeopleContext())
                {
                    var service = new PeopleController(context);
                    var result = service.GetPeopleByName("z").Result;
                    var status = result as StatusCodeResult;

                    Assert.AreEqual(404, status.StatusCode);
                }
            }
            finally
            {
                connection.Close();
            }
        }
    }
}