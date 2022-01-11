using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Day27_File_IO
{
    class BinaryOperations
    {
        //serialization
        public static void BinarySerialization()
        {
            Person person = new Person();
            string path = @"C:\Users\Kranthi\Desktop\Bridgelabz\Day27_File_IO\Day27_File_IO\TextFile1.txt";
            BinaryFormatter binary = new BinaryFormatter();
            FileStream file = new FileStream(path, FileMode.OpenOrCreate);
            binary.Serialize(file, person);
        }

        //Deserialization
        public static void BinaryDeserialization()
        {
            Person person = new Person();
            string path = @"C:\Users\Kranthi\Desktop\Bridgelabz\Day27_File_IO\Day27_File_IO\TextFile1.txt";
            BinaryFormatter binary = new BinaryFormatter();
            FileStream file = new FileStream(path, FileMode.Open);
            Person results = (Person)binary.Deserialize(file);
            Console.WriteLine("FirstName \t" + results.FirstName + "\tLastName\t" + results.LastName);
        }
    }

    [Serializable]
    class Person
    {
        public string FirstName { get; set; } = "Akhil";
        public string LastName { get; set; } = "Kumar";

    }
}