using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Jump()
    {
        //Debug.Log("Salto");
        animator.SetTrigger("saltar");
    }

    public void Collision()
    {
        //Debug.Log("Golpeado por obstaculo");
        animator.SetTrigger("hit");
    }
}
