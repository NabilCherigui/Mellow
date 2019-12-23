using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetVisualizer : MonoBehaviour
{
    /// <summary>
    /// Possible Target Arrow Prefab
    /// </summary>
    [SerializeField]
    private TargetArrow _possibleTargetArrowPrefab;

    /// <summary>
    /// Target Arrow Prefab
    /// </summary>
    [SerializeField]
    private TargetArrow _targetArrowPrefab;

    /// <summary>
    /// Possible Target Arrows
    /// </summary>
    public List<TargetArrow> PossibleTargetArrows;

    /// <summary>
    /// Target Arrow
    /// </summary>
    public TargetArrow _targetArrow;

    /// <summary>
    /// Instantiate Target Arrow And Possible Target Arrows Based On Character Amount
    /// </summary>
    /// <param name="arrowAmount"></param>
    public void CreateArrows(int arrowAmount)
    {
        _targetArrow = Instantiate(_targetArrowPrefab);
        _targetArrow.gameObject.SetActive(false);
        for (int i = 0; i < arrowAmount; i++)
        {
            PossibleTargetArrows.Add(Instantiate(_possibleTargetArrowPrefab));
            PossibleTargetArrows[i].gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Disable All Possible Target Arrows
    /// </summary>
    public void DisableArrows()
    {
        for (int i = 0; i < PossibleTargetArrows.Count; i++)
        {
            PossibleTargetArrows[i].gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Place Possible Target Arrows Above Possible Targets
    /// </summary>
    /// <param name="characters"></param>
    public void SetArrow(List<Character> characters)
    {
        for (int i = 0; i < characters.Count; i++)
        {
            PossibleTargetArrows[i].SetArrow(characters[i]);
        }
    }

    /// <summary>
    /// Place Target Arrow Above Selected Target
    /// </summary>
    /// <param name="character"></param>
    public void SetTargetArrow(Character character)
    {
        _targetArrow.SetArrow(character);
    }

    /// <summary>
    /// Disable Target Arrow
    /// </summary>
    public void DisableTargetArrow()
    {
        _targetArrow.gameObject.SetActive(false);
    }
}
