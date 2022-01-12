using Data;
using Domain;
using System;
using System.Linq;

namespace ConsoleApplication
{
    class Program
    {
        private static TopicContext __context = new TopicContext();
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            __context.Database.EnsureCreated();

            RemoveStandard();

            Console.WriteLine("Press any key");
            Console.ReadKey();
        }

        private static void badInputForStandardSubject()
        {
            var subject1 = __context.Subjects.Find(1);
            
            var standardSubject1 = new StandardSubject();
            standardSubject1.standardId = 1;
            
            subject1.standardSubjects.Add(standardSubject1);
            __context.SaveChanges();
        }

        private static void AddStandard()
        {
            var standard = new Standard();
            Console.WriteLine($"The Id before {standard.Id}");
            __context.Standards.Add(standard);
            Console.WriteLine($"The Id after {standard.Id}");
            __context.SaveChanges();
        }
        private static void AddSubject()
        {
            var subject = new Subject();
            var standardSubject1 = new StandardSubject();
            standardSubject1.standardId = 1;

            var standardSubject2 = new StandardSubject();
            standardSubject2.standardId = 2;

            subject.standardSubjects.Add(standardSubject1);
            subject.standardSubjects.Add(standardSubject2);

            __context.Subjects.Add(subject);
            __context.SaveChanges();
        }
        private static void AddTopic()
        {
            var topic = new Topic();
            var standardId = 1;
            var subjectId = 1;
            var standardSubjectObj = __context.StandardSubjects.Where(s => s.standardId == standardId && s.subjectId == subjectId).FirstOrDefault();
            topic.standardSubjectId = standardSubjectObj.Id;
            __context.Topics.Add(topic);
            __context.SaveChanges();
        }

        private static void GetStandards()
        {
            var standardsList = __context.Standards.ToList();
            foreach (var standard in standardsList)
            {
                Console.WriteLine($"The id of the standard is {standard.Id}");
            }
        }
        private static void GetSubjects()
        {
            var subjectsList = __context.Subjects.ToList();
            foreach (var subject in subjectsList)
            {
                Console.WriteLine($"The id of the subject is {subject.Id}");
            }
        }
        private static void GetTopics()
        {
            var topicsList = __context.Topics.ToList();
            foreach (var topic in topicsList)
            {
                Console.WriteLine($"The id of the topic is {topic.Id}");
            }
        }

        private static void RemoveStandard()
        {
            var standard = __context.Standards.Find(1);
            __context.Standards.Remove(standard);
            __context.SaveChanges();
        }
    }
}
