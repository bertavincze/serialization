using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace SerializePeople
{
    class Program
    {
        static void Main(string[] args)
        {
            Person p = new Person("Example", DateTime.Now, Person.Genders.female);
            p.Serialize(@"C:\Users\Tulajdonos\Desktop\temp.txt");
            p.DeSerialize(@"C:\Users\Tulajdonos\Desktop\temp.txt");

            string fileName = @"C:\Users\Tulajdonos\Desktop\tempFile.txt";

            IFormatter formatter = new BinaryFormatter();

            SerializeItem(fileName, formatter);
            DeserializeItem(fileName, formatter);
            Console.WriteLine("Serialization complete.");
            Console.ReadKey();
        }

        public static void SerializeItem(string fileName, IFormatter formatter)
        {
            Person person = new Person("Example", DateTime.Parse("1980.10.10."), Person.Genders.male); 

            FileStream stream = new FileStream(fileName, FileMode.Create);
            formatter.Serialize(stream, person);
            stream.Close();
        }


        public static void DeserializeItem(string fileName, IFormatter formatter)
        {
            FileStream s = new FileStream(fileName, FileMode.Open);
            Person person = (Person)formatter.Deserialize(s);
            Console.WriteLine(person.Name);
            Console.WriteLine(person.DateOfBirth);
            Console.WriteLine(person.Gender);
            Console.WriteLine(person.age);
        }
    }
}
