using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectScreenVisual : MonoBehaviour
{
	/// <summary>
	/// Character Icon Transform
	/// </summary>
	[SerializeField] private Transform _characterIconContent;

	/// <summary>
	/// Character Upgrade Screen
	/// </summary>
	[SerializeField] private CharacterUpgradeScreenVisual _characterUpgradeScreenVisual;

	/// <summary>
	/// Character Icon Prefab
	/// </summary>
	[SerializeField] private CharacterIconVisual _characterFramePrefab;

	void Awake() {
		Prime(GameSave.Instance.Characters);
	}

	/// <summary>
	/// Create Characters In Select Screen
	/// </summary>
	/// <param name="characters"></param>
	void Prime(CharacterData[] characters) {
		for (int i = 0; i < characters.Length; i += 1) {
			CharacterIconVisual visual = (CharacterIconVisual)Instantiate(_characterFramePrefab) as CharacterIconVisual;
			visual.Prime(characters[i], _characterUpgradeScreenVisual);
			visual.gameObject.SetActive(true);
			visual.gameObject.transform.SetParent(_characterIconContent);
		}
	}
}
