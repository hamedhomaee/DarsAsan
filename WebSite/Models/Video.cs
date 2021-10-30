using System;
using System.ComponentModel.DataAnnotations;

namespace DarsAsan.Models
{
    public class Video
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Path { get; set; }
    }
}