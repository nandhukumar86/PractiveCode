using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.ComponentModel.DataAnnotations;

namespace SampleConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Sample Code");

            ICollection<Student> studentsList1 = new List<Student>()
            {
                new Student(){ISBN = "1", StudentName = "Nandha", Grade="8th"},
                new Student(){ISBN = "2", StudentName = "kumar", Grade="9th"},
                new Student(){ISBN = "3", StudentName = "anu", Grade="10th"},
                new Student(){ISBN = "4", StudentName = "Prasanna", Grade="11th"}
            };

            ICollection<Student> studentsList2 = new List<Student>()
            {
                //deleted ID 1, added ID 5.
                new Student(){ISBN = "2", StudentName = "kumar", Grade="12th"},
                new Student(){ISBN = "3", StudentName = "anu", Grade="10th"},
                new Student(){ISBN = "4", StudentName = "Prasanna", Grade="11th"},
                new Student(){ISBN = "5", StudentName = "Prasanna", Grade="11th"}
            };

            ICollection<Student> studentsUpdates = CompareList<Student>.GetUpdates(studentsList1, studentsList2);
            ICollection<Student> studentsInserts = CompareList<Student>.GetInserts(studentsList1, studentsList2);
            ICollection<Student> studentsdeletes = CompareList<Student>.GetDeletes(studentsList1, studentsList2);

            Console.WriteLine("Updates");
            DisplayStudents(studentsUpdates);

            Console.WriteLine("Insterts");
            DisplayStudents(studentsInserts);

            Console.WriteLine("Deletes");
            DisplayStudents(studentsdeletes);


        }

        private static void DisplayStudents(ICollection<Student> listItems)
        {
            foreach (var item in listItems)
            {
                Console.WriteLine($"{item.ISBN}--{item.StudentName}--{item.Grade}");

            }
        }

       
    }

    public class Student
    {
        [Key]
        public string ISBN { get; set; }
        public string StudentName { get; set; }
        public string Grade { get; set; }
    }

    public class EntityPKComparer<T> : IEqualityComparer<T>
    {
        private PropertyInfo Property { get; set; }

        public EntityPKComparer()
        {
            Property = typeof(T).GetProperties()
                 .FirstOrDefault(p => p.GetCustomAttributes(false)
                     .Any(a => a.GetType() == typeof(KeyAttribute)));
        }

        public bool Equals(T x, T y)
        {
            return (Property.GetValue(x)).Equals(Property.GetValue(y)) ? true : false;
        }

        public int GetHashCode(T obj)
        {
            object hashCode = Property.GetValue(obj);
            return hashCode.GetHashCode();
        }
    }

    public static class CompareList<T>
    {
        public static ICollection<T> GetDeletes(ICollection<T> list1, ICollection<T> list2)
        {
            return list1.Except(list2, new EntityPKComparer<T>()).ToList();
        }

        public static ICollection<T> GetInserts(ICollection<T> list1, ICollection<T> list2)
        {
            return list2.Except(list1, new EntityPKComparer<T>()).ToList();
        }

        public static ICollection<T> GetUpdates(ICollection<T> list1, ICollection<T> list2)
        {
            return list2.Intersect(list1, new EntityPKComparer<T>()).ToList();
        }
    }


}
