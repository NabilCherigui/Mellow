using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnVisualizer : MonoBehaviour
{
    /// <summary>
    /// Arrow Object
    /// </summary>
    [SerializeField]
    private GameObject _arrow;

    /// <summary>
    /// Degrees To Turn Per Second
    /// </summary>
    [SerializeField]
    private float _degreesPerSecond;

    /// <summary>
    /// Starting Gap Between Target
    /// </summary>
    [SerializeField]
    private float _startingGap;

    /// <summary>
    /// Arrow Bobbing Speed
    /// </summary>
    [SerializeField]
    private float _bobbingSpeed;

    /// <summary>
    /// Character Object
    /// </summary>
    private Character _character;

    /// <summary>
    /// Differance Between Character And Target Position
    /// </summary>
    [SerializeField]
    private Vector3 _positionDifference;

    private void Update()
    {
        HoverArrow();
    }

    /// <summary>
    /// Activate Arrow Above Character
    /// </summary>
    /// <param name="character"></param>
    public void SetArrow(Character character)
    {
        _character = character;
        _arrow.SetActive(true);
        _arrow.transform.position = new Vector3(character.transform.position.x,character.transform.position.y + _startingGap,character.transform.position.z);
    }

    /// <summary>
    /// Rotate And Bob Arrow Above Character
    /// </summary>
    public void HoverArrow()
    {
        //Rotation
        _arrow.transform.Rotate(new Vector3(0f, Time.deltaTime * _degreesPerSecond, 0f), Space.World);
        //Bobbing
        _arrow.transform.position = Vector3.Lerp(_character.transform.position + (_positionDifference / 4), _character.transform.position + (_positionDifference / 2), (Mathf.Sin(_bobbingSpeed * Time.time) + 1.0f) / 2.0f);
    }
}
