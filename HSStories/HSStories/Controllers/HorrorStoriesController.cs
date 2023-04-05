using HSStories.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



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
        public async Task<ActionResult<List<Horrorstory>>> GetStories()
        {
            var stories = await _context.Horrorstories.ToListAsync();

            if (stories == null || stories.Count == 0)
            {
                return NotFound();
            }

            return Ok(stories);
        }


        [HttpGet("Story/{id}")]
        public async Task<ActionResult<Horrorstory>> GetStory(int id)
        {
            var story = await _context.Horrorstories.FindAsync(id);

            if (story == null)
            {
                return NotFound();
            }

            return Ok(story);
        }

        [HttpPost("CreateStory"), Authorize]
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

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok("Story Created");
        }



        [HttpPut("EditStory/{id}"), Authorize]
        public async Task<ActionResult<Horrorstory>> EditStory(int id, Horrorstory Updatedstory)
        {
            var story = await _context.Horrorstories.FindAsync(id);

            if (story == null)

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


        [HttpDelete("DeleteStory"), Authorize]
        public async Task<ActionResult> DeleteStory(int id)
        {
            var story = await _context.Horrorstories.FindAsync(id);

            if (story == null)

            {
                return BadRequest("That Story Doesnt Exist");
            }

            _context.Remove(story);
            _context.SaveChanges();

            return Ok("Story Correctly Removed");
        }
    }
}
