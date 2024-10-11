
namespace To_Do_List.Models
{
    public class Group(string name, int userId)
    {
        public int Id { get; set; }
        public string Name { get; set; } = name;
        public int UserId { get; set; } = userId;
        public User User { get; set; }
        public List<Task> Tasks { get; set; } = [];
    }
    public class GroupDTO(Group group)
    {
        public int Id { get; set; } = group.Id;
        public string Name { get; set; } = group.Name;
        public int UserId { get; set; } = group.UserId;
        public int TaskCount { get; set; } = group.Tasks.Count;
    }
}
