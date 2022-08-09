namespace Wpf_MVVM.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public int PositionId { get; set; } 
        public Position Position { get; set; } 

    }
}
