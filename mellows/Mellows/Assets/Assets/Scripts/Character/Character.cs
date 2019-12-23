using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    /// <summary>
    /// Combination List Dictionary Key
    /// </summary>
    public List<Combination> CombinationKey;

    /// <summary>
    /// Attack List Dictionary Value
    /// </summary>
    [SerializeField]
    private List<Attack> _attackValue;

    /// <summary>
    /// Combination Key Attack Value Dictionary
    /// </summary>
    /// <typeparam name="Combination"></typeparam>
    /// <typeparam name="Attack"></typeparam>
    /// <returns></returns>
    public Dictionary<Combination, Attack> CombinationMap = new Dictionary<Combination, Attack>();

    /// <summary>
    /// Speed value
    /// </summary>
    public float Speed;

    /// <summary>
    /// Health Value
    /// </summary>
    public float Health;

    /// <summary>
    /// Max Health Value
    /// </summary>
    public float MaxHealth;

    // 1 = friendly
    // 0 = enemy
    /// <summary>
    /// Team Allignment Value
    /// </summary>
    public float TeamAlignment;

    /// <summary>
    /// Defense Value
    /// </summary>
    public float Defense;

    /// <summary>
    /// Basic Attack
    /// </summary>
    [SerializeField]
    private Attack _basicAttack;

    /// <summary>
    /// Possible Target Bool
    /// </summary>
    public bool PossibleTarget;

    /// <summary>
    /// Health Bar Image
    /// </summary>
    public Image HealthBar;

    /// <summary>
    /// Character Sprite
    /// </summary>
    public Sprite CharacterSprite;

    /// <summary>
    /// Character animator
    /// </summary>
    public CharacterAnimator Animator;

    private void Awake()
    {
        for (int i = 0; i < CombinationKey.Count; i++)
        {
            CombinationMap.Add(CombinationKey[i],_attackValue[i]);
        }
    }

    /// <summary>
    /// Display Attack Value
    /// </summary>
    /// <param name="attack"></param>
    public void Attack(Attack attack)
    {
        print(attack.Damage);
    }

    /// <summary>
    /// Display Attack Value
    /// </summary>
    public void Attack()
    {
        print(_basicAttack.Damage);
    }

    /// <summary>
    /// Check If Combination Key Has Attack Value
    /// </summary>
    /// <param name="combination"></param>
    /// <returns></returns>
    public Attack HasAttackOnCombination(Combination combination)
    {
        foreach (var item in CombinationMap.Keys)
        {
            int correct = 0;

            for (int i = 0; i < item.Inputs.Count; i++)
            {
                if(item.Inputs.Count != combination.Inputs.Count)
                {
                    continue;
                }
                else if(item.Inputs[i] != combination.Inputs[i])
                {
                    print("Incorrect : Correct " + correct);
                    continue;
                }
                else if(correct != item.Inputs.Count)
                {
                    correct++;
                    print("Still Counting : Correct " + correct);
                }

                if(correct == item.Inputs.Count)
                {
                    print("Found it!! : Correct " + correct);
                    return CombinationMap[item];
                }
            }
        }

        return _basicAttack;
    }

    public void OnMouseDown()
    {
        if(PossibleTarget)
        {
            BattleController.Instance.TargetSelected(gameObject.GetComponent<Character>());
        }
    }
}
