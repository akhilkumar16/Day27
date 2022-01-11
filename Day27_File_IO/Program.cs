using System;
using System.IO;

namespace Day27_File_IO
{
    class Program
    {
        static void Main(string[] args)
        {
            //Program.FileExists();
            //Program.ReadAllLines();
            //Program.ReadAllText();
            Program.FileCopy();
        }
        public static void FileExists()
        {
            String path = @"C:\Users\Kranthi\Desktop\Day27_File_IO\Day27_File_IO\Example.txt";
            if (File.Exists(path))
            {
                Console.WriteLine("file exists");
            }
            Console.ReadLine();
        }
        public static void ReadAllLines()
        {
          String path = @"C:\Users\Kranthi\Desktop\Day27_File_IO\Day27_File_IO\Example.txt";
            String[] Lines;
            Lines = File.ReadAllLines(path);
            Console.WriteLine(Lines[0]);
            Console.WriteLine(Lines[1]);
            Console.ReadLine();
        }
        public static void ReadAllText()
        {
            String path = @"C:\Users\Kranthi\Desktop\Day27_File_IO\Day27_File_IO\Example.txt";
            String lines;
            lines = File.ReadAllText(path);
            Console.WriteLine(lines);
            Console.ReadLine();
        }
        public static void FileCopy()
        {
            String path = @"C:\Users\Kranthi\Desktop\Day27_File_IO\Day27_File_IO\Example.txt";
            String copypath = @"C:\Users\Kranthi\Desktop\Day27_File_IO\Day27_File_IO\samplecopy1.txt";
            File.Copy(path, copypath);
            Console.ReadKey();
        }
    }
}
