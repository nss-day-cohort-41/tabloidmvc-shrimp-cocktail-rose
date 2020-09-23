using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TabloidMVC.Models
{
    public class Post
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [DisplayName("Header Image URL")]
        public string ImageLocation { get; set; }

        public DateTime CreateDateTime { get; set; }

        [DisplayName("Published")]
        [DataType(DataType.Date)]
        public DateTime? PublishDateTime { get; set; }

        public bool IsApproved { get; set; }

        [Required]
        [DisplayName("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        [DisplayName("Author")]
        public int UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }

        public int ReadTime
        {
            get
            {
                char[] delimiters = new char[] { ' ', '\r', '\n' };
                int count = Content.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).Length;
                float minutes = count/265f;
                int minutesRounded = (int)Math.Ceiling(minutes);
                return minutesRounded;
            }
        }
    }
}
