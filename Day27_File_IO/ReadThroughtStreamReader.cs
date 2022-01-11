using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Day27_File_IO
{
    class ReadThroughtStreamReader
    {
        public void WriteUsingStreamReader()
        {
            String path = @"C:\Users\Kranthi\Desktop\Day27_File_IO\Day27_File_IO\Example.txt";
            using (StreamWriter sr = File.AppendText(path))
            {
                sr.WriteLine("Hello World-.Net is Awesome");
                sr.Close();

                Console.ReadKey();
            }
        }
        public void ReadFromStreamReader()
        {
            String path = @"C:\Users\Kranthi\Desktop\Day27_File_IO\Day27_File_IO\Example.txt";
            using (StreamReader sr = File.OpenText(path))
            {
                String s = "";
                while ((s = sr.ReadLine))!= null)
                {
                    Console.WriteLine(s);
                }
            }
            Console.ReadKey();
        }
    }
}
