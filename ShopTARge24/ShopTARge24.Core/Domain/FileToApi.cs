namespace ShopTARge24.Core.Domain
{
    public class FileToApi
    {
        public Guid Id { get; set; }
        public string? ExistingFilePath { get; set; }
        public Guid? SpaceshipId { get; set; }
        public Guid? RealEstateId { get; set; }
        public Guid? KindergartenId { get; set; }
        public byte[] ImageData { get; set; }
        public string ImageTitle { get; set; }

        public double? Area { get; set; }
        public string? location { get; set; }
        public int RoomNumber { get; set; }
        public string? BuildingType { get; set; }
        public DateTime? CreatedAd { get; set; }
        public DateTime? UpdatedAd { get; set; }
    }
}