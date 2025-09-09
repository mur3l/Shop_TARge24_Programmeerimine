namespace ShopTARge24.Models.Spaceships
{
    public class SpaceshipCreateViewModel
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public string? Classification { get; set; }
        public DateTime? BuiltDate { get; set; }
        public int? Crew { get; set; }
        public int? EnginePower { get; set; }

    }
}
