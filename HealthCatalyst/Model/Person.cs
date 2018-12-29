using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HealthCatalyst.Model
{
    public class Person
    {
        [Key]
        public int Id { get; set; }

        public string Photo { get; set; }

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
