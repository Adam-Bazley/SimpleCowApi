namespace SimpleCowApi.Data.Models
{
    public class Cow
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int Age { get; set; }
        public int? FarmId { get; set; } // Nullable FarmId
        public Farm? Farm { get; set; }  // Navigation Property for Farm
    }
}
