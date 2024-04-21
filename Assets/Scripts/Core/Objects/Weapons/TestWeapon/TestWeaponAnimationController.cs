using UnityEngine;

public class TestWeaponAnimationController : AnimationController{
    // Consts
    private const string ALTERNATE_ATTACK_TRIGGER = "AlternateAttack_param";

    // Attributes
    private readonly TestWeapon testWeapon;
    
    // Constructor
    public TestWeaponAnimationController(TestWeapon testWeapon) : base(testWeapon){
        this.testWeapon = testWeapon;
    }

    // Functions
    public void AnimateAlternateAttack(){
        animator.SetBool(ALTERNATE_ATTACK_TRIGGER, true);
    }
}