using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain
{
    public class Subject
    {
        public Subject()
        {
            standardSubjects = new List<StandardSubject>();
        }
        
        public int Id { get; set; }
        public string subjectName { get; set; }

        public List<StandardSubject> standardSubjects { get; set; }
    }
}
