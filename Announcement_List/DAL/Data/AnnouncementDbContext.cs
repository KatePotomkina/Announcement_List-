using Announcement_List.Domain_Layer.Model;
using Microsoft.EntityFrameworkCore;

namespace Announcement_List.DAL.Data;

public class AnnouncementDbContext : DbContext
{
    public AnnouncementDbContext(DbContextOptions<AnnouncementDbContext> options) : base(options)
    {
    }

    public DbSet<Announcement> Announcement { get; set; }
}