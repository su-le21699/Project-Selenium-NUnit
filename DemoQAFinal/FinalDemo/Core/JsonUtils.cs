using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace FinalDemo.Core
{
    public class JsonUtils
    {
        public static IEnumerable<T> LoadJsonData<T>(string fileLocation)
        {
            string projectDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string jsonFilePath = Path.Combine(projectDirectory, fileLocation);
            var jsonString = File.ReadAllText(jsonFilePath);
            var dataList = JsonSerializer.Deserialize<List<T>>(jsonString);

            if (dataList == null || dataList.Count == 0)
            {
                throw new InvalidOperationException("File name is not exist");
            }

            return dataList;

        }
        public static Dictionary<string, T> ReadDictionaryJson<T>(string filepath)
        {
            var jsonData = File.ReadAllText(filepath);
            var data = JsonSerializer.Deserialize<Dictionary<string, T>>(jsonData);
            return data ?? new Dictionary<string, T>();
        }
    }
}
