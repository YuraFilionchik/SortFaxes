using System;
using System.IO;
using System.Windows.Forms;

namespace SortFaxes
{
  static  class Writelog
    {

        public static void WriteLog(string line, string fileName="log.txt")
        {
            try
            {
                int log_size = 100000;
                //пишем все сообщения, генерируемые службой во время работы, в локальный файл на диске
                FileStream fs1 = new FileStream(fileName, FileMode.Append);
                long lenght = fs1.Length;
                fs1.Dispose();
                if (lenght >= log_size) //log_size - предельный размер лог-файла в байтах
                {
                    File.Move(fileName,
                        fileName+"_" + DateTime.Now.ToShortDateString() + "." + DateTime.Now.Hour + "." +
                        DateTime.Now.Minute + "." + DateTime.Now.Second + @".old");
                }
                FileStream fs2 = new FileStream(fileName, FileMode.Append);
                StreamWriter sw = new StreamWriter(fs2);
                sw.WriteLine(DateTime.Now.ToString() + ": " + line);
                sw.Close();
                fs2.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка записи лога");
            }
        }
    }
}
