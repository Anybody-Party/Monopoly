using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private string IsLose = "IsLose";
    private string IsWon = "IsWon";
    private string IsJump = "IsJump";
    private string IsFall = "IsFall";
    private string IsSad = "IsSad";
    private string IsHappy = "IsHappy";

    public void Lose() => animator.SetTrigger(IsLose);

    public void Won() => animator.SetTrigger(IsWon);

    public void Jump()
    {
        Utility.ResetAnimtor(animator);
        animator.SetTrigger(IsJump);
    }

    public void Sad() => animator.SetTrigger(IsSad);

    public void Happy() => animator.SetTrigger(IsHappy);

    public void Fall() => animator.SetTrigger(IsFall);
}
