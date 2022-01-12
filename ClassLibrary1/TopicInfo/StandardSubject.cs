using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class StandardSubject
    {
        public StandardSubject()
        {
            topics = new List<Topic>();
        }
        
        public int Id { get; set; }
        public int standardId { get; set; }
        public int subjectId { get; set; }

        public Standard standard { get; set; }
        public Subject subject { get; set; }
        public List<Topic> topics { get; set; }

    }
}
