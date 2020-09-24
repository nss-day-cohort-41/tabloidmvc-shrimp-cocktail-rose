using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TabloidMVC.Models.ViewModels
{
    public class PostTagMgmtViewModel
    {
        public Post Post { get; set; }

        public List<Tag> PostTags { get; set; }

        public List<Tag> AllTags { get; set; }
    }
}
