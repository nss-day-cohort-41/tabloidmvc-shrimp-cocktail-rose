using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TabloidMVC.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [Required]
       
        public int PostId { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]

        public string Content { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayName("Date")]
        public DateTime CreateDateTime { get; set; }


       
        public int UserProfileId { get; set; }
        public UserProfile User { get; set; }

    }
}
