using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamGehem
{
    public class EnumFileGenerator
    {
        private static readonly string file_content_format_first = "using System;\nnamespace TeamGehem{{\npublic enum E{0}{{\n";
        private static readonly string file_content_format_type1 = "{0} = {1},\n";
        private static readonly string file_content_format_type2 = "{0} = {1}, //{2}\n";
        private static readonly string file_content_format_end = "}}";
        private static readonly string file_name_format = "./E{0}.cs";
        private static readonly string file_extention = "Local*.csv";

        private static readonly string key_file_name = "Local_Lang";

        private static readonly string complete_message_format = "{0}. Create {1}";
        private static readonly string not_found_file_message = "not found files.";

        private static readonly EnumFileGenerator instance_ = new EnumFileGenerator();
        public static EnumFileGenerator Instance { get { return instance_; } }

        private StringBuilder sb = new StringBuilder();

        private EnumFileGenerator()
        {
            sb = new StringBuilder();
        }

        public static void GenerateAllEnumFile()
        {
            Instance.GenerateAllEnumFile_();
        }

        private void GenerateAllEnumFile_()
        {
            DirectoryInfo directory_info = new DirectoryInfo("./");

            FileInfo[] file_info_array = directory_info.GetFiles(string.Format("*{0}",file_extention));
            //File_info_array.OrderBy(f => f.Name); Should not linq
            SortFileInfo(ref file_info_array);

            List<string> line_list = new List<string>();
            EnumFileGenerator file_gen = EnumFileGenerator.Instance;

            int enum_index = 0;
            int file_info_array_length = file_info_array.Length;
            if (file_info_array_length == 0) { Console.WriteLine(not_found_file_message); return; }

            for (int file_index = 0; file_index < file_info_array.Length; ++file_index)
            {
                FileInfo file_info = file_info_array[file_index];
                string file_name = file_info.Name.TrimEnd(file_extention.ToCharArray());

                using (TextReader tr = file_info.OpenText())
                {
                    string line;
                    while (!string.IsNullOrEmpty(line = tr.ReadLine()))
                    {
                        line_list.Add(line);
                    }
                }
                int list_count = line_list.Count;
                string[] key_name = null;
                string[] value_name = null;
                if (file_name.Equals(key_file_name))
                {
                    var parts = line_list[0].Split(',');
                    key_name = new string[parts.Length - 1];
                    for(int j=1; j<parts.Length; ++j)
                    {
                        key_name[j - 1] = parts[j];
                    }
                    file_gen.CreateEnumFile(file_name, key_name, value_name, 0);
                }
                else
                {
                    key_name = new string[list_count];
                    value_name = new string[list_count];
                    for (int i = 0; i < list_count; ++i)
                    {
                        var parts = line_list[i].Split(',');
                        key_name[i] = parts[0];
                        value_name[i] = parts[1];
                     }
                    file_gen.CreateEnumFile(file_name, key_name, value_name, enum_index * key_name.Length);
                    ++enum_index;
                }

                line_list.Clear();

                Console.WriteLine(complete_message_format, file_index, file_name);
            }
        }

        private void CreateEnumFile(string file_name, string [] contents, string [] value_name, int fiducial_value)
        {
            sb.AppendFormat(file_content_format_first, file_name);

            if(value_name == null)
            {
                for (int i = 0; i < contents.Length; ++i)
                {
                    sb.AppendFormat(file_content_format_type1, contents[i], i + fiducial_value);
                }
            }
            else
            {
                for (int i = 0; i < contents.Length; ++i)
                {
                    sb.AppendFormat(file_content_format_type2, contents[i], i + fiducial_value, value_name[i]);
                }
            }

            sb.Append(file_content_format_end);

            string savePath = string.Format(@file_name_format, file_name);

            System.IO.File.WriteAllText(savePath, sb.ToString(), Encoding.Default);
            sb.Length = 0;
        }

        private void SortFileInfo(ref FileInfo[] fileEntries)
        {
            Array.Sort(fileEntries,
               delegate(FileInfo x, FileInfo y)
               {
                   return y.Name.CompareTo(x.Name);
               }
             );
        }
    }
}
