using System;
using System.Diagnostics;
using System.IO;

namespace BenfordConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            int startsWithOne = 0;
            int startsWithNine = 0;
            Random random = new Random();

            FileStream fs = File.OpenRead(@"C:\Users\oabjo\OneDrive\Bilder\IMG_0028.JPG");
            var b = 0;
            var count = 0;
            while(b!=-1)
            {
                b = fs.ReadByte();
                count++;
                string number = b.ToString();
                if (number.StartsWith('1'))
                    startsWithOne++;
                if (number.StartsWith('9'))
                    startsWithNine++;

            }
            //for (int i = 0; i < 1000000; i++)
            //{
            //    var b = fs.ReadByte();

            //    string number = i.ToString();
            //    //int randomNumber = random.Next(0,999999);
            //    //string number = randomNumber.ToString();
            //    if (number.StartsWith('1'))
            //        startsWithOne++;
            //    if (number.StartsWith('9'))
            //        startsWithNine++;
            //}

            Debug.WriteLine(startsWithOne);
            Debug.WriteLine(startsWithNine);

        }
    }
}
