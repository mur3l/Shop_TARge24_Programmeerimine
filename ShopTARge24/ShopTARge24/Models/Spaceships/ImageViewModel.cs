namespace ShopTARge24.Models.Spaceships
{
    public class ImageViewModel
    {
        public Guid ImageId { get; set; }
        public Guid Id { get; set; }
        public string? Filepath { get; set; }
        public Guid? SpaceshipId { get; set; }
        public string? ImageTitle { get; set; }
        public byte[]? ImageData { get; set; }
        public string? Image { get; set; }
    }
}