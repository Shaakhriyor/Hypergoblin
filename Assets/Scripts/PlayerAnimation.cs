using UnityEngine;

public class PlayerAnimatorFix : MonoBehaviour
{
    void Start()
    {
        Animator anim = GetComponentInChildren<Animator>();
        if (anim != null)
        {
            anim.Play("mixami_com");
        }
    }
}