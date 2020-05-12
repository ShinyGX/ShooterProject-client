using UnityEngine;

public class MonoSignleton<T> : MonoBehaviour where T: Component
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<T>();
                if(instance == null)
                {
                    GameObject obj = new GameObject("__monoSignleton");
                    instance = obj.AddComponent<T>();
                }
            }

            return instance;
        }
    }
}
