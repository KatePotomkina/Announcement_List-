using Announcement_List.DAL.Data;
using Announcement_List.Domain_Layer.Model;
using Microsoft.EntityFrameworkCore;

namespace Announcement_List.BLL.Service;

public class AnnouncementService : IAnnouncementService
{
    private readonly AnnouncementDbContext _context;

    public AnnouncementService(AnnouncementDbContext context)
    {
        _context = context;
    }

    public async Task<Announcement> AddAnnouncement(Announcement newAnnouncement)
    {
        var createdAnnouncement = new Announcement
        {
            Id = newAnnouncement.Id,
            Title = newAnnouncement.Title,
            Description = newAnnouncement.Description,
            Date = newAnnouncement.Date,
            Added = newAnnouncement.Added
        };

        var addedAnnouncement = await _context.Announcement.AddAsync(createdAnnouncement);

        await _context.SaveChangesAsync();

        return addedAnnouncement.Entity;
    }

    public async Task<Announcement> DeleteAnnouncement(int id)
    {
        var deleted = await _context.Announcement.FirstOrDefaultAsync(x => x.Id == id);
        if (deleted is not null)
        {
            _context.Announcement.Remove(deleted);
            await _context.SaveChangesAsync();
        }

        return deleted;
    }

    public async Task<IEnumerable<Announcement>> GetAllAnnouncements()
    {
        return await _context.Announcement.ToListAsync();
    }

    public async Task<Announcement> GetAnnouncementById(int id)
    {
        return await _context.Announcement.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Announcement> PutAnnouncement(Announcement updated)
    {
        var announcementToEdit = await _context.Announcement.FirstOrDefaultAsync(e => e.Id == updated.Id);
        var editeddate = DateTime.Now;
        if (announcementToEdit != null)
        {
            announcementToEdit.Title = updated.Title;
            announcementToEdit.Description = updated.Description;
            announcementToEdit.Date = updated.Date;
            announcementToEdit.Added = editeddate;
            await _context.SaveChangesAsync();
        }

        return announcementToEdit;
    }

    public async Task<IEnumerable<Announcement>> Find3Similar(int announcementId)
    {
        var compared = await _context.Announcement.FirstOrDefaultAsync(x=>x.Id==announcementId);
    
        if (compared is null)
        {
            return Enumerable.Empty<Announcement>();
        }

        var allAnnouncements = await _context.Announcement.ToListAsync();

        var similarAnnouncements = allAnnouncements
            .Where(a => a.Id != announcementId &&
                        (CompareTitles(compared, a) || CompareDescription(compared, a)))
            
            .Take(3)
            .ToList();

        return similarAnnouncements;
    }

    public bool CompareTitles(Announcement compT,  Announcement other)
    {
        var comparedTitleWords = compT.Title.Split(' ');
        var otherTitleWords = other.Title.Split(' ');

        return comparedTitleWords.Any(word => otherTitleWords.Contains(word));    }

    public bool CompareDescription(Announcement compD,  Announcement other)
    {
        var comparedDescriptionWords = compD.Description.Split(' ');
        var otherDescriptionWords = other.Description.Split(' ');

        return comparedDescriptionWords.Any(word => otherDescriptionWords.Contains(word));
    }    

    
}