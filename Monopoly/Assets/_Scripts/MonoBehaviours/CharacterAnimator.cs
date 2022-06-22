using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();

        //PlayerController playerController = GetComponent<PlayerController>();
        //playerController.Walking = PlayerController_Walking;
    }

    private void PlayerController_Walking(float playerSpeed)
    {
        animator.SetBool("IsWalking", playerSpeed > 0);
        animator.SetFloat("WalkingSpeed", playerSpeed);
    }
}
