using Bogus;
using InsertMillionRecords;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

// initialize data context
var connectionString = "Server=127.0.0.1; Database=ProjectDetailsDB; Integrated Security=False; User Id = SA; Password=Admin1234!; MultipleActiveResultSets=False;TrustServerCertificate=True";
var contextOptionsBuilder = new DbContextOptionsBuilder<DataContext>();
contextOptionsBuilder.UseSqlServer(connectionString);
var context = new DataContext(contextOptionsBuilder.Options);


// create database
await context.Database.EnsureDeletedAsync();
await context.Database.EnsureCreatedAsync();

//// setup bogus faker
//var faker = new Faker<TaskDetails>();
//faker.RuleFor(p => p.Id, f => Guid.NewGuid());
//faker.RuleFor(p => p.TaskTitle, f => f.Commerce.ProductName());
//faker.RuleFor(p => p.TaskDetail, f => f.Commerce.Categories(1)[0]);
//faker.RuleFor(p => p.TaskAssignTo, f => f.Person.FullName);
//faker.RuleFor(p => p.TaskStatus, f => "Open");
//faker.RuleFor(p => p.TaskCreatedAt, f => f.Date.PastOffset(60, DateTime.Now.AddYears(-18)).Date);
//faker.RuleFor(p => p.TaskCreatedBy, f => f.Internet.Email());
//faker.RuleFor(p => p.IsDeleted, f => false);

var faker = new Faker();

// Generate Users
var userFaker = new Faker<User>()
    .RuleFor(p => p.Id, f => Guid.NewGuid())    
    .RuleFor(p => p.FirstName, f => f.Person.FirstName)
    .RuleFor(p => p.LastName, f => f.Person.LastName)
    .RuleFor(p => p.FullName, f => f.Person.FirstName+" "+f.Person.LastName)
    .RuleFor(p => p.Email, f => f.Person.FirstName + "_" + f.Person.LastName + "@gmail.com");

var users = userFaker.Generate(10000);
await context.Users.AddRangeAsync(users);
await context.SaveChangesAsync();


// Generate Projects
var projectFaker = new Faker<Project>()
    .RuleFor(p => p.Id, f => Guid.NewGuid())
    .RuleFor(p => p.ProjectName, f => f.Company.CompanyName())
    .RuleFor(p => p.UserId, f => f.PickRandom(users).Id)
    .RuleFor(p => p.StartDate, f => f.Date.Past(2))
    .RuleFor(p => p.EndDate, f => f.Date.Future(1));

var projects = projectFaker.Generate(3000);
await context.Projects.AddRangeAsync(projects);
await context.SaveChangesAsync();

// Generate TaskDetails
var taskFaker = new Faker<TaskDetails>()
    .RuleFor(p => p.Id, f => Guid.NewGuid())
    .RuleFor(p => p.TaskTitle, f => f.Commerce.ProductName())
    .RuleFor(p => p.TaskDetail, f => f.Commerce.Categories(1)[0])
    .RuleFor(p => p.TaskAssignTo, f => f.Person.FullName)
    .RuleFor(p => p.TaskStatus, f => "Open")
    .RuleFor(p => p.TaskCreatedAt, f => f.Date.PastOffset(60, DateTime.Now.AddYears(-18)).Date)
    .RuleFor(p => p.TaskCreatedBy, f => f.Internet.Email())
    .RuleFor(p => p.IsDeleted, f => false)
    .RuleFor(p => p.ProjectId, f => f.PickRandom(projects).Id);

var tasks = taskFaker.Generate(7000);

await context.TaskDetails.AddRangeAsync(tasks);
await context.SaveChangesAsync();

// Generate TaskComments
var commentFaker = new Faker<TaskComments>()
    .RuleFor(p => p.Id, f => Guid.NewGuid())
    .RuleFor(p => p.Comment, f => f.Lorem.Sentence())
    .RuleFor(p => p.CommentedBy, f => f.Person.FullName)
    .RuleFor(p => p.CommentedAt, f => f.Date.Recent())
    .RuleFor(p => p.TaskId, f => f.PickRandom(tasks).Id);

var comments = commentFaker.Generate(5000);


await context.TaskComments.AddRangeAsync(comments);
await context.SaveChangesAsync();

// Generate TaskAttachments
var attachmentFaker = new Faker<TaskAttachments>()
    .RuleFor(p => p.Id, f => Guid.NewGuid())
    .RuleFor(p => p.FileName, f => f.System.FileName())
    .RuleFor(p => p.FileType, f => f.System.FileExt())
    .RuleFor(p => p.TaskId, f => f.PickRandom(tasks).Id);

var attachments = attachmentFaker.Generate(4000);
await context.TaskAttachments.AddRangeAsync(attachments);
await context.SaveChangesAsync();

// Generate TaskHistory
var historyFaker = new Faker<TaskHistory>()
    .RuleFor(p => p.Id, f => Guid.NewGuid())
    .RuleFor(p => p.Action, f => f.Lorem.Word())
    .RuleFor(p => p.ActionDate, f => f.Date.Past())
    .RuleFor(p => p.TaskId, f => f.PickRandom(tasks).Id);

var histories = historyFaker.Generate(5500);
await context.TaskHistories.AddRangeAsync(histories);
await context.SaveChangesAsync();


// Generate TaskTags
var tagFaker = new Faker<TaskTags>()
    .RuleFor(p => p.Id, f => Guid.NewGuid())
    .RuleFor(p => p.Tag, f => f.Lorem.Word())
    .RuleFor(p => p.TaskId, f => f.PickRandom(tasks).Id);

var tags = tagFaker.Generate(3000);

await context.TaskTags.AddRangeAsync(tags);
await context.SaveChangesAsync();







//// generate 1 million products
//var products = faker.Generate(1_000_00);

//var batches = products
//    .Select((p, i) => (Product: p, Index: i))
//    .GroupBy(x => x.Index / 100_000)
//    .Select(g => g.Select(x => x.Product).ToList())
//    .ToList();

//// insert batches
//var stopwatch = new Stopwatch();
//stopwatch.Start();

//var count = 0;
//foreach (var batch in batches)
//{
//    count++;
//    Console.WriteLine($"Inserting batch {count} of {batches.Count}...");

//    await context.Products.AddRangeAsync(batch);
//    await context.SaveChangesAsync();
//}

//stopwatch.Stop();

//Console.WriteLine($"Elapsed time: {stopwatch.Elapsed}");
Console.WriteLine("Press any key to exit...");