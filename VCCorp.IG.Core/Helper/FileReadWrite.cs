using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VCCorp.IG.Core.Helper
{
    public class FileReadWrite
    {
        public string _pathFile;

        public FileReadWrite(string pathFile)
        {
            _pathFile = pathFile;
        }

        //Đọc dữ liệu trả ra array list
        public List<string> GetListLine()
        {
            try
            {
                if (File.Exists(_pathFile))
                {
                    List<string> lis = new List<string>();
                    using (Stream stream = File.OpenRead(_pathFile))
                    {
                        using (StreamReader r = new StreamReader(stream))
                        {
                            while (r.EndOfStream == false)
                            {
                                lis.Add(r.ReadLine());
                            }
                        }
                    }
                    return lis;
                }
            }
            catch { }
            return null;
        }

        //Ghi file khi đã kiểm tra sự tồn tại
        public bool WriteLineToFile_HasDelete(object textline)
        {
            try
            {
                string folder = Path.GetDirectoryName(_pathFile);
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                if (File.Exists(_pathFile))
                {
                    File.Delete(_pathFile);
                }

                using (StreamWriter sw = new StreamWriter(_pathFile, true, Encoding.UTF8)) // ghi kieu appent
                {
                    sw.WriteLine(textline);
                }
                return true;
            }
            catch { }

            return false;
        }

        //Ghi file theo từng dòng
        public bool WriteLineToFile_Append(object textline)
        {
            try
            {
                string folder = Path.GetDirectoryName(_pathFile);
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                using (StreamWriter sw = new StreamWriter(_pathFile, true, Encoding.UTF8)) // ghi kieu appent
                {
                    sw.WriteLine(textline);
                }
                return true;
            }
            catch { }

            return false;
        }

        //Xóa file
        public bool Delete()
        {
            try
            {
                if (File.Exists(_pathFile))
                {
                    File.Delete(_pathFile);
                }

                return true;

            }
            catch { }
            return false;
        }

    }
}
