using Microsoft.EntityFrameworkCore;

namespace InsertMillionRecords;
public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<TaskDetails> Products { get; set; }
}
