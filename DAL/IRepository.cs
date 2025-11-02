namespace Forum.DAL;
using BL.Domain;

public interface IRepository
{
    User ReadUser(int id);
    IEnumerable<User> ReadUsers();
    IEnumerable<User> ReadUsersByNameAndDateOfBirth(string name, DateOnly? dateOfBirth);
    void CreateUser(User user);

    Message ReadMessage(int id);
    IEnumerable<Message> ReadMessages();
    IEnumerable<Message> ReadMessagesByMessageType(MessageType messageType);
    void CreateMessage(Message message);
    
    Comment ReadComment(int id);
    IEnumerable<Comment> ReadComments();
    void CreateComment(Comment comment);

    IEnumerable<User> ReadAllUsersWithMessages();
    public User ReadUserWithMessage(int id);
    IEnumerable<Message> ReadAllMessagesWithComments();
    
    void CreateUserMessage(UserMessage userMessage);
    void DeleteUserMessage(int userId, int messageId);
    IEnumerable<Message> ReadMessagesOfUser(int userId);
    IEnumerable<Message> ReadNonMessagesOfUser(int userId);
}