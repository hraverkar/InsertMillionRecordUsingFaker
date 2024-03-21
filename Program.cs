using Bogus;
using InsertMillionRecords;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

// initialize data context
var connectionString = "Server=127.0.0.1; Database=Weather; Integrated Security=False; User Id = SA; Password=Admin1234!; MultipleActiveResultSets=False;TrustServerCertificate=True";
var contextOptionsBuilder = new DbContextOptionsBuilder<DataContext>();
contextOptionsBuilder.UseSqlServer(connectionString);
var context = new DataContext(contextOptionsBuilder.Options);

// create database
await context.Database.EnsureDeletedAsync();
await context.Database.EnsureCreatedAsync();

// setup bogus faker
var faker = new Faker<TaskDetails>();
faker.RuleFor(p => p.Id, f => Guid.NewGuid());
faker.RuleFor(p => p.TaskTitle, f => f.Commerce.ProductName());
faker.RuleFor(p => p.TaskDetail, f => f.Commerce.Categories(1)[0]);
faker.RuleFor(p => p.TaskAssignTo, f => f.Person.FullName);
faker.RuleFor(p => p.TaskStatus, f => "Open");
faker.RuleFor(p => p.TaskCreatedAt, f => f.Date.PastOffset(60, DateTime.Now.AddYears(-18)).Date);
faker.RuleFor(p => p.TaskCreatedBy, f => f.Internet.Email());
faker.RuleFor(p => p.IsDeleted, f => false);

// generate 1 million products
var products = faker.Generate(1_000_00);

var batches = products
    .Select((p, i) => (Product: p, Index: i))
    .GroupBy(x => x.Index / 100_000)
    .Select(g => g.Select(x => x.Product).ToList())
    .ToList();

// insert batches
var stopwatch = new Stopwatch();
stopwatch.Start();

var count = 0;
foreach (var batch in batches)
{
    count++;
    Console.WriteLine($"Inserting batch {count} of {batches.Count}...");

    await context.Products.AddRangeAsync(batch);
    await context.SaveChangesAsync();
}

stopwatch.Stop();

Console.WriteLine($"Elapsed time: {stopwatch.Elapsed}");
Console.WriteLine("Press any key to exit...");