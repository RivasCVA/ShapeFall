using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveLoadSystem
{
    static string path = Application.persistentDataPath + "/gamedata.play";

    public static void SaveGameData(GameData data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static GameData LoadGameData()
    {
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            GameData gameData = formatter.Deserialize(stream) as GameData;
            stream.Close();
            return gameData;
        }
        else
        {
            return new GameData();
        }
    }

}
