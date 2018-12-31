using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HealthCatalyst.Model;
using System.Threading;

namespace HealthCatalyst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly PeopleContext _context;

        public PeopleController(PeopleContext context)
        {
            _context = context;
        }

        // GET: api/People
        [HttpGet]
        public IEnumerable<Person> GetPeople()
        {
            return _context.People;
        }

        // GET: api/People/searchInput
        [HttpGet("{searchInput}", Name = "Search")]
        public async Task<IActionResult> GetPeopleByName([FromRoute] string searchInput)
        {
            Thread.Sleep(5000);
            if(HasSpecialCharacters(searchInput))
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var lowerSearch = searchInput.ToLower();
            var people = await _context.People.Where(x => x.FirstName.Contains(lowerSearch, StringComparison.OrdinalIgnoreCase) || 
                                                          x.LastName.Contains(lowerSearch, StringComparison.OrdinalIgnoreCase))
                                                          .ToListAsync();
            if (people == null || people.Count == 0)
            {
                return NotFound();
            }

            return Ok(people);
        }

        private bool HasSpecialCharacters(string input)
        {
            return input.Any(ch => !Char.IsLetterOrDigit(ch));
        }
    }
}