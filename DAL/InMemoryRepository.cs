using Forum.BL.Domain;

namespace Forum.DAL;

public class InMemoryRepository : IRepository
{
    private readonly List<User> _users = new();
    private readonly List<Message> _messages = new();
    private readonly List<Comment> _comments = new();

    public User ReadUser(int id)
    {
        return _users.FirstOrDefault(u => u.Id == id);
    }

    public IEnumerable<User> ReadUsers()
    {
        return _users;
    }

    public IEnumerable<User> ReadUsersByNameAndDateOfBirth(string name, DateOnly? dateOfBirth)
    {
        return _users.Where(user =>
            (string.IsNullOrEmpty(name) || user.Name.Contains(name)) &&
            (dateOfBirth.HasValue) || user.DateOfBirth == dateOfBirth);
    }


    public void CreateUser(User user)
    {
        _users.Add(user);
    }

    public Message ReadMessage(int id)
    {
        return _messages.FirstOrDefault(m => m.Id == id);
    }

    public IEnumerable<Message> ReadMessages()
    {
        return _messages;
    }

    public IEnumerable<Message> ReadMessagesByMessageType(MessageType messageType)
    {
        return _messages
            .Where(message => message.Type == messageType)
            .ToList();
    }

    public void CreateMessage(Message message)
    {
        _messages.Add(message);
    }

    public Comment ReadComment(int id)
    {
        return _comments.FirstOrDefault(m => m.Id == id);
    }

    public IEnumerable<Comment> ReadComments()
    {
        return _comments;
    }

    public void CreateComment(Comment comment)
    {
        _comments.Add(comment);
    }

    public IEnumerable<User> ReadAllUsersWithMessages()
    {
        throw new NotImplementedException();
    }
    public IEnumerable<Message> ReadAllMessagesWithComments()
    {
        throw new NotImplementedException();
    }

    public void CreateUserMessage(UserMessage userMessage)
    {
        throw new NotImplementedException();
    }

    public void DeleteUserMessage(int userId, int messageId)
    {
        throw new NotImplementedException();
    }
    
    public User ReadUserWithMessage(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Message> ReadMessagesOfUser(int userId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Message> ReadNonMessagesOfUser(int userId)
    {
        throw new NotImplementedException();
    }
}