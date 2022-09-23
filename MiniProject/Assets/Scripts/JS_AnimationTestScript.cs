using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JS_AnimationTestScript : MonoBehaviour
{
    public Animator _animator;
    public AnimationState _aniState;
    public int status;
    
    public List<AnimationClip> Anilist = new List<AnimationClip>();




    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.A))
        {

        }
        
    }
}
