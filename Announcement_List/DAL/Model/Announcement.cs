namespace Announcement_List.Domain_Layer.Model;

public class Announcement
{
    public int Id { get; set; }

    public string Title { get; set; }

    public DateTime Date { get; set; }

    public string Description { get; set; }

    public DateTime Added { get; set; }
}