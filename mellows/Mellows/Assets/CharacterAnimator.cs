using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimationControllerIDs {
    IDLE = 0,
    ATTACK = 1,
    SPECIAL_ATTACK = 1
}

public class CharacterAnimator :MonoBehaviour
{
    /// <summary>
    /// The animation objects
    /// </summary>
    [SerializeField] private GameObject[] _animations;
    /// <summary>
    /// The current animation id
    /// </summary>
    private int currentAnimationId = (int)AnimationControllerIDs.IDLE;
    /// <summary>
    /// A flag if the animation shoudl return to the idle animation
    /// </summary>
    private bool shouldGoBackToIdle = false;

    /// <summary>
    /// Sets the controller to the idle animation
    /// </summary>
    void Start() {
        SetAnimation(AnimationControllerIDs.IDLE);
    }

    /// <summary>
    /// Handles the going back to the idle animation
    /// </summary>
    void Update() {
        if (shouldGoBackToIdle) {
            var animator = _animations[currentAnimationId].GetComponent<Animator>();
            print(animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !animator.IsInTransition(0)) {
                SetAnimation(AnimationControllerIDs.IDLE);
                shouldGoBackToIdle = false;
            }
        }
    }

    /// <summary>
    /// Sets the animtion to the selected animation
    /// </summary>
    /// <param name="id"></param>
    /// <param name="backToIdle"></param>
    public void SetAnimation(AnimationControllerIDs id, bool backToIdle = false) {
        for (int i = 0; i < _animations.Length; i += 1) {
            _animations[i].SetActive((int)id == i);
            currentAnimationId = (int)id;
        }

        shouldGoBackToIdle = backToIdle;
    }
}
