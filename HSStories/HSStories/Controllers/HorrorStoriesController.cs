using HSStories.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HSStories.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HorrorStoriesController : ControllerBase
    {
        private readonly HsstoriesContext _context;

        public HorrorStoriesController(HsstoriesContext context)
        {
            _context = context;
        }

        [HttpGet("Stories")]
        public ActionResult GetStories()
        {
            var stories = _context.Horrorstories.ToList();

            if (stories == null || stories.Count == 0)
            {
                return NotFound();
            }

            return Ok(stories);
        }

        [HttpGet("Story/{id}")]
        public ActionResult GetStory(int id)
        {
            var story = _context.Horrorstories.Find(id);

            if (story == null)
            {
                return NotFound();
            }

            return Ok(story);
        }

        [HttpPost]
        public ActionResult CreateStory(Horrorstory story)
        {
            try
            {

                _context.Add(story);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            _context.SaveChanges();

            return Ok("Story Created");
        }

        [HttpPut("EditStory/{id}")]
        public ActionResult EditStory(int id ,Horrorstory Updatedstory)
        {
            var story = _context.Horrorstories.Find(id);

            if(story == null)
            {
                return BadRequest("We Coulnd Find Any Story Whit That Id");
            }

            story.Title = Updatedstory.Title;
            story.Text = Updatedstory.Text;
            story.Image1 = Updatedstory.Image1;
            story.Image2 = Updatedstory.Image2;
            story.Image3 = Updatedstory.Image3;
            story.Image4 = Updatedstory.Image4;
            
            _context.SaveChanges();
            
            return Ok("Changes Correctly Applied :" + story);
            
        }


        [HttpDelete("DeleteStory")]
        public ActionResult DeleteStory(int id)
        {
            var story = _context.Horrorstories.Find(id);

            if(story == null)
            {
                return BadRequest("That Story Doesnt Exist");
            }

            _context.Remove(story);
            _context.SaveChanges();

            return Ok("Story Correctly Removed");
        }
    }
}
