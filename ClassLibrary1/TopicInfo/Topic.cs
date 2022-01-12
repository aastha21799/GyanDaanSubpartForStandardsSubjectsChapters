using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain
{
    public class Topic
    {
        public int Id { get; set; }
        public string topicName { get; set; }
        public int level { get; set; }
        public int standardSubjectId { get; set; }

        public StandardSubject standardSubject { get; set; }
    }
}
