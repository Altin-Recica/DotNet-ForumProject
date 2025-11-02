using System.Text;

namespace Forum.BL;
using Domain;
using DAL;
using System.ComponentModel.DataAnnotations;

public class Manager : IManager
{
    private readonly IRepository _repo;

    public Manager(IRepository repository)
    {
        _repo = repository;
    }

    public User GetUser(int id)
    {
        return _repo.ReadUser(id);
    }

    public IEnumerable<User> GetUsers()
    {
        return _repo.ReadUsers();
    }

    public IEnumerable<User> GetUsersByNameAndDateOfBirth(string name, DateOnly? dateOfBirth)
    {
        return _repo.ReadUsersByNameAndDateOfBirth(name, dateOfBirth);
    }

    public User AddUser(string name, DateOnly dateOfBirth, int? age)
    {
        User newUser = new User(name, dateOfBirth, age);
        
        List<ValidationResult> errors = new List<ValidationResult>();
        bool isValid = Validator.TryValidateObject(newUser, new ValidationContext(newUser), errors, validateAllProperties: true);

        if (!isValid)
        {
            StringBuilder sb = new StringBuilder();
            foreach (ValidationResult validationResult in errors)
            {
                sb.Append("|"+validationResult.ErrorMessage);
            }
            throw new ValidationException(sb.ToString());
        }


        _repo.CreateUser(newUser);
        return newUser;
    }

    public Message GetMessage(int id)
    {
        return _repo.ReadMessage(id);
    }

    public IEnumerable<Message> GetMessages()
    {
        return _repo.ReadMessages();
    }

    public IEnumerable<Message> GetMessagesByMessageType(MessageType messageType)
    {
        return _repo.ReadMessagesByMessageType(messageType);
    }

    public Message AddMessage(string content, MessageType type, DateOnly messageDate)
    {
        Message newMessage = new Message(content, type, messageDate);
        
        List<ValidationResult> errors = new List<ValidationResult>();
        bool isValid = Validator.TryValidateObject(newMessage, new ValidationContext(newMessage), errors, validateAllProperties: true);

        if (!isValid)
        {
            StringBuilder sb = new StringBuilder();
            foreach (ValidationResult validationResult in errors)
            {
                sb.Append("|"+validationResult.ErrorMessage);
            }
            throw new ValidationException(sb.ToString());
        }

        _repo.CreateMessage(newMessage);
        return newMessage;
    }

    public Comment GetComment(int id)
    {
        return _repo.ReadComment(id);
    }

    public IEnumerable<Comment> GetComments()
    {
        return _repo.ReadComments();
    }

    public Comment AddComment(string text, int upvotes, int downvotes)
    {
        Comment newComment = new Comment(text, upvotes, downvotes);
        
        List<ValidationResult> errors = new List<ValidationResult>();
        bool isValid = Validator.TryValidateObject(newComment, new ValidationContext(newComment), errors, validateAllProperties: true);

        if (!isValid)
        {
            StringBuilder sb = new StringBuilder();
            foreach (ValidationResult validationResult in errors)
            {
                sb.Append("|"+validationResult.ErrorMessage);
            }
            throw new ValidationException(sb.ToString());
        }

        _repo.CreateComment(newComment);
        return newComment;
    }
    
    public User GetUserWithMessage(int id)
    {
        return _repo.ReadUserWithMessage(id);
    }


    public IEnumerable<Message> GetAllMessagesWithComments()
    {
        return _repo.ReadAllMessagesWithComments();
    }
    
    public UserMessage AddUserMessage(User user, Message message, DateOnly dateTime)
    {
        UserMessage userMessage = new UserMessage 
        { 
            User = user, 
            Message = message, 
            InteractionDate = dateTime
        };
        _repo.CreateUserMessage(userMessage);
        return userMessage;
    }

    public void RemoveUserMessage(int userId, int messageId)
    {
        _repo.DeleteUserMessage(userId, messageId);
    }

    public IEnumerable<User> GetAllUsersWithMessages()
    {
        return _repo.ReadAllUsersWithMessages();
    }

    public IEnumerable<Message> GetUserMessages(int userId)
    {
        return _repo.ReadMessagesOfUser(userId);
    }

    public IEnumerable<Message> GetNonUserMessages(int userId)
    {
        return _repo.ReadNonMessagesOfUser(userId);
    }
}