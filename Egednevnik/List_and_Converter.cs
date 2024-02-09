using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace Egednevnik
{
    internal class Note
    {
        public string Name_of_note;
        public string Discription;
        public string Date_of_note;
    }

    internal class Converter_to_JSON
    {
        public static void MySerealize<T>(T notes)
        {
            string json = JsonConvert.SerializeObject(notes, Formatting.Indented);
            File.WriteAllText("E:\\Lessons\\П\\C# (Скорогудаева София Алексеевна)\\1. Практические\\4\\Egednevnik\\Converted_data\\Заметки.json", json);
        }
        public static T MyDerealize<T>() 
        {
            string json = File.ReadAllText("E:\\Lessons\\П\\C# (Скорогудаева София Алексеевна)\\1. Практические\\4\\Egednevnik\\Converted_data\\Заметки.json");
            T note = JsonConvert.DeserializeObject<T>(json);
            return note;
        }
    }
}
