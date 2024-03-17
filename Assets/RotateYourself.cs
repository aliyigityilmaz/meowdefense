using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateYourself : MonoBehaviour
{
   public Animator animator;
   
    public void animate()
    {
        animator.SetBool("isAwake",true);
    }
}
