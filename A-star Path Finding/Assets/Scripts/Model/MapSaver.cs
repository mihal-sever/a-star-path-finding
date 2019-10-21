using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class MapSaver : IMapSaver
{
    public void SaveMap(int[,] grid, Point startPoint, Point goalPoint)
    {
        Save save = new Save(grid, startPoint, goalPoint);

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(GetDataPath());
        bf.Serialize(file, save);
        file.Close();
    }

    public Save LoadMap()
    {
        if (File.Exists(GetDataPath()))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(GetDataPath(), FileMode.Open);
            Save save = (Save)bf.Deserialize(file);
            file.Close();

            return save;
        }
        else return null;
    }

    private string GetDataPath()
    {
#if UNITY_EDITOR
        return "Assets/map.save";
#else
        return Application.persistentDataPath + "/map.save";
#endif
    }
}
