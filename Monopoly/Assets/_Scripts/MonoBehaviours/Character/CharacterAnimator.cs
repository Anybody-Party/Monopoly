using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private string IsLose = "IsLose";
    private string IsWon = "IsWon";
    private string IsJump = "IsJump";

    private void Start()
    {
    }

    public void Lose()
    {
        animator.SetTrigger(IsLose);
    }

    public void Won()
    {
        animator.SetTrigger(IsWon);
    }

    public void Jump()
    {
        animator.SetTrigger(IsJump);
    }
}
