namespace Forum.BL.Domain;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
public class Message : IValidatableObject //Bericht
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Bericht mag niet leeg zijn")]
    [MinLength(5, ErrorMessage = "Bericht mag minstens 5 tekens bevatten")]
    public string Content { get; set; }
    
    public MessageType Type { get; set; }
    
    [DataType(DataType.Date, ErrorMessage = "Stuurdatum is ongeldig")]
    public DateOnly MessageDate { get; set; }
    
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    
    public ICollection<UserMessage> UserMessages { get; set; }

    public Message() { }
    
    public Message(string content, MessageType type, DateOnly messageDate)
    {
        Content = content;
        Type = type;
        MessageDate = messageDate;
    }
    
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        List<ValidationResult> errors = new List<ValidationResult>();

        if (!Enum.IsDefined(typeof(MessageType), Type))
        {
            errors.Add(new ValidationResult("Message type is onbekend.", new[] { "Type" }));
        }

        return errors;
    }
    
    public override string ToString()
    {
        return $"'{Content}' ({Type}), Stuurdatum: {MessageDate:yyyy-MM-dd}";
    }
}