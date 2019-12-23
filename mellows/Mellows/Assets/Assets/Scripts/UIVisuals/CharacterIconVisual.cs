using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterIconVisual : MonoBehaviour
{
    /// <summary>
    /// Character's Image
    /// </summary>
    [SerializeField] private Image _image;
    
    /// <summary>
    /// Character's Image Toggle State
    /// </summary>
    [SerializeField] private Toggle _toggle;

    /// <summary>
    /// Toggle Character Image
    /// </summary>
    /// <param name="character"></param>
    /// <param name="_characterUpgradeScreenVisual"></param>
    public void Prime(CharacterData character, CharacterUpgradeScreenVisual _characterUpgradeScreenVisual) {
    	_image.sprite = character.SelectScreenIcon;
    	_toggle.onValueChanged.AddListener((change) => {
    		_characterUpgradeScreenVisual.Prime(character);
    	});
    }
}
