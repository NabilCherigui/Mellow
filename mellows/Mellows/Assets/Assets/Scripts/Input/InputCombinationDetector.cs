using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputCombinationDetector : MonoBehaviour
{
    /// <summary>
    /// Vector2 Start, End, Last Move, And Current Move Position
    /// </summary>
    [SerializeField] private Vector2 _startPosistion, _endPosition, _lastMovePosition, _currentMovePosition;

    /// <summary>
    /// Combo List
    /// </summary>
    [SerializeField] private List<string> _combo;

    /// <summary>
    /// Text
    /// </summary>
    /// <returns></returns>
    [SerializeField] private Text _text;

    void Update ()
    {
        // Handle native touch events
        foreach (Touch touch in Input.touches)
        {
            HandleTouch(touch.fingerId, touch.position, touch.phase);
        }

        // Simulate touch events from mouse events
        if (Input.touchCount == 0)
        {
            //Left Mouse Button
            if (Input.GetMouseButtonDown(0))
            {
                HandleTouch(10, Input.mousePosition, TouchPhase.Began);
            }
            else if (Input.GetMouseButton(0))
            {
                HandleTouch(10, Input.mousePosition, TouchPhase.Moved);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                HandleTouch(10, Input.mousePosition, TouchPhase.Ended);
            }
        }
    }

    /// <summary>
    /// Detect Input Type And Direction To Be Used For A Combo
    /// </summary>
    /// <param name="touchFingerId"></param>
    /// <param name="touchPosition"></param>
    /// <param name="touchPhase"></param>
    private void HandleTouch(int touchFingerId, Vector3 touchPosition, TouchPhase touchPhase) {
        switch (touchPhase) {
        case TouchPhase.Began:
            _startPosistion = touchPosition;
            _lastMovePosition = touchPosition;
            break;
        case TouchPhase.Moved:
            _currentMovePosition = touchPosition;
            _lastMovePosition = _currentMovePosition;
            break;
        case TouchPhase.Ended:
            _endPosition = touchPosition;

            if(Mathf.Abs(_startPosistion.x - _endPosition.x) > Mathf.Abs(_startPosistion.y - _endPosition.y) && Mathf.Abs(_startPosistion.x - _endPosition.x) > 30)
            {
                if (_startPosistion.x < _endPosition.x)
                {
                    _combo.Add("RIGHT");
                    _text.text += "RIGHT/";
                }
                else if(_startPosistion.x > _endPosition.x)
                {
                    _combo.Add("LEFT");
                    _text.text += "LEFT/";
                }
            }
            else if(Mathf.Abs(_startPosistion.x - _endPosition.x) < Mathf.Abs(_startPosistion.y - _endPosition.y) && Mathf.Abs(_startPosistion.y - _endPosition.y) > 30)
            {
                if (_startPosistion.y < _endPosition.y)
                {
                    _combo.Add("UP");
                    _text.text += "UP/";
                }
                else if(_startPosistion.y > _endPosition.y)
                {
                    _combo.Add("DOWN");
                    _text.text += "DOWN/";
                }
            }
            else
            {
                _combo.Add("TAP");
                _text.text += "TAP/";
            }
            break;
        }
    }
}
