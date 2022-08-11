using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Spawner
{
    public static T CreateOnTemplate<T>(T prefab, Transform parent, Vector3 position) where T : MonoBehaviour
    {
        T p = MonoBehaviour.Instantiate(prefab, parent);
        p.transform.position = position;
        return p;
    }

    public static T CreateOnTemplate<T>(T prefab, Transform parent) where T : MonoBehaviour => CreateOnTemplate<T>(prefab, parent, Vector3.zero);
    public static T CreateOnTemplate<T>(T prefab, Vector3 position) where T : MonoBehaviour => CreateOnTemplate<T>(prefab, null, position);
    public static T CreateOnTemplate<T>(T prefab) where T : MonoBehaviour => CreateOnTemplate<T>(prefab, null, Vector3.zero);
}
