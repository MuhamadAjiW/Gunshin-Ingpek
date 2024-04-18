using UnityEngine;

public class DummyAnimationController : AnimationController{
    // Consts
    private const string HIT_TRIGGER = "hit_param";

    // Attributes
    private readonly Dummy dummy;

    // Constructor
    public DummyAnimationController(Dummy dummy){
        this.dummy = dummy;
        model = dummy.transform.Find("Model");
        animator = model.GetComponent<Animator>();
        meshRenderer = model.GetComponent<MeshRenderer>();

        dummy.OnDamagedEvent += IndicateDamaged;
    }

    // Functions
    private void IndicateDamaged(){
        Debug.Log("Dummy is damaged");
        meshRenderer.material.color = Color.red;
    }
}