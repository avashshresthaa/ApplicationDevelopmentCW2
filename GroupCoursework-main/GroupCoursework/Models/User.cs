﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace GroupCoursework.Models
{
    public class User

    {
        [Key]
        public int UserNumber { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "User type is required")]
        public string UserType { get; set; }
        public string UserPassword { get; set; }
    }
}
