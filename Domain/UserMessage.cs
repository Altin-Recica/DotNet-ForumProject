using System.ComponentModel.DataAnnotations;

namespace Forum.BL.Domain;

public class UserMessage
{
    public int Id { get; set; }
    [DataType(DataType.Date)] public DateOnly InteractionDate { get; set; }
    [Required] public User User { get; set; }
    [Required] public Message Message { get; set; }
}