using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class SaveSystem
{
    static string SaveFuleName => "save";
    static string SaveFuleType => "txt";
    static string SavePath => string.Format("{0}/{1}.{2}", Application.persistentDataPath, SaveFuleName, SaveFuleType);

    static SaveMethod method = new SaveMethodBinary();

    static public void Save(SaveableEntity[] saveableEntities)
    {
        method.Save(saveableEntities, SavePath);
    }

    static public void Load(SaveableEntity[] saveableEntities)
    {
        method.Load(saveableEntities, SavePath);
    }

    static public void Load() => Load(SaveableEntity.SaveableEntities.ToArray());
    static public void Save() => Save(SaveableEntity.SaveableEntities.ToArray());

    static public bool HasSaveFile()
    {
        return File.Exists(SavePath);
    }
}
