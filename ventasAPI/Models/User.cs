using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ventasAPI.Models
{
    public abstract class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public long Document { get; set; }
        [Required]
        public Gender Gender { get; set; }
        [Required]
        public DateTime Birthday { get; set; }
        [Required]
        [EmailAddress]
        [MaxLength(80)]
        public string Email { get; set; }
        [Required]
        public string ImageURL { get; set; }
    }
}