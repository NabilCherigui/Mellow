using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUpgradeScreenVisual : MonoBehaviour
{
	/// <summary>
	/// Character Image
	/// </summary>
	public Image _image;

	/// <summary>
	/// Character Name Text
	/// </summary>
	public Text _name;

	/// <summary>
	/// Character Level Text
	/// </summary>
	public Text _level;
	
	/// <summary>
	/// Character Health Text
	/// </summary>
	public Text _health;

	/// <summary>
	/// Character Speed Text
	/// </summary>
	public Text _speed;

	/// <summary>
	/// Character Cost Text
	/// </summary>
	public Text _cost;

	/// <summary>
	/// Visualize Characters In Upgrade Screen
	/// </summary>
	/// <param name="data"></param>
    public void Prime(CharacterData data) {
    	_image.sprite = data.Illustratie;
    	_name.text = data.Name;
		int level = GameSave.Instance.CharacterLevels[data.Index];
    	_level.text = level.ToString();
    	_health.text = data.Levels[level].Health.ToString();
    	_speed.text = data.Levels[level].Speed.ToString();
    	_cost.text = data.Levels[level].Cost.ToString();
    }
}
