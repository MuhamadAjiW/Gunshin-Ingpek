using UnityEngine;

public class SwordAnimationController : AnimationController
{
    private const string ALTERNATE_ATTACK_TRIGGER = "AlternateAttack_param";

    private readonly Sword sword;

    public SwordAnimationController(Sword sword) : base(sword)
    {
        this.sword = sword;
    }

    public void AnimateAlternateAttack()
    {
        animator.SetTrigger(ALTERNATE_ATTACK_TRIGGER);
    }
}
