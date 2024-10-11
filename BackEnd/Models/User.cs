namespace To_Do_List.Models
{
    public class User(string login, string fullName, string password)
    {
        public int Id { get; set; }
        public string Login { get; set; } = login;
        public string FullName { get; set; } = fullName;
        public string Password { get; set; } = password;
        public virtual List<Task> Tasks { get; set; } = [];
        public List<Group> Groups { get; set; } = [];
    }
}
