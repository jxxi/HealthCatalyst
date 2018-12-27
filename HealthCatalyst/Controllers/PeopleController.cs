using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace HealthCatalyst.Controllers
{
    [Route("api/[controller]")]
    public class PeopleController : Controller
    {
        private static IEnumerable<Person> SamplePeople = new[]
        {
            new Person
            {
                FirstName = "Harry",
                LastName = "Potter",
                Address = "4 Privet Drive, Little Whinging, Surrey",
                Birthday = new DateTime(1980, 7, 31),
                Interests = "Potions, Defense Against the Dark Arts, Baking",
                Photo = Properties.Resources.Harry
            },

            new Person
            {
                FirstName = "Sherlock",
                LastName = "Holmes",
                Address = "221B Baker Street",
                Birthday = new DateTime(1854, 1, 6),
                Interests = "Being a detective, Reading, Chess",
                Photo = Properties.Resources.Sherlock
            },

            new Person
            {
                FirstName = "Hermione",
                LastName = "Grainger",
                Address = "4 Privet Drive, Little Whinging, Surrey",
                Birthday = new DateTime(1979, 9, 19),
                Interests = "Charms, Reading, Cats",
                Photo = Properties.Resources.Hermione
            }
        };

        [HttpGet("[action]")]
        public IEnumerable<Person> People()
        {
            return SamplePeople;
        }

        public class Person
        {
            public byte[] Photo { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Address { get; set; }
            public string Interests { get; set; }
            public DateTime Birthday { get; set; }

            public string Age
            {
                get
                {
                    var today = DateTime.Today;
                    var age = today.Year - Birthday.Year;
                    // Go back to the year the person was born in case of a leap year
                    if (Birthday > today.AddYears(-age)) age--;

                    return age.ToString();
                }
            }
        }
    }
}
