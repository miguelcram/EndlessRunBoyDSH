using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class AnimatorSwitcher : MonoBehaviour
{
    public GameObject timmy;
    public string Animacion1;
    public string Animacion2;
    public float forwardDistance = 0.5f;

    private Animator animator;
    private bool cambioAnimacion = false;
    
    void Start()
    {
        if(timmy != null)
        {
            animator = timmy.GetComponent<Animator>();
        }
    }

    void Update()
    {
        if(!cambioAnimacion && IsAnimationFinished(Animacion1))
        {
            SwitchAnimation();
            cambioAnimacion = true;
        }
    }

    bool IsAnimationFinished(string animationName)
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        return stateInfo.IsName(animationName) && stateInfo.normalizedTime >= 1.0f;
    }

    void SwitchAnimation()
    {
        transform.Rotate(0, 180, 0);
        transform.Translate(0, 0, forwardDistance);
        animator.Play(Animacion2);
    }
}
