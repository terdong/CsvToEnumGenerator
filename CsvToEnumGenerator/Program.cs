using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvToEnumGenerator
{
    struct ItemData
    {
        public string key;
        public string value;
    }

    class Program
    {

        static void Main(string[] args)
        {
            var items = new List<ItemData>();

            foreach (var line in File.ReadAllLines("Localization.csv"))
            {
                var parts = line.Split(',');
                items.Add(new ItemData
                {
                    key = parts[0],
                    value = parts[1],
                });
            }

            foreach(ItemData item in items)
            {
                Console.WriteLine("items = {0}, {1}", item.key, item.value);
            }

            string path_00 = "using System;namespace CsvToEnumGenerator{{\npublic enum {0}{{\n";
            string path_01 = "\n}}";

            StringBuilder sb = new StringBuilder();

            sb.AppendFormat(path_00, "TestEnum");
            sb.Append("test_00 = 1,\ntest_01 = 2");
            sb.Append(path_01);
            // 저장 경로를 지정 합니다.
            string savePath = @"./TestEnum.cs";
            // 입력 할 text 값
            string textValue = "텍스트 파일 입력";
            // Text 파일 생성 및 text 를 입력 합니다.
            System.IO.File.WriteAllText(savePath, sb.ToString(), Encoding.Default);

        }
    }
}
