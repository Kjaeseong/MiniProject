using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearAnimationChange : MonoBehaviour
{
    private AnimationState _aniState;

    private void Awake()
    {
        _aniState = GetComponent<AnimationState>();
    }

    public void ChangeAniClip(AnimationClip _animationClip)
    {
        
    }
}
