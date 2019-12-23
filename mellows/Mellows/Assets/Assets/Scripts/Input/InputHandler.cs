using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    /// <summary>
    /// Vector 2 Start And End Position
    /// </summary>
    [SerializeField] private Vector2 _startPosistion, _endPosition;

    /// <summary>
    /// Current Combination
    /// </summary>
    public Combination CurrentCombination;

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
    /// Translate Mouse And Touch Input To Input Usable In A Combination
    /// </summary>
    /// <param name="touchFingerId"></param>
    /// <param name="touchPosition"></param>
    /// <param name="touchPhase"></param>
    private void HandleTouch(int touchFingerId, Vector3 touchPosition, TouchPhase touchPhase) {
        switch (touchPhase) {
        case TouchPhase.Began:
            _startPosistion = touchPosition;
            break;
        case TouchPhase.Moved:
            break;
        case TouchPhase.Ended:
            _endPosition = touchPosition;

            if(Mathf.Abs(_startPosistion.x - _endPosition.x) > Mathf.Abs(_startPosistion.y - _endPosition.y) && Mathf.Abs(_startPosistion.x - _endPosition.x) > 30)
            {
                if (_startPosistion.x < _endPosition.x)
                {
                    CurrentCombination.Inputs.Add(InputTypes.RIGHT);
                }
                else if(_startPosistion.x > _endPosition.x)
                {
                    CurrentCombination.Inputs.Add(InputTypes.LEFT);
                }
            }
            else if(Mathf.Abs(_startPosistion.x - _endPosition.x) < Mathf.Abs(_startPosistion.y - _endPosition.y) && Mathf.Abs(_startPosistion.y - _endPosition.y) > 30)
            {
                if (_startPosistion.y < _endPosition.y)
                {
                    CurrentCombination.Inputs.Add(InputTypes.UP);
                }
                else if(_startPosistion.y > _endPosition.y)
                {
                    CurrentCombination.Inputs.Add(InputTypes.DOWN);
                }
            }
            else
            {
                CurrentCombination.Inputs.Add(InputTypes.TAP);
            }
            break;
        }
    }
}
