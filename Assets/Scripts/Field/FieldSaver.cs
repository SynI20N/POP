using Newtonsoft.Json;
using System.IO;
using System;
using UnityEngine;

public class FieldSaver
{
    private static string _path = Application.persistentDataPath + "/data.json";
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

    public static Field Load()
    {
        string json;
        Field field;

        json = File.ReadAllText(_path);
        field = JsonConvert.DeserializeObject<Field>(json);
        Cell[,] cells = field.GetField();
        foreach(var c in cells)
        {
            Debug.Log(c.GetPosition());
        }

        return field;
    }
}