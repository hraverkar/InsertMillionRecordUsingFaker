namespace InsertMillionRecords;


public class User
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }

    // Navigation property
    public ICollection<Project> Projects { get; set; }
}


public class Project
{
    public Guid Id { get; set; }
    public string ProjectName { get; set; }
    public Guid UserId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    // Navigation properties
    public User User { get; set; }
    public ICollection<TaskDetails> Tasks { get; set; }
}


public class TaskDetails
{
    public Guid Id { get; set; }
    public string TaskTitle { get; set; }
    public string TaskDetail { get; set; }
    public string TaskAssignTo { get; set; }
    public string TaskStatus { get; set; }
    public DateTime TaskCreatedAt { get; set; }
    public string TaskCreatedBy { get; set; }
    public bool IsDeleted { get; set; }
    public Guid ProjectId { get; set; }

    // Navigation properties
    public Project Project { get; set; }
    public ICollection<TaskComments> Comments { get; set; }
    public ICollection<TaskAttachments> Attachments { get; set; }
    public ICollection<TaskHistory> Histories { get; set; }
    public ICollection<TaskTags> Tags { get; set; }
}


public class TaskComments
{
    public Guid Id { get; set; }
    public string Comment { get; set; }
    public string CommentedBy { get; set; }
    public DateTime CommentedAt { get; set; }
    public Guid TaskId { get; set; }

    // Navigation property
    public TaskDetails Task { get; set; }
}


public class TaskAttachments
{
    public Guid Id { get; set; }
    public string FileName { get; set; }
    public string FileType { get; set; }
    public Guid TaskId { get; set; }

    // Navigation property
    public TaskDetails Task { get; set; }
}


public class TaskHistory
{
    public Guid Id { get; set; }
    public string Action { get; set; }
    public DateTime ActionDate { get; set; }
    public Guid TaskId { get; set; }

    // Navigation property
    public TaskDetails Task { get; set; }
}


public class TaskTags
{
    public Guid Id { get; set; }
    public string Tag { get; set; }
    public Guid TaskId { get; set; }

    // Navigation property
    public TaskDetails Task { get; set; }
}

