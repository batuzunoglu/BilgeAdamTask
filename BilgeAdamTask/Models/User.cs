using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BilgeAdamTask.Models
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        [Required(ErrorMessage ="Email Adress can not be null")]
        [EmailAddress(ErrorMessage ="Invalid Email Adress")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Password can not be null")]
        [MinLength(6,ErrorMessage ="Password can be minimum 6 characters")]
        [MaxLength(32,ErrorMessage ="Password can be maximum 32 characters")]
        public string Password { get; set; }
        public ICollection<Post> Posts { get; set; }
        public User()
        {
            Posts = new HashSet<Post>();
        }

    }
}
