using Forum.UI.CA;
using Forum.BL;
using Forum.DAL.EF;
using Microsoft.EntityFrameworkCore;

DbContextOptionsBuilder<UserDbContext> optionsBuilder = new DbContextOptionsBuilder<UserDbContext>();
UserDbContext context = new UserDbContext(optionsBuilder.Options);
Repository repo = new Repository(context);
IManager mgr = new Manager(repo);

if (context.CreateDatabase(dropDatabase: true))
    DataSeeder.Seed(context);

ConsoleUi consoleUi = new ConsoleUi(mgr);
consoleUi.Run();


