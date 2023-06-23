using Announcement_List.BLL.Service;
using Announcement_List.Domain_Layer.Model;
using Microsoft.AspNetCore.Mvc;

namespace Announcement_List.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AnnouncementController : ControllerBase
{
    private readonly ILogger<AnnouncementController> _logger;
    private readonly IAnnouncementService _service;

    public AnnouncementController(IAnnouncementService service, ILogger<AnnouncementController> logger)
    {
        _service = service;
        _logger = logger;
    }

    // GET: api/Announcement
    [HttpGet("All")]
    public async Task<ActionResult<IEnumerable<Announcement>>> GetAnnouncements()
    {
        try
        {
            var announcement_list = await _service.GetAllAnnouncements();
            if (announcement_list is null)
            {
                _logger.LogInformation("Elements not found");
                return NotFound();
            }

            return Ok(announcement_list);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    // GET: api/Announcement/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Announcement>> GetAnnouncement(int id)
    {
        try
        {
            var announcement = await _service.GetAnnouncementById(id);
            if (announcement is null)
            {
                _logger.LogInformation($"Element {id} not found ", DateTimeOffset.UtcNow);

                return NotFound();
            }

            return announcement;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    // PUT: api/Announcement/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAnnouncement(int id, Announcement announcement)
    {
        try
        {
            var editedAnnouncement = await _service.PutAnnouncement(announcement);

            if (editedAnnouncement is null)
            {
                _logger.LogInformation("Element not found");

                return BadRequest();
            }

            return Ok(announcement);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    
    [HttpGet("Similar{id}")]
    public async Task<ActionResult<IEnumerable<Announcement>>> GetSimilarAnnouncements(int id)
    {
        try
        {
            var similarAnnouncements = await _service.Find3Similar(id);

            if (similarAnnouncements.Any())
            {
                return Ok(similarAnnouncements);
            }
            _logger.LogInformation("Element not found");

            return NotFound();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }


    // POST: api/Announcement
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Announcement>> PostAnnouncement(Announcement announcement)
    {
        await _service.AddAnnouncement(announcement);

        return CreatedAtAction("GetAnnouncement", new { id = announcement.Id }, announcement);
    }

    // DELETE: api/Announcement/
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAnnouncement(int id)
    {
        var announcement = await _service.GetAnnouncementById(id);
        if (announcement == null)
        {
            _logger.LogInformation("Element not found");

            return NotFound();
        }

        await _service.DeleteAnnouncement(announcement.Id);

        return NoContent();
    }
}