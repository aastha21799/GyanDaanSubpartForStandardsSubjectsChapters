using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class ForChapter
    {
        public int Id { get; set; }
        public string chapterName { get; set; }
        public int level { get; set; }
        public List<SelectListItem> standardsDrp { get; set; }
        public int standardId { get; set; }
        public List<SelectListItem> subjectsDrp { get; set; }
        public int subjectId { get; set; }
    }
}
