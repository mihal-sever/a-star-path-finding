using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public class MapSaver : IMapSaver
{
    public void SaveMap(int[,] grid, Point startPoint, Point goalPoint)
    {
        Save save = new Save(grid, startPoint, goalPoint);

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
        bf.Serialize(file, save);
        file.Close();
    }

    public Save LoadMap()
    {
        if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
            Save save = (Save)bf.Deserialize(file);
            file.Close();

            return save;
        }
        else return null;
    }
}
