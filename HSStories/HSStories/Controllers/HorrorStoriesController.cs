using HSStories.Models;
<<<<<<< HEAD
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
=======
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
>>>>>>> 49f822e3c6306d99d2563e4a320acc869e2bb83e

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
<<<<<<< HEAD
        public async Task<ActionResult<List<Horrorstory>>> GetStories()
        {
            var stories = await _context.Horrorstories.ToListAsync();
=======
        public ActionResult GetStories()
        {
            var stories = _context.Horrorstories.ToList();
>>>>>>> 49f822e3c6306d99d2563e4a320acc869e2bb83e

            if (stories == null || stories.Count == 0)
            {
                return NotFound();
            }

            return Ok(stories);
        }

        [HttpGet("Story/{id}")]
<<<<<<< HEAD
        public async Task<ActionResult<Horrorstory>> GetStory(int id)
        {
            var story =await  _context.Horrorstories.FindAsync(id);
=======
        public ActionResult GetStory(int id)
        {
            var story = _context.Horrorstories.Find(id);
>>>>>>> 49f822e3c6306d99d2563e4a320acc869e2bb83e

            if (story == null)
            {
                return NotFound();
            }

            return Ok(story);
        }

<<<<<<< HEAD
        [HttpPost("CreateStory"),Authorize]
        public async Task<IActionResult> CreateStoryAsync([FromForm] Horrorstory story, List<IFormFile> files)
        {
            try
            {
                if (files != null && files.Count > 0)
                {
                    for (int i = 0; i < files.Count; i++)
                    {
                        var file = files[i];

                        using (var ms = new MemoryStream())
                        {
                            await file.CopyToAsync(ms);

                            switch (i)
                            {
                                case 0:
                                    story.Image1 = ms.ToArray();
                                    break;
                                case 1:
                                    story.Image2 = ms.ToArray();
                                    break;
                                case 2:
                                    story.Image3 = ms.ToArray();
                                    break;
                                case 3:
                                    story.Image4 = ms.ToArray();
                                    break;
                            }
                        }
                    }
                }

                _context.Add(story);
                await _context.SaveChangesAsync();
=======
        [HttpPost]
        public ActionResult CreateStory(Horrorstory story)
        {
            try
            {

                _context.Add(story);
>>>>>>> 49f822e3c6306d99d2563e4a320acc869e2bb83e
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
<<<<<<< HEAD
=======
            _context.SaveChanges();
>>>>>>> 49f822e3c6306d99d2563e4a320acc869e2bb83e

            return Ok("Story Created");
        }

<<<<<<< HEAD

        [HttpPut("EditStory/{id}"),Authorize]
        public async Task <ActionResult<Horrorstory>> EditStory(int id, Horrorstory Updatedstory)
        {
            var story = await _context.Horrorstories.FindAsync(id);

            if (story == null)
=======
        [HttpPut("EditStory/{id}")]
        public ActionResult EditStory(int id ,Horrorstory Updatedstory)
        {
            var story = _context.Horrorstories.Find(id);

            if(story == null)
>>>>>>> 49f822e3c6306d99d2563e4a320acc869e2bb83e
            {
                return BadRequest("We Coulnd Find Any Story Whit That Id");
            }

            story.Title = Updatedstory.Title;
            story.Text = Updatedstory.Text;
            story.Image1 = Updatedstory.Image1;
            story.Image2 = Updatedstory.Image2;
            story.Image3 = Updatedstory.Image3;
            story.Image4 = Updatedstory.Image4;
<<<<<<< HEAD

            _context.SaveChanges();

            return Ok("Changes Correctly Applied :" + story);

        }


        [HttpDelete("DeleteStory"),Authorize]
        public async Task<ActionResult> DeleteStory(int id)
        {
            var story = await _context.Horrorstories.FindAsync(id);

            if (story == null)
=======
            
            _context.SaveChanges();
            
            return Ok("Changes Correctly Applied :" + story);
            
        }


        [HttpDelete("DeleteStory")]
        public ActionResult DeleteStory(int id)
        {
            var story = _context.Horrorstories.Find(id);

            if(story == null)
>>>>>>> 49f822e3c6306d99d2563e4a320acc869e2bb83e
            {
                return BadRequest("That Story Doesnt Exist");
            }

            _context.Remove(story);
            _context.SaveChanges();

            return Ok("Story Correctly Removed");
        }
    }
}
