namespace ShopTARge24.Views.Spaceships
{
    public class ImageViewModel
    {
        public Guid Id { get; set; }
        public string? FilePath { get; set; }
        public Guid? SpaceshipId { get; set; }
        public Guid? RealEstateId { get; set; }
        public Guid? KindergartenId { get; set; }
        public string? ImageTitle { get; set; }
        public byte[]? ImageData { get; set; }
        public string? Image { get; set; }

    }
}
