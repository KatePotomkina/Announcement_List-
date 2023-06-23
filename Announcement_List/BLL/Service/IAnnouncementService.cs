using Announcement_List.Domain_Layer.Model;

namespace Announcement_List.BLL.Service;

public interface IAnnouncementService
{
    Task<Announcement> AddAnnouncement(Announcement newAnnouncement);
    Task<Announcement> DeleteAnnouncement(int id);
    Task<IEnumerable<Announcement>> GetAllAnnouncements();
    Task<Announcement> GetAnnouncementById(int id);
    Task<Announcement> PutAnnouncement(Announcement updated);
    Task<IEnumerable<Announcement>> Find3Similar(int announcementId);
    bool CompareTitles(Announcement compT,  Announcement other);
    bool CompareDescription(Announcement compD, Announcement other);
}