
using System.Collections.Generic;

public abstract class SaveMethod
{
    public abstract void Save(SaveableEntity[] saveableEntities, string savePath);

    public abstract void Load(SaveableEntity[] saveableEntities, string savePath);

    protected void CaptureState(SaveableEntity[] saveableEntities, Dictionary<string, object> state)
    {
        foreach (var saveable in saveableEntities)
        {
            state[saveable.Id] = saveable.CaptureState();
        }
    }

    protected void RestoreState(SaveableEntity[] saveableEntities, Dictionary<string, object> state)
    {
        foreach (var saveable in saveableEntities)
        {
            if (state.TryGetValue(saveable.Id, out object value))
            {
                saveable.RestoreState(value);
            }
        }
    }
}
