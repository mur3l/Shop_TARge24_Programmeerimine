using Microsoft.AspNetCore.Http;
using ShopTARge24.Core.Dto;
using System.Diagnostics.Tracing;

namespace ShopTARge24.Core.Dto
{
        public class KindergartenDto
        {
        public Guid? Id { get; set; }
        public string Nr { get; set; }
        public string GroupName { get; set; }
        public int ChildrenCount { get; set; }
        public string KindergartenName { get; set; }
        public string TeacherName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public IList<IFormFile> Files { get; set; }
        public FileToApiDto[]? fileToApiDtos { get; set; }
    }
}
