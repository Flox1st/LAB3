namespace WebApplication1.Models
{
    public class Owner
    {
        public int? Id { get; set; }
        public string? name { get; set; }
        public List<dog>? dogs { get; set; } = new List<dog>();
    }
}
