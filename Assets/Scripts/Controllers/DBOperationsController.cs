using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class DBOperationsController : MonoBehaviour
{
    private BinaryFormatter formatter = new BinaryFormatter();

    public static DBOperationsController element;

    private void Awake()
    {
        element = this;
    }

    public void CreateSaving(GameDataConfig data)
    {
        CreateFileStream(Application.persistentDataPath + "/gameData.save", data);
    }

     public GameDataConfig LoadSaving()
    {
        string path = Application.persistentDataPath + "/gameData.save";

        if (File.Exists(path))
        {
            FileStream stream = new FileStream(path, FileMode.Open);
            GameDataConfig data = formatter.Deserialize(stream) as GameDataConfig;
            stream.Close();
            return data;
        }
        else
        {
            GameDataConfig config = new GameDataConfig();

            CreateSaving(config);

            return config;
        }
    }

    private void CreateFileStream<T>(string path, T data)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, data);

        stream.Close();
    }
}
