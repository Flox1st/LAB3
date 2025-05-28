using Humanizer.Localisation;

namespace WebApplication1.Models
{
    public class dogAndColor
    {
        public int Id { get; set; }
        public int? ColorId { get; set; }
        public Color? Color { get; set; }
        public int? dogId { get; set; }
        public dog? dog { get; set; }
    }
}
