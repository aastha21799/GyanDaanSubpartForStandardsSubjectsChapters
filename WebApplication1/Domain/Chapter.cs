using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebApplication1.Domain
{
    public class Chapter
    {
        public int Id { get; set; }
        public string chapterName { get; set; }
        public int level { get; set; }
        public int standardSubjectId { get; set; }

        public StandardSubject standardSubject { get; set; }
    }
}
