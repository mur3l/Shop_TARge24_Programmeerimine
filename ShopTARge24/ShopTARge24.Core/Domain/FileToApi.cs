namespace ShopTARge24.Core.Domain
{
    public class FileToApi
    {
        public Guid Id { get; set; }
        public string? ExistingFilePath { get; set; }
        public Guid? SpaceshipId { get; set; }
        public Guid? RealEstateId { get; set; }
        public byte[] ImageData { get; set; }
        public string ImageTitle { get; set; }
    }
}