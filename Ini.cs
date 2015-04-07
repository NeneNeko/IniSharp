using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace IniSharp
{
    class Ini
    {
        public static Ini Create(string path)
        {
            File.WriteAllText(path, "", Encoding.Unicode);
            return new Ini(path);
        }

        public Ini(string path)
        {
            Path = path;
        }

        public void SetValue(string name, object value)
        {
            var lines = File.ReadAllLines(Path).ToList();

            string targetLine = string.Format("{0}={1}", name, value);

            try
            {
                lines[lines.FindIndex(line => line.StartsWith(name, StringComparison.OrdinalIgnoreCase))] = targetLine;
            }
            catch (IndexOutOfRangeException)
            {
                lines.Add(targetLine);
            }

            File.WriteAllLines(Path, lines);
        }

        public T GetValue<T>(string name)
        {
            var line = File.ReadAllLines(Path).First(l => l.StartsWith(name, StringComparison.OrdinalIgnoreCase));
            string value = line.Split(new [] {'='}, 2)[1];

            return (T)Convert.ChangeType(value, typeof(T));
        }

        public string Path { get; set; }
    }
}
