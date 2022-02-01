using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class ForStandard
    {
        public int Id { get; set; }
        public int standardName { get; set; }
        public List<SelectListItem> subjectsDrp { get; set; }
        public int[] subjectIds { get; set; }
    }
}
