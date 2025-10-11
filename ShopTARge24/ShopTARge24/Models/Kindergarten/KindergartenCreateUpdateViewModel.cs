using Microsoft.AspNetCore.Http;
using ShopTARge24.Models.Spaceships;
using System;
using System.Collections.Generic;

namespace ShopTARge24.Models.Kindergarten
{
    public class KindergartenCreateUpdateViewModel
    {
        public Guid? Id { get; set; }
        public string Nr { get; set; }
        public string GroupName { get; set; }
        public int ChildrenCount { get; set; }
        public string KindergartenName { get; set; }
        public string TeacherName { get; set; }
        public DateTime? CreatedAt { get; internal set; }
        public DateTime? ModifiedAt { get; internal set; }

        public IList<IFormFile> Files { get; set; }
        public List<ImageViewModel> Images { get; set; } = new();
    }
}
