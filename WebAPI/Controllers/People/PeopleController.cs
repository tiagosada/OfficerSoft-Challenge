using System;
using Microsoft.AspNetCore.Mvc;
using Domain.People;
using Domain.Users;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

namespace WebAPI.Controllers.People
{
    [ApiController]
    [Route("[controller]")]
    public class PeopleController : ControllerBase
    {
        public readonly IPeopleService _peopleService;
        public readonly IUsersService _usersService;
        public PeopleController(IUsersService usersService, IPeopleService peopleService)
        {
            _peopleService = peopleService;
            _usersService = usersService;
        }
        
        [HttpPost]
        [Authorize]
        public IActionResult Create(CreatePersonRequest request)
        {
            var response = _peopleService.Create(
                request.Name,
                request.CPF,
                request.CEP,
                request.Address,
                request.Number,
                request.District,
                request.Complement,
                request.UF,
                request.RG
                );

            if (!response.IsValid)
            {
                return BadRequest(response.Errors);
            }

            return Ok(response.Id);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Remove(Guid id)
        {
            var studentRemoved = _peopleService.Remove(id);

            if (!studentRemoved)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult GetByID(Guid id)
        {
            var person = _peopleService.Get(id);

            if (person == null)
            {
                return NotFound();
            }

            return Ok(person);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetAll(string name)
        {
            var people = _peopleService.GetAll();
            
            if (!string.IsNullOrWhiteSpace(name))
            {
                var transformedName = name.ToLower().Trim();
                people = people.Where(x => x.Name.ToLower().Contains(transformedName));
            }
            
            return Ok(people.OrderBy(x => x.Name));
        }
    }
}