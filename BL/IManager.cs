namespace Forum.BL;
using Domain;

public interface IManager
{
    User GetUser(int id);
    IEnumerable<User> GetUsers();
    IEnumerable<User> GetUsersByNameAndDateOfBirth(string name, DateOnly? dateOfBirth);
    
    Message GetMessage(int id);
    IEnumerable<Message> GetMessages();
    IEnumerable<Message> GetMessagesByMessageType(MessageType messageType);
    
    IEnumerable<Comment> GetComments();
    Comment AddComment(string text, int upvotes, int downvotes);
    
    User AddUser(string name, DateOnly dateOfBirth, int? age);
    Message AddMessage(string content, MessageType type, DateOnly messageDate);
    
    public User GetUserWithMessage(int id);
    IEnumerable<Message> GetAllMessagesWithComments();
    
    UserMessage AddUserMessage(User user, Message message, DateOnly dateTime);
    void RemoveUserMessage(int userId, int messageId);
    IEnumerable<User> GetAllUsersWithMessages();
    IEnumerable<Message> GetUserMessages(int userId);
    IEnumerable<Message> GetNonUserMessages(int id);
}