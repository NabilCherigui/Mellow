using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sparkle : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    public System.Action OnFind;

    void OnMouseOver() {
        if (Input.GetMouseButtonDown(0)) {
            OnFind();
        }
    }
}
