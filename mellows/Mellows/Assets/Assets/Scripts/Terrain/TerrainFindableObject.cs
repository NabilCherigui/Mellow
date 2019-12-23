using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainFindableObject : Singleton<TerrainFindableObject>
{
    /// <summary>
    /// Sparke Prefab
    /// </summary>
    [SerializeField] private GameObject _sparklePrefab;

    /// <summary>
    /// Searching Bool
    /// </summary>
    [SerializeField] private bool _searching;

    /// <summary>
    /// Possible Hiding Spots Objects
    /// </summary>
    [SerializeField] private GameObject[] _props;

    /// <summary>
    /// Selected Hiding Spot Object
    /// </summary>
    [SerializeField] private GameObject _prop;

    /// <summary>
    /// Instantiated Sparkle Object
    /// </summary>
    [SerializeField] private GameObject _currentSparkle;

    /// <summary>
    /// Select Object To Instantiate Sparkle
    /// </summary>
    /// <param name="onFind"></param>
    public void InstantiateFindable(System.Action onFind) {
        _currentSparkle = Instantiate(_sparklePrefab);
        _currentSparkle.GetComponent<Sparkle>().OnFind = onFind;
        int i = Random.Range(0, _props.Length);

        _searching = true;
        _prop = _props[i];
    }

    void Update() {
        if (_searching) _currentSparkle.transform.position = _prop.transform.position;
    }

    /// <summary>
    /// Destroy Sparkle
    /// </summary>
    public void DestroyFindable() {
        _searching = false;
        Destroy(_currentSparkle);
    }
}
