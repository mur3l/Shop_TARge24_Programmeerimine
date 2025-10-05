using ShopTARge24.Models.Spaceships;

namespace ShopTARge24.Models.RealEstates
{
    public class RealEstateDeleteViewModel
    {
        public Guid? Id { get; set; }
        public double? Area { get; set; }
        public string? Location { get; set; }
        public int? RoomNumber { get; set; }
        public string? BuildingType { get; set; }

        //ImageViewModel
        public List<IFormFile> Files { get; set; }
        public List<ImageViewModel> ImageViewModels { get; set; } = new();
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}