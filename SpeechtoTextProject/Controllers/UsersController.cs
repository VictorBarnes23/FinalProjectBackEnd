using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpeechtoTextProject.Models;

namespace SpeechtoTextProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private SpeechtoTextContext DbContext = new SpeechtoTextContext();


        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            UsersTable result = DbContext.UsersTables.FirstOrDefault(u => u.GoogleId == id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost()]
        public IActionResult AddUser([FromBody] UsersTable users)
        {
            UsersTable result = DbContext.UsersTables.FirstOrDefault(u => u.GoogleId == users.GoogleId);
            if (result == null)
            {

                DbContext.UsersTables.Add(users);
                DbContext.SaveChanges();
                return CreatedAtAction(nameof(Get), new { id = users.GoogleId }, users);

            }
            return NoContent();

        }

    }
}
