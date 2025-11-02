namespace Forum.UI.MVC.Models.dto;

public class NewMessageDto
{
    public int UserId { get; set; }
    public int MessageId { get; set; }
    public DateOnly InteractieDatum { get; set; }
}