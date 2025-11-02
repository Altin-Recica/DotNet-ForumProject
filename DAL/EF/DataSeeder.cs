namespace Forum.DAL.EF;
using BL.Domain;

public static class DataSeeder
{
    public static void Seed(UserDbContext context)
    {
        User pieter = new User("Pieter", new DateOnly(1990, 1, 15), 34);
        User anna = new User("Anna", new DateOnly(1985, 5, 10), 39);
        User jan = new User("Jan", new DateOnly(1995, 9, 20), 28);
        User maria = new User("Maria", new DateOnly(1980, 3, 5), 44);
        User daan = new User("Daan", new DateOnly(1990, 1, 15), 34);
        User sophie = new User("Sophie", new DateOnly(1985, 5, 10), 39);
        User bram = new User("Bram", new DateOnly(1995, 9, 20), 28);
        User lotte = new User("Lotte", new DateOnly(1980, 3, 5), 44);
        
        Message hallo = new Message( "Hallo, hoe gaat het?", MessageType.Text, new DateOnly(2019, 5, 2));
        Message goed = new Message("Goed, bedankt!", MessageType.Text, new DateOnly(2019, 2, 4));
        Message leuk = new Message("Leuk je te ontmoeten.", MessageType.Text, new DateOnly(2019, 3, 5));
        Message nieuw = new Message("Wat is er nieuw?", MessageType.Text, new DateOnly(2019, 6, 16));
        Message link1 = new Message("www.link1.com", MessageType.Link, new DateOnly(2019, 3, 11));
        Message link2 = new Message("www.link2.com", MessageType.Link, new DateOnly(2019, 9, 9));
        Message link3 = new Message("www.link3.com", MessageType.Link, new DateOnly(2019, 7, 13));
        Message link4 = new Message( "www.link4.com", MessageType.Link, new DateOnly(2019, 6, 7));

        Comment comment1 = new Comment("Goed!",10,11);
        Comment comment2 = new Comment("Top",20,12);
        Comment comment3 = new Comment("Aangenaam",110,13);
        Comment comment4 = new Comment("Niks",40,13);
        Comment comment5 = new Comment("Mooie website!",50,11);
        Comment comment6 = new Comment("Bedankt",30,11);
        Comment comment7 = new Comment("Dat is de juiste website",110,12);
        Comment comment8 = new Comment("Top, bedankt!",120,11);
        
        UserMessage pieterHallo = new UserMessage
        {
            User = pieter,
            Message = hallo,
            InteractionDate = new DateOnly(2022, 1, 15)
        };

        UserMessage annaGoed = new UserMessage
        {
            User = anna,
            Message = goed,
            InteractionDate = new DateOnly(2022, 1, 16)
        };

        UserMessage janLeuk = new UserMessage
        {
            User = jan,
            Message = leuk,
            InteractionDate = new DateOnly(2022, 1, 17)
        };

        UserMessage mariaNieuw = new UserMessage
        {
            User = maria,
            Message = nieuw,
            InteractionDate = new DateOnly(2022, 1, 18)
        };

        UserMessage daanLink1 = new UserMessage
        {
            User = daan,
            Message = link1,
            InteractionDate = new DateOnly(2022, 1, 19)
        };

        UserMessage sophieLink2 = new UserMessage
        {
            User = sophie,
            Message = link2,
            InteractionDate = new DateOnly(2022, 1, 20)
        };

        UserMessage bramLink3 = new UserMessage
        {
            User = bram,
            Message = link3,
            InteractionDate = new DateOnly(2022, 1, 21)
        };

        UserMessage lotteLink4 = new UserMessage
        {
            User = lotte,
            Message = link4,
            InteractionDate = new DateOnly(2022, 1, 22)
        };
        
        comment1.ParentMessage = hallo;
        comment2.ParentMessage = goed;
        comment3.ParentMessage = leuk;
        comment4.ParentMessage = nieuw;
        comment5.ParentMessage = link1;
        comment6.ParentMessage = link2;
        comment7.ParentMessage = link3;
        comment8.ParentMessage = link4;
        
        hallo.Comments.Add(comment1);
        goed.Comments.Add(comment2);
        leuk.Comments.Add(comment3);
        nieuw.Comments.Add(comment4);
        link1.Comments.Add(comment5);
        link2.Comments.Add(comment6);
        link3.Comments.Add(comment7);
        link4.Comments.Add(comment8);
        
        context.Users.Add(pieter);
        context.Users.Add(anna);
        context.Users.Add(jan);
        context.Users.Add(maria);
        context.Users.Add(daan);
        context.Users.Add(sophie);
        context.Users.Add(bram);
        context.Users.Add(lotte);
        
        context.Messages.Add(hallo);
        context.Messages.Add(goed);
        context.Messages.Add(leuk);
        context.Messages.Add(nieuw);
        context.Messages.Add(link1);
        context.Messages.Add(link2);
        context.Messages.Add(link3);
        context.Messages.Add(link4);

        context.Comments.Add(comment1);
        context.Comments.Add(comment2);
        context.Comments.Add(comment3);
        context.Comments.Add(comment4);
        context.Comments.Add(comment5);
        context.Comments.Add(comment6);
        context.Comments.Add(comment7);
        context.Comments.Add(comment8);

        context.UserMessages.Add(pieterHallo);
        context.UserMessages.Add(annaGoed);
        context.UserMessages.Add(janLeuk);
        context.UserMessages.Add(mariaNieuw);
        context.UserMessages.Add(daanLink1);
        context.UserMessages.Add(sophieLink2);
        context.UserMessages.Add(bramLink3);
        context.UserMessages.Add(lotteLink4);

        context.SaveChanges();
        context.ChangeTracker.Clear();
    }
}