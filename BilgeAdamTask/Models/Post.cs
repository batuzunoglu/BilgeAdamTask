using System;
using System.ComponentModel.DataAnnotations;

namespace BilgeAdamTask.Models
{
    public class Post : BaseEntity
    {
        [Required(ErrorMessage ="Title can not be null")]
        public string Title { get; set; }
        [Required(ErrorMessage ="Content can not be null")]
        public string Content { get; set; }
        public DateTime PublishDate { get; set; }
        public string ImageUrl { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }


    }
}
