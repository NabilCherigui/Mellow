using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetArrow : MonoBehaviour
{
    /// <summary>
    /// Degrees To Turn Per Second
    /// </summary>
    [SerializeField]
    private float _degreesPerSecond;

    /// <summary>
    /// Starting Distance Between Arrow And Target
    /// </summary>
    [SerializeField]
    private float _startingGap;

    /// <summary>
    /// Arrow Bobbing Speed
    /// </summary>
    [SerializeField]
    private float _bobbingSpeed;

    /// <summary>
    /// Target Character
    /// </summary>
    private Character _character;

    /// <summary>
    /// Differance Between Arrow And Character Position
    /// </summary>
    [SerializeField]
    private Vector3 _positionDifference;

    private void Update()
    {
        HoverArrow();
    }

    /// <summary>
    /// Activate Target Arrow Above Target
    /// </summary>
    /// <param name="character"></param>
    public void SetArrow(Character character)
    {
        gameObject.SetActive(true);
        transform.position = new Vector3(character.transform.position.x,character.transform.position.y + _startingGap,character.transform.position.z);
        _character = character;
    }

    /// <summary>
    /// Rotate And Bob Target Arrow Above Character
    /// </summary>
    public void HoverArrow()
    {
        //Rotation
        transform.Rotate(new Vector3(0f, Time.deltaTime * _degreesPerSecond, 0f), Space.World);
        //Bobbing
        transform.position = Vector3.Lerp(_character.transform.position + (_positionDifference / 4), _character.transform.position + (_positionDifference / 2), (Mathf.Sin(_bobbingSpeed * Time.time) + 1.0f) / 2.0f);
    }
}
