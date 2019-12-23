using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerVisualizer : MonoBehaviour
{
    /// <summary>
    /// Timer Object
    /// </summary>
    [SerializeField]
    private GameObject _timer;

    /// <summary>
    /// Timer Image
    /// </summary>
    [SerializeField]
    private Image _timerImage;

    /// <summary>
    /// Timer Text
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI _timerText;

    private void Start()
    {
        // _timerImage = _timer.GetComponent<Image>();
        EnableTimerImage(false);
    }

    /// <summary>
    /// Enable Timer Image
    /// </summary>
    /// <param name="enable"></param>
    public void EnableTimerImage(bool enable)
    {
        _timer.SetActive(enable);
    }

    /// <summary>
    /// Start Timer Visualizer
    /// </summary>
    /// <param name="totalTime"></param>
    public void StartTimerVisualizer(float totalTime)
    {
        print(_timerImage.fillAmount);
        _timerImage.fillAmount -= 1.0f / totalTime * Time.deltaTime;
        _timerText.text = System.Math.Round(_timerImage.fillAmount, 1).ToString();
    }

    /// <summary>
    /// Reset Timer Visualizer
    /// </summary>
    public void ResetTimerVisualizer()
    {
        _timerImage.fillAmount = 1;
    }

    /// <summary>
    /// Check If Timer Is Done
    /// </summary>
    /// <param name="fillValue"></param>
    /// <returns>Timer Done Status </returns>
    public bool IsTimerDone(float fillValue)
    {
        if(_timerImage.fillAmount == fillValue)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
