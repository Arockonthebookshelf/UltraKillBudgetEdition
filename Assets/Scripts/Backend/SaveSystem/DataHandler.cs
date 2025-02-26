using System.IO;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DataHandler 
{
   private string dataDirPath;
    private string dataFilePath;
    public DataHandler(string dataDirPath, string dataFilePath)
    {
        this.dataDirPath = dataDirPath;
        this.dataFilePath = dataFilePath;
    }

    public GameData LoadData()
    {
        string fullPath = Path.Combine(dataDirPath, dataFilePath);
        GameData LoadData = null;
        if(File.Exists(fullPath))
        {
            try
            {
                string dataToLoad ="";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using(StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }
                LoadData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch
            {
                Debug.LogError("Failed to load data from " + fullPath);
            }
        }
        return LoadData;
    }
public void SaveData(GameData gameData)
    {
     string fullPath = Path.Combine(dataDirPath, dataFilePath);
     try
     {
        Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
        string dataToStore = JsonUtility.ToJson(gameData);

        using(FileStream stream = new FileStream(fullPath, FileMode.Create))
        {
            using(StreamWriter writer = new StreamWriter(stream))
            {
                writer.Write(dataToStore);
            }
        }
     }
     catch
     {
            Debug.LogError("Failed to save data to " + fullPath);
     }
          
    }

}
