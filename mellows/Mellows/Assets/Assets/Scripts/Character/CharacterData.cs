using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Ability {
	public string Name;
	public int RequiredLevel;
	public int Damage;
	public InputType[] Inputs;
}

[System.Serializable]
public class CData {
	public int Health;
	public int Speed;
	public int Cost;
	public Ability[] Abilities;

}

[CreateAssetMenu(fileName = "CharacterData", menuName = "Data/Character")]
public class CharacterData : ScriptableObject
{
	public int Index;
	public Sprite SelectScreenIcon;
	public Sprite Illustratie;
	public string Name;
	public CData[] Levels;
}