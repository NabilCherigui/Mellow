using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboVisualizer : MonoBehaviour
{
    /// <summary>
    /// Input Types List
    /// </summary>
    [SerializeField]
    private List<InputTypes> _inputTypes;

    /// <summary>
    /// Combo Sprites List
    /// </summary>
    [SerializeField]
    private List<Sprite> _comboSprites;

    /// <summary>
    /// Combo Image
    /// </summary>
    [SerializeField]
    private Image _comboImage;

    /// <summary>
    /// Combo Image List
    /// </summary>
    [SerializeField]
    private List<Image> _comboImages;

    /// <summary>
    /// Combination Sprites Dictionary
    /// </summary>
    /// <typeparam name="InputTypes"></typeparam>
    /// <typeparam name="Sprite"></typeparam>
    /// <returns></returns>
    public Dictionary<InputTypes, Sprite> CombinationSprites = new Dictionary<InputTypes, Sprite>();

    private void Awake()
    {
        for (int i = 0; i < _inputTypes.Count; i++)
        {
            CombinationSprites.Add(_inputTypes[i],_comboSprites[i]);
        }
    }

    /// <summary>
    /// Instantiate Combo Sprite Per Input
    /// </summary>
    /// <param name="inputType"></param>
    public void CreateComboSprite(InputTypes inputType)
    {
        foreach (var item in CombinationSprites.Keys)
        {
            if(inputType == item)
            {
                _comboImages.Add(Instantiate(_comboImage, gameObject.transform));
                _comboImages[_comboImages.Count - 1].GetComponent<Image>().sprite = CombinationSprites[item];
            }
        }
    }

    /// <summary>
    /// Clear Combo Sprites
    /// </summary>
    public void ResetComboSprites()
    {
        for (int i = 0; i < _comboImages.Count; i++)
        {
            Destroy(_comboImages[i].gameObject);
        }
        _comboImages.Clear();
    }
}
