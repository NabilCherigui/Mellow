using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainSwitcher : Singleton<TerrainSwitcher>
{
    /// <summary>
    /// Current Terrain Object
    /// </summary>
    [SerializeField] private GameObject _currentTerrainObject;

    /// <summary>
    /// Switch Enabled Terrain Object
    /// </summary>
    /// <param name="_terrainObject"></param>
    public void Switch(GameObject _terrainObject) {
        _currentTerrainObject.SetActive(false);
        _terrainObject.SetActive(true);

        _currentTerrainObject = _terrainObject;
    }

    /// <summary>
    /// Animate Rotation To New Terrain Object
    /// </summary>
    /// <param name="prev"></param>
    /// <param name="next"></param>
    /// <returns></returns>
    private IEnumerator AnimateRotation(GameObject prev, GameObject next) {
        yield break;
    }

    /// <summary>
    /// Animate Transition To New Terrain Object
    /// </summary>
    /// <param name="next"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    private IEnumerator AnimateMove(GameObject next, float target) {
        yield break;
    }
}
