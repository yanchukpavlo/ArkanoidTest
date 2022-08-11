using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveMethodBinary : SaveMethod
{
    public override void Load(SaveableEntity[] saveableEntities, string savePath)
    {
        Dictionary<string, object> state = LoadFile(savePath);
        RestoreState(saveableEntities, state);
    }

    public override void Save(SaveableEntity[] saveableEntities, string savePath)
    {
        Dictionary<string, object> state = LoadFile(savePath);
        CaptureState(saveableEntities, state);
        SaveFile(state, savePath);
    }    

    static Dictionary<string, object> LoadFile(string savePath)
    {
        if (!File.Exists(savePath))
        {
            return new Dictionary<string, object>();
        }

        using (FileStream stream = File.Open(savePath, FileMode.Open))
        {
            var formatter = GetFormatter();
            var data = (Dictionary<string, object>)formatter.Deserialize(stream);
            stream.Close();
            return data;
        }
    }

    static void SaveFile(object state, string savePath)
    {
        using (var stream = File.Open(savePath, FileMode.Create))
        {
            var formatter = GetFormatter();
            formatter.Serialize(stream, state);
            stream.Close();
        }
    }

    static BinaryFormatter GetFormatter()
    {
        BinaryFormatter bf = new BinaryFormatter();
        SurrogateSelector ss = new SurrogateSelector();

        Vector3SerializationSurrogate v3ss = new Vector3SerializationSurrogate();
        ss.AddSurrogate(typeof(Vector3),
                        new StreamingContext(StreamingContextStates.All),
                        v3ss);

        QuaternionSerializationSurrogate qss = new QuaternionSerializationSurrogate();
        ss.AddSurrogate(typeof(Quaternion),
                        new StreamingContext(StreamingContextStates.All),
                        qss);

        ColorSerializationSurrogate css = new ColorSerializationSurrogate();
        ss.AddSurrogate(typeof(Color),
                        new StreamingContext(StreamingContextStates.All),
                        css);

        bf.SurrogateSelector = ss;
        return bf;
    }
}
