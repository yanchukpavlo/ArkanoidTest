using UnityEngine;

public static class CollisionExtensions
{
    public static bool TryGetComponent<T>(this Collision other, out T component)
    {
        if (other.gameObject.TryGetComponent(out T c))
        {
            component = c;
            return true;
        }

        component = default(T);
        return false;
    }

    public static bool TryGetComponentInArticulation<T>(this Collision other, out T component)
    {
        if (other.articulationBody)
        {
            var c = other.articulationBody.GetComponent<T>();
            if (c != null)
            {
                component = c;
                return true;
            }
        }

        component = default(T);
        return false;
    }

    public static bool TryGetComponentInRigidbody<T>(this Collision other, out T component)
    {
        if (other.rigidbody)
        {
            var c = other.rigidbody.GetComponent<T>();
            if (c != null)
            {
                component = c;
                return true;
            }
        }

        component = default(T);
        return false;
    }




    public static bool TryGetComponent<T>(this Collider other, out T component)
    {
        if (other.gameObject.TryGetComponent(out T c))
        {
            component = c;
            return true;
        }

        component = default(T);
        return false;
    }

    public static bool TryGetComponentInArticulation<T>(this Collider other, out T component)
    {
        if (other.attachedArticulationBody)
        {
            var c = other.attachedArticulationBody.GetComponent<T>();
            if (c != null)
            {
                component = c;
                return true;
            }
        }

        component = default(T);
        return false;
    }

    public static bool TryGetComponentInRigidbody<T>(this Collider other, out T component)
    {
        if (other.attachedRigidbody)
        {
            var c = other.attachedRigidbody.GetComponent<T>();
            if (c != null)
            {
                component = c;
                return true;
            }
        }

        component = default(T);
        return false;
    }
}
