namespace Forum.BL.Domain;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class User : IValidatableObject //Gebruiker
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Naam mag niet leeg zijn")]
    public string Name { get; set; }
    [Required(ErrorMessage = "Geboortedatum moet ingevuld zijn")] 
    public DateOnly DateOfBirth { get; set; }
    [Range(1,120, ErrorMessage = "Leeftijd moet tussen 1 en 120 liggen")]
    public int? Age { get; set; }
    public ICollection<UserMessage> UserMessages { get; set; }
    
    public User() { }

    public User(string name, DateOnly dateOfBirth, int? age)
    {
        Name = name;
        DateOfBirth = dateOfBirth;
        Age = age;
    }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        List<ValidationResult> errors = new List<ValidationResult>();
        
        if (DateOfBirth > DateOnly.FromDateTime(DateTime.Now))
        {
            errors.Add(new ValidationResult("Geboortedatum kan niet in de toekomst liggen.", new[] { "DateOfBirth" }));
        }
        
        return errors;
    }
    
    public override string ToString()
    {
        return $"{Name}, Geboortedatum: {DateOfBirth:yyyy-MM-dd} ( {(Age.HasValue ? Age.Value.ToString() : "N/A")} jaar )";
    }
}