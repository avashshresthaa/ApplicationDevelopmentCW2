﻿using System.ComponentModel.DataAnnotations;

namespace GroupCoursework.Models
{
    public class DVDCategory
    {
        [Key]
        public int CategoryNumber { get; set; }
        [StringLength(100, MinimumLength = 10)]
        public string CategoryDescription { get; set; }
        [Required(ErrorMessage = "Restriction is required")]
        public Boolean AgeRestricted { get; set; }
    }
}
