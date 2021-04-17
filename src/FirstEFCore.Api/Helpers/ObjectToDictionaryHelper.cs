using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FirstEFCore.Api.Helpers
{
    public static class ObjectToDictionaryHelper
    {
        public static void ConvertJsonToDictionary(IDictionary<string, object> json)
        {
            foreach (var (key, value) in json.ToArray())
            {
                var valueType = value.GetType();
                if (valueType == typeof(JObject))
                {
                    var obj = JsonConvert.DeserializeObject<Dictionary<string, object>>(value.ToString() ?? string.Empty);
                    json[key] = obj;
                    ConvertJsonToDictionary(obj);
                }
                else if (valueType == typeof(JArray))
                {
                    var jArray = value as JArray;
                    if (jArray == null) continue;

                    if (jArray.First is {HasValues: false})
                    {
                        var array = 
                            (from item in jArray.ToArray() 
                            let strValue = item.ToString() 
                            where !strValue.Equals("{}")
                            select item.ToString()).Cast<object>().ToList();
                        json[key] = array;
                    }
                    else
                    {
                        var dictionaries = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(jArray.ToString());
                        json[key] = dictionaries;
                        if (dictionaries == null) continue;
                        foreach (var dictionary in dictionaries)
                        {
                            ConvertJsonToDictionary(dictionary);
                        }
                    }
                }
                else
                {
                    json[key] = value;
                }
            }
        }
    }
}