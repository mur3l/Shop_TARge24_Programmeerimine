﻿namespace ShopTARge24.Models.Kindergarten
{
    public class KindergartenDeleteViewModel
    {
        public Guid? Id { get; set; }
        public string Nr { get; set; }
        public string GroupName { get; set; }
        public int ChildrenCount { get; set; }
        public string KindergartenName { get; set; }
        public string TeacherName { get; set; }

        public DateTime? CreatedAt { get; set; }
            public DateTime? ModifiedAt { get; set; }
    }
}
