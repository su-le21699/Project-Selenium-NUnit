namespace FinalDemo.Core
{
    public class DataProvider<T>
    {
        private static Dictionary<string, T> _data;

        public static void Initialize(string filePath)
        {
            _data = JsonUtils.ReadDictionaryJson<T>(filePath);
        }
        public static T LoadDataByKey(string key)
        {
            if (_data.ContainsKey(key))
                return _data[key];
            return default;
        }
    }
}