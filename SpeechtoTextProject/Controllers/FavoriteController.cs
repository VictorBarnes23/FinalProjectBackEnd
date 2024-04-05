using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpeechtoTextProject.Models;

namespace SpeechtoTextProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteController : ControllerBase
    {
        private SpeechtoTextContext DbContext = new SpeechtoTextContext();


        [HttpGet()]
        public IActionResult GetAll()
        {
            List<FavoriteWord> result = DbContext.FavoriteWords.ToList();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            List<FavoriteWord> result = DbContext.FavoriteWords.Where(f => f.UserId == id).ToList();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public IActionResult AddWord([FromBody] FavoriteWord f)
        {
            DbContext.FavoriteWords.Add(f);
            DbContext.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = f.UserId }, f);
        }


        [HttpPut("{id}")]
        public IActionResult UpdateFav(int id,[FromBody] FavoriteWord f )
        {
            FavoriteWord FavWord = DbContext.FavoriteWords.Find(id);
            FavWord.Context = f.Context;
            
            if (FavWord == null)
            {
                return BadRequest();
            }
            else if (!DbContext.FavoriteWords.Any(f => f.Id == id))
            {
                return NotFound();
            }
            DbContext.FavoriteWords.Update(FavWord);
            DbContext.SaveChanges();
            return NoContent();
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteById(int id)
        {
            FavoriteWord result = DbContext.FavoriteWords.FirstOrDefault(f => f.Id == id);
            if(result == null)
            {
                return NotFound($"No favorite found for id: {id} ");
            }
            DbContext.FavoriteWords.Remove(result);
            DbContext.SaveChanges();
            return NoContent();
        }
    }
}
