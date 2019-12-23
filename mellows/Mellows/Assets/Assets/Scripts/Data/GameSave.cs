using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSave : Singleton<GameSave>
{
    /// <summary>
    /// Levels Data Array
    /// </summary>
    public LevelData[] Levels;

    /// <summary>
    /// Characters Data Array
    /// </summary>
    [SerializeField] private CharacterData[] _characters;

    /// <summary>
    /// Public Characters Data Array
    /// </summary>
    /// <value></value>
    public CharacterData[] Characters { get { return _characters; } set { } }

    /// <summary>
    /// Level Stage ID
    /// </summary>
    private int _levelStage = 0;

    /// <summary>
    /// Public Level Stage ID
    /// </summary>
    /// <value></value>
    public int LevelStage { get { return _levelStage; } set { } }

    /// <summary>
    /// XP Potion Amount
    /// </summary>
    private int _xpPotionAmount = 0;

    /// <summary>
    /// Public XP Potion Amount
    /// </summary>
    /// <value></value>
    public int XPPotionAmount { get { return _xpPotionAmount; } set { } }

    /// <summary>
    /// Character Levels Array
    /// </summary>
    /// <returns></returns>
    [SerializeField] public int[] CharacterLevels;

    void Awake()
    {
        if (!PlayerPrefs.HasKey("HasSave")) {
            PlayerPrefs.SetInt("HasSave", 1);

            PlayerPrefs.SetInt("LevelStage", 1);
            PlayerPrefs.SetInt("XPPotionAmount", 0);

            for (int i = 0; i < 3; i += 1) {
                PlayerPrefs.SetInt("CharacterLevel" + i, 1);
            }
        }

        _levelStage = PlayerPrefs.GetInt("LevelStage");
        _xpPotionAmount = PlayerPrefs.GetInt("XPPotionAmount");

        CharacterLevels = new int[3];
        for (int i = 0; i < 3; i += 1) {
            CharacterLevels[i] = PlayerPrefs.GetInt("CharacterLevel" + i);
        }
    }
}
