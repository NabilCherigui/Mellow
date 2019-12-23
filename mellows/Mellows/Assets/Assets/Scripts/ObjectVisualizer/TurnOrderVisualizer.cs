using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnOrderVisualizer : MonoBehaviour
{
    /// <summary>
    /// Character Image
    /// </summary>
    [SerializeField]
    private Image _characterImage;

    /// <summary>
    /// Character Images List
    /// </summary>
    [SerializeField]
    private List<Image> _characterImages;

    /// <summary>
    /// Add Character Images
    /// </summary>
    public void CreateCharacterImage()
    {
        _characterImages.Add(Instantiate(_characterImage, gameObject.transform));
    }

    /// <summary>
    /// Set Character Image To Character Sprite
    /// </summary>
    /// <param name="sprite"></param>
    /// <param name="character"></param>
    public void SetCharacterImage(Sprite sprite, int character)
    {
        _characterImages[character].GetComponent<Image>().sprite = sprite;
    }

}
