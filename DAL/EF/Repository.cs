using Microsoft.EntityFrameworkCore;

namespace Forum.DAL.EF;
using BL.Domain;

public class Repository : IRepository
{
    private readonly UserDbContext _context;

    public Repository(UserDbContext context)
    {
        _context = context;
    }

    public User ReadUser(int id)
    {
        return _context.Users.Find(id);
    }

    public IEnumerable<User> ReadUsers()
    {
        return _context.Users;
    }

    public IEnumerable<User> ReadUsersByNameAndDateOfBirth(string name, DateOnly? dateOfBirth)
    {
        IQueryable<User> query = _context.Users.AsQueryable();

        if (!string.IsNullOrEmpty(name))
        {
            query = query.Where(user => user.Name.ToLower().Contains(name.ToLower()));
        }

        if (dateOfBirth.HasValue)
        {
            query = query.Where(user => user.DateOfBirth == dateOfBirth.Value);
        }

        return query.ToList();
    }

    public void CreateUser(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
    }

    public Message ReadMessage(int id)
    {
        return _context.Messages.Find(id);
    }

    public IEnumerable<Message> ReadMessages()
    {
        return _context.Messages;
    }

    public IEnumerable<Message> ReadMessagesByMessageType(MessageType messageType)
    {
        return _context.Messages.Where(message => message.Type == messageType);
    }

    public void CreateMessage(Message message)
    {
        _context.Messages.Add(message);
        _context.SaveChanges();
    }
    
    public Comment ReadComment(int id)
    {
        return _context.Comments.Find(id);
    }

    public IEnumerable<Comment> ReadComments()
    {
        return _context.Comments;
    }

    public void CreateComment(Comment comment)
    {
        _context.Comments.Add(comment);
        _context.SaveChanges();
    }

    
    public IEnumerable<User> ReadAllUsersWithMessages()
    {
        return _context.Users.Include(user => user.UserMessages)
            .ThenInclude(message => message.Message);
    }
    
    public User ReadUserWithMessage(int messageId)
    {
        return _context.Users
            .FirstOrDefault(user => user.UserMessages.Any(userMessage => userMessage.Message.Id == messageId));
    }

    public IEnumerable<Message> ReadAllMessagesWithComments()
    {
        return _context.Messages
        .Include(message => message.Comments);
    }
    
    public void CreateUserMessage(UserMessage userMessage)
    {
        _context.UserMessages.Add(userMessage);
        _context.SaveChanges();
    }

    public void DeleteUserMessage(int userId, int messageId)
    {
        UserMessage deleteUserMessage =
            _context.UserMessages.
                Single(um => um.User.Id == userId && um.Message.Id == messageId);
        _context.UserMessages.Remove(deleteUserMessage);
        _context.SaveChanges();
    }

    public IEnumerable<Message> ReadMessagesOfUser(int userId)
    {
        return _context.Messages
            .Where(m => m.UserMessages.Any(um => um.User.Id == userId));
    }

    public IEnumerable<Message> ReadNonMessagesOfUser(int userId)
    {
        return _context.Messages
            .Where(m => m.UserMessages.All(um => um.User.Id != userId));
    }
}