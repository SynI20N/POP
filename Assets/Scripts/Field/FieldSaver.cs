using Newtonsoft.Json;
using System.IO;
using System;
using UnityEngine;

public class FieldSaver : MonoBehaviour
{
    private static string _path = Path.Combine(Application.persistentDataPath, "data.json");
    public static void Save(Field field)
    {
        try
        {
            string json = JsonConvert.SerializeObject(
                field,
                Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
            File.WriteAllText(_path, json);
        }
        catch(Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    public static void Load()
    {
        string hexPosition = "";
        string json = File.ReadAllText(_path);
        using (var reader = new JsonTextReader(new StringReader(json)))
        {
            while (reader.Read())
            {
                if (reader.TokenType == JsonToken.String && reader.Value.ToString() == "Hex")
                {
                    //GameObject hex = Instantiate(SpawnHelper.LoadPrefab("Environment", reader.Value.ToString()));
                    reader.Read();
                    reader.Read();
                    Debug.LogFormat("{0} - {1} - {2}", reader.TokenType, reader.ValueType, reader.Value);
                    //hex.transform.position = 
                }
            }
        }
    }
}