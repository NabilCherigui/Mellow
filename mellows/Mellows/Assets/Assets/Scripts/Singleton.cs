using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    public static T Instance {
        get {
            if (_instance == null) {
                var find = GameObject.FindObjectOfType<T>();
                if (find != null) {
                    _instance = find;
                } else {
                    var go = new GameObject("Singleton: " + typeof(T).FullName);
                    _instance = go.AddComponent<T>();
                }
            }
            return _instance;
        }
    }
}
