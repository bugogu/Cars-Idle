using UnityEngine;

public class Mono<T> : MonoBehaviour where T : Mono<T>
{
    private static volatile T instance = null;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType(typeof(T)) as T;
            }
            return instance;
        }
    }
}
