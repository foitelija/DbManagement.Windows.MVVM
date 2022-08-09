namespace Wpf_MVVM.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<Position> Positions { get; set; }
    }
}
