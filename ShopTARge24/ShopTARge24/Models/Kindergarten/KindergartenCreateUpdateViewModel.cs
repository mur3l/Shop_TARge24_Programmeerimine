using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using ShopTARge24.Models.Spaceships;

namespace ShopTARge24.Models.Kindergarten
{
    public class KindergartenCreateUpdateViewModel
    {
        //CRUD TESTI JAOKS
        [Range(0, int.MaxValue, ErrorMessage = "Please enter numbers only!")]
        public int ChildrenCount { get; set; }


        public Guid? Id { get; set; }
        public string? Nr { get; set; }
        public string? GroupName { get; set; }
        public string? KindergartenName { get; set; }
        public string? TeacherName { get; set; }
        public DateTime? CreatedAt { get; internal set; }
        public DateTime? ModifiedAt { get; internal set; }

        public IList<IFormFile>? Files { get; set; }
        public IEnumerable<ImageViewModel>? Images { get; set; }

    }
}
