using UnityEngine;

public abstract class AnimationController {
    // Attributes
    public Transform model;
    public MeshRenderer meshRenderer;
    public Animator animator;

    public AnimationController(MonoBehaviour dummy){
        model = dummy.transform.Find("Model");
        animator = model.GetComponent<Animator>();
        meshRenderer = model.GetComponent<MeshRenderer>();
    }
}