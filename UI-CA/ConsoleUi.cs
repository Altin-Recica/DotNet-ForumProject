namespace Forum.UI.CA;

using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using BL.Domain;
using BL;

class ConsoleUi
{
    private readonly IManager _mgr;

    public ConsoleUi(IManager mgr)
    {
        _mgr = mgr;
    }

    public void Run()
    {
        int choice;
        do
        {
            Console.WriteLine("Wat wil je doen?");
            Console.WriteLine("==================");
            Console.WriteLine("0) Afsluiten");
            Console.WriteLine("1) Toon alle Gebruikers");
            Console.WriteLine("2) Toon berichten van een bepaald type");
            Console.WriteLine("3) Toon alle Berichten");
            Console.WriteLine("4) Filter gebruikers op naam en/of geboortedatum");
            Console.WriteLine("5) Voeg een nieuwe gebruiker toe");
            Console.WriteLine("6) Voeg een nieuw bericht toe");
            Console.WriteLine("7) Add message to user");
            Console.WriteLine("8) Remove message from user");
            
            Console.Write("Keuze (0-8): ");

            if (int.TryParse(Console.ReadLine(), out choice))
            {
                switch (choice)
                {
                    case 1:
                        ShowAllUsers();
                        break;
                    case 2:
                        FilterMessagesByType();
                        break;
                    case 3:
                        ShowAllMessages();
                        break;
                    case 4:
                        FilterUsersWithNameOrDateOfBirth();
                        break;
                    case 5:
                        AddNewUser();
                        break;
                    case 6:
                        AddNewMessage();
                        break;
                    case 7:
                        AddMessageToUser();
                        break;
                    case 8:
                        RemoveMessageFromUser();
                        break;
                    case 0:
                        Console.WriteLine("Tot ziens!");
                        break;
                    default:
                        Console.WriteLine("Ongeldige keuze. Probeer opnieuw.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Ongeldige invoer. Voer een getal in (0-4).");
            }

            Console.WriteLine();
        } while (choice != 0);
    }
    
    private void ShowAllUsers()
    {
        Console.WriteLine("Alle Gebruikers:");
        Console.WriteLine("===================");

        foreach (var user in _mgr.GetAllUsersWithMessages())
        {
            Console.WriteLine(user);

            foreach (UserMessage userMessage in user.UserMessages)
            {
                Console.WriteLine($" - {userMessage.Message}");
            }
        }
        Console.WriteLine("===================");
    }
    
    private void FilterMessagesByType()
    {
        Console.Write("Geef het berichttype (0 voor Text, 1 voor Link): ");
        if (Enum.TryParse<MessageType>(Console.ReadLine(), out var messageType))
        {
            Console.WriteLine($"Berichten van type '{messageType}'");
            Console.WriteLine("===============================");

            foreach (var message in _mgr.GetMessagesByMessageType(messageType))
            {
                Console.WriteLine(message);
            }
            
            Console.WriteLine("===============================");
        }
        else
        {
            Console.WriteLine("Ongeldig berichttype.");
        }
        
    } 
    
    private void ShowAllMessages()
    {
        Console.WriteLine("Alle Berichten:");
        Console.WriteLine("===================");
        
        foreach (var message in _mgr.GetAllMessagesWithComments())
        {
            Console.WriteLine(message);
            foreach (Comment comment in message.Comments)
            {
                Console.WriteLine($" - {comment}");
            }
        }
        Console.WriteLine("===================");
    }

    private void FilterUsersWithNameOrDateOfBirth()
    {
        
        Console.Write("Enter (part of) the name or leave blank: ");
        string name = Console.ReadLine();
        
        Console.Write("Enter a full date (yyyy/MM/dd) or leave blank: ");
        DateOnly? date = null;
        if (DateOnly.TryParse(Console.ReadLine(), out DateOnly parsedDate))
        {
            date = parsedDate;
        }
        
        Console.WriteLine("Filter resultaten:");
        Console.WriteLine("===================");
        
        foreach (User user in _mgr.GetUsersByNameAndDateOfBirth(name, date))
        {
            Console.WriteLine(user);
        }
        
        Console.WriteLine("===================");
    }
    
    private void AddNewUser()
    {
        Console.WriteLine("Voeg een nieuwe gebruiker toe:");
        Console.WriteLine("==============================");
        
        Console.Write("Naam: ");
        string name = Console.ReadLine();
        
        DateOnly date;
        bool isValidDate;

        do
        {
            Console.Write("Geboortedatum (yyyy/MM/dd): ");
            string input = Console.ReadLine();
    
            isValidDate = DateOnly.TryParse(input, out date);
    
            if (!isValidDate)
            {
                Console.WriteLine("Error: Ongeldige datum. Probeer opnieuw.");
            }
        }
        while (!isValidDate);
        
        int age = CalculateAge(date);
        
        try
        {
            User newUser = _mgr.AddUser(name, date, age);
            Console.WriteLine($"Nieuwe gebruiker toegevoegd: {newUser}");
        }
        catch (ValidationException exc)
        {
            foreach (string errorMsg in exc.Message.Split("|"))
            {
                Console.WriteLine(errorMsg);
            }
        }
        
        Console.WriteLine("==============================");
    }

    private int CalculateAge(DateOnly date)
    {
        DateOnly today = DateOnly.FromDateTime(DateTime.Today);
        int age = today.Year - date.Year;
    
        if (today < date.AddYears(age))
        {
            age--;
        }
    
        return age;
    }

    void AddNewMessage()
    {
        Console.WriteLine("Voeg een nieuwe bericht toe:");
        Console.WriteLine("==============================");
        
        Console.Write("Ingoud: ");
        string content = Console.ReadLine();
        
        MessageType messageType;
        bool isValidType;
        do
        {
            Console.Write("Berichttype (0 voor Text, 1 voor Link): ");
            string input = Console.ReadLine();
    
            isValidType = Enum.TryParse(input, out messageType);
    
            if (!isValidType)
            {
                Console.WriteLine("Error: Ongeldige berichttype. Probeer opnieuw.");
            }
        }
        while (!isValidType);

        
        DateOnly date;
        bool isValidDate;
        do
        {
            Console.Write("Verzenddatum (yyyy/MM/dd): ");
            string input = Console.ReadLine();
    
            isValidDate = DateOnly.TryParse(input, out date);
    
            if (!isValidDate)
            {
                Console.WriteLine("Error: Ongeldige datum. Probeer opnieuw.");
            }
        }
        while (!isValidDate);
        
        try
        {
            Message newMessage = _mgr.AddMessage(content, messageType, date);
            Console.WriteLine($"Nieuwe gebruiker toegevoegd: {newMessage}");
        }
        catch (ValidationException exc)
        {
            foreach (string errorMsg in exc.Message.Split("|"))
            {
                Console.WriteLine(errorMsg);
            }
        }
        
        Console.WriteLine("==============================");
    }
    
    private void AddMessageToUser()
    {
        DateOnly interactionDate = GetInteractionDate();
        Message selectedMessage = GetMessage();
        User selectedUser = GetUser();
        
        try
        {
            _mgr.AddUserMessage(selectedUser, selectedMessage, interactionDate);
            Console.WriteLine($"'{selectedMessage.Content} toegevoegd bij {selectedUser.Name}'");
        }
        catch (Exception)
        {
            Console.WriteLine("Error bij het toevoegen van dit bericht aan de gebruiker");
        }
        
    }

    private void RemoveMessageFromUser()
    {
        User selectedUser = GetUser();
        Message selectedMessage = GetMessageOfUser(selectedUser.Id);
        
        try
        {
            _mgr.RemoveUserMessage(selectedUser.Id, selectedMessage.Id);
            Console.WriteLine($"'{selectedMessage.Content}' verwijderd bij '{selectedUser.Name}'");
        }
        catch (Exception)
        {
            Console.WriteLine("Error bij het verwijderen van dit bericht.");
        }
    }
    
    private DateOnly GetInteractionDate()
    {
        while (true)
        {
            Console.Write("Datum van interactie (YYYY-MM-DD): ");
            if (DateOnly.TryParse(Console.ReadLine(), out DateOnly interactionDate))
            {
                return interactionDate;
            }
            else
            {
                Console.WriteLine("Interactiedatum is ongeldig. Voer een geldige datum in.");
            }
        }
    }
    
    private User GetUser()
    {
        while (true)
        {
            Console.WriteLine("Kies het gebruiker:");
            foreach (User user in _mgr.GetUsers())
            {
                Console.WriteLine($"{user.Id}: {user.Name}");
            }
            Console.Write("Gebruiker ID: ");
            if (int.TryParse(Console.ReadLine(), out int userId) && _mgr.GetUsers().Any(u => u.Id == userId))
            {
                User user = _mgr.GetUsers().FirstOrDefault(u => u.Id == userId);
                return user;
            }
            else
            {
                Console.WriteLine("Gebruiker ID is ongeldig. Voer een geldig ID in. ");
            }
            
        }
    }
    
    private Message GetMessage()
    {
        while (true)
        {
            Console.WriteLine("Kies het bericht:");
            foreach (Message message in _mgr.GetMessages())
            {
                Console.WriteLine($"{message.Id}: {message.Content}");
            }
            Console.Write("Bericht ID: ");
            if (int.TryParse(Console.ReadLine(), out int messageId) && _mgr.GetMessages().Any(m => m.Id == messageId))
            {
                Message message = _mgr.GetMessages().FirstOrDefault(m => m.Id == messageId);
                return message;
            }
            else
            {
                Console.WriteLine("Bericht ID is ongeldig. Voer een geldig ID in.");
            }
            
        }
    }

    private Message GetMessageOfUser(int userId)
    {
        while (true)
        {
            Console.WriteLine("Kies het bericht:");
            foreach (Message message in _mgr.GetUserMessages(userId))
            {
                Console.WriteLine($"{message.Id}: {message.Content}");
            }
            Console.Write("Bericht ID: ");
            if (int.TryParse(Console.ReadLine(), out int messageId) && _mgr.GetUserMessages(userId).Any(m => m.Id == messageId))
            {
                Message message = _mgr.GetMessages().FirstOrDefault(m => m.Id == messageId);
                return message;
            }
            else
            {
                Console.WriteLine("Bericht ID is ongeldig. Voer een geldig ID in.");
            }
            
        }
    }
}

