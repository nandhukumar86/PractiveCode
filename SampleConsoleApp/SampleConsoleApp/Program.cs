using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Sample Code");

            List<Student> studentsList1 = new List<Student>()
            {
                new Student(){ID = 1, StudentName = "Nandha", Grade="8th"},
                new Student(){ID = 2, StudentName = "kumar", Grade="9th"},
                new Student(){ID = 3, StudentName = "anu", Grade="10th"},
                new Student(){ID = 4, StudentName = "Prasanna", Grade="11th"}
            };

            List<Student> studentsList2 = new List<Student>()
            {
                //deleted ID 1, added ID 5.
                new Student(){ID = 2, StudentName = "kumar", Grade="12th"},
                new Student(){ID = 3, StudentName = "anu", Grade="10th"},
                new Student(){ID = 4, StudentName = "Prasanna", Grade="11th"},
                new Student(){ID = 5, StudentName = "Prasanna", Grade="11th"}
            };

            List<Student> studentsUpdates = GetUpdates(studentsList1, studentsList2);
            List<Student> studentsInserts = GetInserts(studentsList1, studentsList2);
            List<Student> studentsdeletes = GetDeletes(studentsList1, studentsList2);

            Console.WriteLine("Updates");
            DisplayStudents(studentsUpdates);

            Console.WriteLine("Insterts");
            DisplayStudents(studentsInserts);

            Console.WriteLine("Deletes");
            DisplayStudents(studentsdeletes);


        }

        private static void DisplayStudents(List<Student> students)
        {
            foreach (var item in students)
            {
                Console.WriteLine($"{item.ID}--{item.StudentName}--{item.Grade}");

            }
        }

        private static List<Student> GetDeletes(List<Student> studentsList1, List<Student> studentsList2)
        {
            var studentsDeletes = from _list1 in studentsList1
                                  join _list2 in studentsList2 on _list1.ID equals _list2.ID into gj
                                  from subset in gj.DefaultIfEmpty()
                                  select _list1;

            return studentsDeletes.ToList<Student>();
        }

        private static List<Student> GetInserts(List<Student> studentsList1, List<Student> studentsList2)
        {
            var studentsInserts = from _list2 in studentsList2
                                  join _list1 in studentsList1 on _list2.ID equals _list1.ID into gj
                                  from subset in gj.DefaultIfEmpty()
                                  select _list2;

            return studentsInserts.ToList<Student>();
        }

        private static List<Student> GetUpdates(List<Student> studentsList1, List<Student> studentsList2)
        {
            var studentsUpdates = from _list1 in studentsList1
                                  join _list2 in studentsList2 on _list1.ID equals _list2.ID
                                  select _list1;

            return studentsUpdates.ToList<Student>();


        }
    }

    public class Student
    {
        public int ID { get; set; }
        public string StudentName { get; set; }
        public string Grade { get; set; }
    }

   
}
