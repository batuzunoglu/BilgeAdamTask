using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BilgeAdamTask.Models
{
    public class Category : BaseEntity
    {
        [Required(ErrorMessage ="Category name can not be null")]
        public string Name { get; set; }
        public ICollection<Post> Posts { get; set; }
        public Category()
        {
            Posts = new HashSet<Post>();
        }
    }
}
