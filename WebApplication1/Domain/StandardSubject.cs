using System;
using System.Collections.Generic;
using System.Text;

namespace WebApplication1.Domain
{
    public class StandardSubject
    {
        public StandardSubject()
        {
            chapters = new List<Chapter>();
        }
        
        public int Id { get; set; }
        public int standardId { get; set; }
        public int subjectId { get; set; }

        public Standard standard { get; set; }
        public Subject subject { get; set; }
        public List<Chapter> chapters { get; set; }

    }
}
