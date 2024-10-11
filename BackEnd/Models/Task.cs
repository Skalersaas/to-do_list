namespace To_Do_List.Models
{
    public class Task(string title, int UserId, int GroupId)
    {
        public int Id { get; set; }
        public int UserId { get; set; } = UserId;
        public User User { get; set; }
        public int GroupId { get; set; } = GroupId;
        public Group Group { get; set; }
        public string Title { get; set; } = title;
        public DateTime CreatedOn { get; set; } = DateTime.Now.ToUniversalTime();
        public bool IsCompleted { get; set; }
        public DateTime CompletedOn { get; set; }
        public DateTime DueTo { get; set; }
        public RepeatType RepeatType { get; set; }
        public bool MyDay { get; set; }
    }
    public enum RepeatType
    {
        None,
        Daily,
        Weekly,
        Monthly,
        Yearly
    }
    public class TaskDTO(Task task)
    {
        public int Id { get; set; } = task.Id;
        public int UserId { get; set; } = task.UserId;
        public int GroupId { get; set; } = task.GroupId;
        public string Title { get; set; } = task.Title;
        public DateTime CreatedOn { get; set; } = task.CreatedOn;
        public bool IsCompleted { get; set; } = task.IsCompleted;
        public DateTime CompletedOn { get; set; } = task.CompletedOn;
        public DateTime DueTo { get; set; } = task.DueTo;
        public RepeatType RepeatType { get; set; } = task.RepeatType;
        public bool MyDay { get; set; } = task.MyDay;

    }
    public class TaskPatch
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime DueTo { get; set; }
        public RepeatType RepeatType { get; set; }
        public bool MyDay { get; set; }
    }
}