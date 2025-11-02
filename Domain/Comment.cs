namespace Forum.BL.Domain;

public class Comment //Reactie (anoniem)
{
    public int Id { get; set; }
    
    public string Text { get; set; }
    public int Upvotes { get; set; }
    public int Downvotes { get; set; }
    
    
    public Message ParentMessage { get; set; }
    
    public Comment() { }
    
    public Comment(string text, int upvotes, int downvotes)
    {
        Text = text;
        Upvotes = upvotes;
        Downvotes = downvotes;
    }
    
    public override string ToString()
    {
        return $"'{Text}' ( Upvotes: {Upvotes}, Downvotes: {Downvotes} )";
    }
}

