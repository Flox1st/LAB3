namespace WebApplication1.Models
{
    public class dog
    {
        public int? id { get; set; }

        public string? name { get; set; }
        public int? OwnerId { get; set; }
        public Owner? Owner { get; set; } 
    }
}
