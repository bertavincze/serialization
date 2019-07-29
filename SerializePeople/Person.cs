using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace SerializePeople
{
    [Serializable]
    class Person : IDeserializationCallback, ISerializable
    {
        [NonSerialized]
        public int age;

        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        internal Genders Gender { get; set; }

        public Person(string name, DateTime dateOfBirth, Genders gender)
        {
            Name = name;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            CalculateAge();
        }

        private void CalculateAge()
        {
            age = (DateTime.Today).Year - DateOfBirth.Year;
        }

        public void Serialize(string output)
        {
            File.Create(output);
            Stream stream = File.Open(output, FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, this);
            stream.Close();
        }

        public void DeSerialize(string output)
        {
            Stream stream = new FileStream(output, FileMode.Open, FileAccess.Read);
            BinaryFormatter formatter = new BinaryFormatter();
            Person p = (Person)formatter.Deserialize(stream);

            Console.WriteLine(p.Name);
            Console.WriteLine(p.Gender);
            Console.WriteLine(p.age);
            Console.ReadKey();
        }

        public override string ToString()
        {
            return "Person: " + Name + " " + DateOfBirth + " " + Gender;
        }

        public void OnDeserialization(object sender)
        {
            age = (DateTime.Today).Year - DateOfBirth.Year;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Name", Name);
            info.AddValue("Date of birth", DateOfBirth);
            info.AddValue("Gender", Gender);
        }

        public Person(SerializationInfo info, StreamingContext context)
        {
            Name = (string)info.GetValue("Name", typeof(string));
            DateOfBirth = (DateTime)info.GetValue("Date of birth", typeof(DateTime));
            Gender = (Genders)Enum.Parse(typeof(Genders), info.GetString("Gender"));
        }

        public enum Genders
        {
            male = 0,
            female = 1
        }
    }
}
