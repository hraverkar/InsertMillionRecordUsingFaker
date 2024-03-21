namespace InsertMillionRecords;

public class TaskDetails
{
    public Guid Id { get; set; }
    public string TaskTitle { get; set; }
    public string TaskDetail { get; set; }
    public string TaskAssignTo { get; set; }
    public string TaskStatus { get; set; }
    public DateTime TaskCreatedAt { get; set; } = DateTime.Now;
    public string TaskCreatedBy { get; set; }
    public bool IsDeleted { get; set; } = false;
}
