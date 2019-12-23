using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    /// <summary>
    /// Terrain Objects Array
    /// </summary>
    [SerializeField] private GameObject[] _terrainObjects;

    /// <summary>
    /// Dialogs Array
    /// </summary>
    [SerializeField] private Dialog[] _dialogs;

    /// <summary>
    /// Objects To Set Active On Battle Array
    /// </summary>
    [SerializeField] private GameObject[] _objectsToSetActiveOnBattle;

    /// <summary>
    /// Character Object
    /// </summary>
    [SerializeField] private GameObject chars;

    /// <summary>
    /// Switch The Terrain And Start Dialog
    /// </summary>
    public void ShowDialog() {
        TerrainSwitcher.Instance.Switch(_terrainObjects[1]);
        _dialogs[0].gameObject.SetActive(true);
        StartCoroutine(_dialogs[0].Type());
    }

    /// <summary>
    /// Start Second Part Of Dialog
    /// </summary>
    public void ShowDialog2() {
        TerrainFindableObject.Instance.DestroyFindable();
        SoundController.Instance.PlaySoundEffect(SoundEffectIDs.CHARACTER_DISCOVERYSPARKLE);
        _dialogs[1].gameObject.SetActive(true);
        StartCoroutine(_dialogs[1].Type());
    }

    /// <summary>
    /// Hide Dialog
    /// </summary>
    public void HideDialog() {
        _dialogs[0].gameObject.SetActive(false);
        TerrainFindableObject.Instance.InstantiateFindable(ShowDialog2);
    }

    /// <summary>
    /// Hide Second Part Of Dialog
    /// </summary>
    public void HideDialog2() {
        _dialogs[1].gameObject.SetActive(false);
        StartFight();
    }

    /// <summary>
    /// Start The Battle
    /// </summary>
    public void StartFight() {
        chars.SetActive(true);
        for (int i = _objectsToSetActiveOnBattle.Length - 1; i >= 0; i -= 1) {
            _objectsToSetActiveOnBattle[i].SetActive(true);
        }
    }
}
