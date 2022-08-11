using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveableEntity : MonoBehaviour
{
    public static List<SaveableEntity> SaveableEntities { get; private set; } = new List<SaveableEntity>();
    [field: SerializeField] public string Id { get; set; }

    private void Awake()
    {
        SaveableEntities.Add(this);
    }

    private void OnDestroy()
    {
        SaveableEntities.Remove(this);
    }

    [ContextMenu("Generate Id")]
    public void GenerateId()
    {
        Id = System.Guid.NewGuid().ToString();
    }

    public object CaptureState()
    {
        var state = new Dictionary<string, object>();

        foreach (var saveable in GetComponents<ISaveable>())
        {
            state[saveable.GetType().ToString()] = saveable.CaptureState();
        }

        return state;
    }

    public void RestoreState(object state)
    {
        var stateDictionary = (Dictionary<string, object>)state;

        foreach (var saveable in GetComponents<ISaveable>())
        {
            string typeName = saveable.GetType().ToString();

            if (stateDictionary.TryGetValue(typeName, out object value))
            {
                saveable.RestoreState(value);
            }
        }
    }
}
