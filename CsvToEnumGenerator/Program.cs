using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamGehem
{
    class Program
    {
        static void Main(string[] args)
        {
            EnumFileGenerator.GenerateAllEnumFile();

            //LocalizationManager.SetLocalizationMessages(1);
            //Console.WriteLine(LocalizationManager.GetMessage(0));

            //Console.ReadLine();
        }
    }
}
