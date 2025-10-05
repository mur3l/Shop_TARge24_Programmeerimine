using Microsoft.AspNetCore.Mvc;
using ShopTARge24.Models.Spaceships;

namespace ShopTARge24.Models.RealEstates
{
    public class RealEstateCreateUpdateViewModel
    {
        public Guid? Id { get; set; }
        public double? Area { get; set; }
        public string? Location { get; set; }
        public int? RoomNumber { get; set; }
        public string? BuildingType { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }

        public List<IFormFile> Files { get; set; }
        public List<ImageViewModel> Images { get; set; }
            = new List<ImageViewModel>();
        public object ImageViewModels { get; internal set; }
    }
}