using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebApplication1.Domain
{
    public class Standard
    {
        public Standard()
        {
            standardSubjects = new List<StandardSubject>();
        }
        public int Id { get; set; }
        public int standardName { get; set; }

        public List<StandardSubject> standardSubjects { get; set; }
    }
}
