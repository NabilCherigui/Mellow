using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleController : Singleton<BattleController>
{
    [SerializeField] private TurnOrderVisualizer _turnOrderVisualizer;
    [SerializeField] private TurnVisualizer _turnVisualizer;
    [SerializeField] private TargetVisualizer _targetVisualizer;
    [SerializeField] private TimerVisualizer _timerVisualizer;

    /// <summary>
    /// Won Bool
    /// </summary>
    [SerializeField] private bool won = false;

    /// <summary>
    /// Win And Lose Screen Transform
    /// </summary>
    [SerializeField] private Transform _wonScreen, _loseScreen;

    /// <summary>
    /// Characters List
    /// </summary>
    /// <typeparam name="Character"></typeparam>
    /// <returns></returns>
    [SerializeField]
    private List<Character> _characters = new List<Character>();

    /// <summary>
    /// Current Character Index
    /// </summary>
    [SerializeField]
    private int _currentCharacterIndex;

    /// <summary>
    /// Input Handler
    /// </summary>
    [SerializeField]
    private InputHandler _inputHandler;

    /// <summary>
    /// Input Time Frame Float
    /// </summary>
    [SerializeField]
    private float _inputTimeFrame;

    /// <summary>
    /// Previous Input Amount
    /// </summary>
    [SerializeField]
    private int _perviousInputAmount;

    /// <summary>
    /// Previous Character Index
    /// </summary>
    private int _previousCharacterIndex;

    /// <summary>
    /// Timer Start Bool
    /// </summary>
    private bool _timerStart = false;

    /// <summary>
    /// Allow Input Bool
    /// </summary>
    private bool _allowInput = false;

    /// <summary>
    /// Completed Combination
    /// </summary>
    [SerializeField]
    private Combination _combination;

    /// <summary>
    /// Completed Combination Attack
    /// </summary>
    [SerializeField]
    private Attack _attack;

    /// <summary>
    /// Possible Target Characters List
    /// </summary>
    [SerializeField]
    private List<Character> _targetCharacters;

    /// <summary>
    /// Selected Target Character
    /// </summary>
    [SerializeField]
    private Character _selectedTarget;

    /// <summary>
    /// Final Damage Value
    /// </summary>
    [SerializeField]
    private float _finalDamage;

    /// <summary>
    /// Current Target Character Health Value
    /// </summary>
    [SerializeField]
    private float _currentCharacterHealth;

    /// <summary>
    /// Target Character Health Value
    /// </summary>
    [SerializeField]
    private float _targetCharacterHealth;
    /// <summary>
    /// Health Adjustment Speed Value
    /// </summary>
    [SerializeField]
    private float _healthSpeed;

    /// <summary>
    /// Health Bar Fill Amount Value
    /// </summary>
    [SerializeField]
    private float _healthBarPercentage;

    /// <summary>
    /// Attack Complete Bool
    /// </summary>
    [SerializeField]
    private bool _attackComplete;

    private void Awake()
    {
                SoundController.Instance.PlaySoundEffect(SoundEffectIDs.CHARACTER_MATCHSTARTS);

        //Finds all characters in the scene and sorts them based on speed
        FindAndSort();

        //Creates the TurnIndicator in the scene
        _turnVisualizer.SetArrow(_characters[_currentCharacterIndex]);

        //changing player test
        _previousCharacterIndex = _currentCharacterIndex;
    }

    private void Start()
    {
        //Create the PossibleTarget and Target Indicators in the scene
        _targetVisualizer.CreateArrows(_characters.Count);

        //Check if a team has won or if the battle should continue
        CheckBattleStatus();

        for (int i = 0; i < _characters.Count; i++)
        {
            _turnOrderVisualizer.CreateCharacterImage();
            _turnOrderVisualizer.SetCharacterImage(_characters[i].CharacterSprite, i);
        }
    }

    private void Update()
    {
        //Check if a team has won, if not continue with the battle
        if(CheckBattleStatus() == false)
        {
            //Check if character index is out of range
            if(_currentCharacterIndex > _characters.Count - 1)
            {
                _currentCharacterIndex = 0;
            }

            //Check if the character selected from the list is alive, if not move to the next character
            if(_characters[_currentCharacterIndex].Health <= 0)
            {
                _currentCharacterIndex ++;
                return;
            }

            //Battle System for Player Characters
            if(_characters[_currentCharacterIndex].TeamAlignment == 1)
            {
                PlayerBattleSystem();
            }
            //Battle System for Enemy Characters
            else if(_characters[_currentCharacterIndex].TeamAlignment == 0)
            {
                EnemyBattleSystem();
            }

            //changing turn indicator
            ChangeTurnIndicator();
        } else {
            SoundController.Instance.PlaySoundEffect(SoundEffectIDs.CHARACTER_MATCHENDS);
            if (won) {
                _wonScreen.gameObject.SetActive(true);
            } else {
                _loseScreen.gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// Change Turn Indicator
    /// </summary>
    public void ChangeTurnIndicator()
    {
        if(_previousCharacterIndex != _currentCharacterIndex)
            {
                _turnVisualizer.SetArrow(_characters[_currentCharacterIndex]);
                _previousCharacterIndex = _currentCharacterIndex;
            }
    }

    /// <summary>
    /// Reset Battle Controller For Next Character
    /// </summary>
    public void ResetBattleController()
    {
        _combination.Inputs.Clear();
        _currentCharacterIndex++;
        _inputHandler.CurrentCombination.Inputs.Clear();
        _perviousInputAmount = 0;
        _timerVisualizer.ResetTimerVisualizer();
        _timerStart = false;
        _selectedTarget = null;
        _attackComplete = false;
        ComboVisualizer.Instance.ResetComboSprites();
    }

    /// <summary>
    /// Try Given Input To Find Correlating Attack
    /// </summary>
    /// <param name="combination"></param>
    /// <returns>Attack</returns>
    public Attack TryInput(Combination combination)
    {
        var attack = _characters[_currentCharacterIndex].HasAttackOnCombination(combination);
        return attack;
    }

    /// <summary>
    /// Check if a team is completly wiped out
    /// </summary>
    /// <param name="teamAlignment"></param>
    /// <returns>Bool</returns>
    public bool CheckTeamVitality(int teamAlignment)
    {
        for (int i = 0; i < _characters.Count; i++)
        {
            if(_characters[i].TeamAlignment == teamAlignment && _characters[i].Health > 0)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Find all characters in the scene and sort them based on speed
    /// </summary>
    public void FindAndSort()
    {
        //Find all objects with Character script
        _characters.AddRange(GameObject.FindObjectsOfType<Character>());
        //Sort all characters base on their speed from high to low
        _characters.Sort((p1,p2)=>p2.Speed.CompareTo(p1.Speed));
    }

    /// <summary>
    /// Check if any team has won
    /// </summary>
    /// <returns>Bool</returns>
    public bool CheckBattleStatus()
    {
        if(CheckTeamVitality(1) == false)
        {
            print("Defeat");
            won = false;
            return true;
        }
        else if(CheckTeamVitality(0) == false)
        {
            print("Victory");
            won = true;
            return true;
        }
        else
        {
            //Activate Gameplay Function
            print("Continue");
            return false;
        }
    }

    /// <summary>
    /// Players Battle System
    /// </summary>
    public void PlayerBattleSystem()
    {
            //Making input possible
                _allowInput = true;

                //Timer is running and isn't done yet
                if(_timerStart && _timerVisualizer.IsTimerDone(0) == false)
                {
                    _timerVisualizer.StartTimerVisualizer(_inputTimeFrame);
                }
                //Timer is done
                else if(_timerVisualizer.IsTimerDone(0))
                {
                    //Stops Input from being registered to the combo
                    _allowInput = false;

                    //Makes a new list with the combo
                    if(_combination.Inputs.Count == 0)
                    {
                        for (int i = 0; i < _inputHandler.CurrentCombination.Inputs.Count; i++)
                        {
                            _combination.Inputs.Add(_inputHandler.CurrentCombination.Inputs[i]);
                        }
                    }

                    //Find the attack relating to the combo
                    SoundController.Instance.PlaySoundEffect(SoundEffectIDs.CHARACTER_SELECTATTACK);
                    _attack = TryInput(_combination);

                    //Place the PossibleTargetsIndicators above the possible targets
                    SoundController.Instance.PlaySoundEffect(SoundEffectIDs.CHARACTER_SELECTTARGET);
                    PlacePossibleTargetArrows(0);

                    //Make it possible to click on possible targets
                    EnablePossibleTargets(_targetCharacters);

                    //When a target is selected
                    if(_selectedTarget != null)
                    {
                        SoundController.Instance.PlaySoundEffect(SoundEffectIDs.CHARACTER_ATTACK);
                        SoundController.Instance.PlaySoundEffect(SoundEffectIDs.CHARACTER_DAMAGE);
                        _characters[_currentCharacterIndex].Animator.SetAnimation(AnimationControllerIDs.ATTACK, true);

                        //Disables the possibilty to click on targets
                        DisablePossibleTargets(_targetCharacters);

                        //Disables the PossibleTargetsIndicators
                        _targetVisualizer.DisableArrows();

                        //Enables the TargetIndicator above the selected target
                        _targetVisualizer.SetTargetArrow(_selectedTarget);

                        //Calculates the actual damage to be done to the target using the targets defense
                        _finalDamage = CalculateDamage(_selectedTarget, _attack.Damage);

                        //If the attack has not finished yet
                        if(_attackComplete == false)
                        {
                            //Gets the selected targets current health to use for furter calculations
                            _currentCharacterHealth = _selectedTarget.Health;

                            //If the attack does any damage to the selected target
                            if(_finalDamage > 0)
                            {
                                //Calculates the targets health after taking damage
                                _targetCharacterHealth = _currentCharacterHealth - _finalDamage;
                            }
                        }

                        //Calculates what the fill amount for the selected targets health bar should be to reflect its actual health
                        _healthBarPercentage = 1 / _selectedTarget.MaxHealth * _targetCharacterHealth;

                        //Check if the health bar fill amount equals the percentage it should have
                        if(_selectedTarget.HealthBar.fillAmount > _healthBarPercentage && _selectedTarget.HealthBar.fillAmount > 0)
                        {
                            //Decrease the health bar fill amount over time
                            _selectedTarget.HealthBar.fillAmount -= 1.0f / _healthSpeed * Time.deltaTime;
                        }
                        else
                        {
                            //Once the health bar fill amount is correct the selected target's health is changed to reflect it having taken damage
                            _selectedTarget.Health = _targetCharacterHealth;

                            //Disable the selected target indicator
                            _targetVisualizer.DisableTargetArrow();

                            //Used to indicate that the attack has finished
                            _attackComplete = true;
                        }

                        if(_attackComplete)
                        {
                            //reset everything for the next character
                            ResetBattleController();
                        }
                    }
                }

                //Input is detected and input is allowed
                if(_inputHandler.CurrentCombination.Inputs.Count > _perviousInputAmount && _allowInput)
                {
                    if(_combination.Inputs.Count == 0)
                    {
                        ComboVisualizer.Instance.CreateComboSprite(_inputHandler.CurrentCombination.Inputs[_perviousInputAmount]);
                    }

                    //Timer is stopped and reset
                    _timerStart = false;
                    _timerVisualizer.ResetTimerVisualizer();
                    //Timer image is enabled
                    _timerVisualizer.EnableTimerImage(true);
                    //Timer starts
                    _timerStart = true;
                    //Used to check new input
                    _perviousInputAmount = _inputHandler.CurrentCombination.Inputs.Count;
                }
    }

    /// <summary>
    /// Enemy Battle System
    /// </summary>
    public void EnemyBattleSystem()
    {
        print("Is Enemy");

                //choose random combo
                if(_combination.Inputs.Count == 0)
                {
                    var randomCombo = Random.Range(0, _characters[_currentCharacterIndex].CombinationKey.Count);

                    //Make a new list with the combo
                    for (int i = 0; i < _characters[_currentCharacterIndex].CombinationKey[randomCombo].Inputs.Count; i++)
                    {
                        _combination.Inputs.Add(_characters[_currentCharacterIndex].CombinationKey[randomCombo].Inputs[i]);
                    }
                }

                //Find the attack relating to the combo
                _attack = TryInput(_combination);

                //Place the PossibleTargetsIndicators above the possible targets
                PlacePossibleTargetArrows(1);

                if(_selectedTarget != null)
                {
                    //Disables the PossibleTargetsIndicators
                    _targetVisualizer.DisableArrows();

                    //Enables the TargetIndicator above the selected target
                    _targetVisualizer.SetTargetArrow(_selectedTarget);

                    //Calculates the actual damage to be done to the target using the targets defense
                    _finalDamage = CalculateDamage(_selectedTarget, _attack.Damage);

                    //If the attack has not finished yet
                    if(_attackComplete == false)
                    {
                        //Gets the selected targets current health to use for furter calculations
                        _currentCharacterHealth = _selectedTarget.Health;

                        //If the attack does any damage to the selected target
                        if(_finalDamage > 0)
                        {
                            //Calculates the targets health after taking damage
                            _targetCharacterHealth = _currentCharacterHealth - _finalDamage;
                        }
                    }

                    //Calculates what the fill amount for the selected targets health bar should be to reflect its actual health
                    _healthBarPercentage = 1 / _selectedTarget.MaxHealth * _targetCharacterHealth;

                    //Check if the health bar fill amount equals the percentage it should have
                    if(_selectedTarget.HealthBar.fillAmount > _healthBarPercentage && _selectedTarget.HealthBar.fillAmount > 0)
                    {
                        //Decrease the health bar fill amount over time
                        _selectedTarget.HealthBar.fillAmount -= 1.0f / _healthSpeed * Time.deltaTime;
                    }
                    else
                    {
                        //Once the health bar fill amount is correct the selected target's health is changed to reflect it having taken damage
                        _selectedTarget.Health = _targetCharacterHealth;

                        //Disable the selected target indicator
                        _targetVisualizer.DisableTargetArrow();

                        //Used to indicate that the attack has finished
                        _attackComplete = true;
                    }

                    if(_attackComplete)
                    {
                        //reset everything for the next character
                        ResetBattleController();
                    }
                }
                else
                {
                    //Select a random target that is still alive
                    var randomTarget = Random.Range(0, _targetCharacters.Count);

                    if(_targetCharacters[randomTarget].Health > 0)
                    {
                        _selectedTarget = _targetCharacters[randomTarget];
                    }
                    else
                    {
                        return;
                    }

                }
    }

    /// <summary>
    /// Place Possible Target Arrows Above Selected Team
    /// </summary>
    /// <param name="teamAlignment"></param>
    public void PlacePossibleTargetArrows(int teamAlignment)
    {
        _targetCharacters.Clear();

        for (int i = 0; i < _characters.Count; i++)
        {
            if(_characters[i].TeamAlignment == teamAlignment && _characters[i].Health > 0)
            {
                _targetCharacters.Add(_characters[i]);
            }
        }

        _targetVisualizer.SetArrow(_targetCharacters);
    }

    /// <summary>
    /// Enable Possible Targets To Be Selected As Targets
    /// </summary>
    /// <param name="characters"></param>
    public void EnablePossibleTargets(List<Character> characters)
    {
        for (int i = 0; i < characters.Count; i++)
        {
            characters[i].PossibleTarget = true;
        }
    }

    /// <summary>
    /// Used in Character to Indicate that Character is selected
    /// </summary>
    /// <param name="character"></param>
    public void TargetSelected(Character character)
    {
        _selectedTarget = character;
    }

    /// <summary>
    /// Disables the Possible Targets to be selected as targets
    /// </summary>
    /// <param name="characters"></param>
    public void DisablePossibleTargets(List<Character> characters)
    {
        for (int i = 0; i < characters.Count; i++)
        {
            characters[i].PossibleTarget = false;
        }
    }

    /// <summary>
    /// Returns the remaining damage after subtracting the selected target's defense
    /// </summary>
    /// <param name="target"></param>
    /// <param name="damage"></param>
    /// <returns>Damage Value</returns>
    public float CalculateDamage(Character target, float damage)
    {
        return damage - target.Defense;
    }
}
