using UnityEngine;

public abstract class AnimationController {
    // Attributes
    public Transform model;
    public MeshRenderer meshRenderer;
    public Animator animator;

    public AnimationController(MonoBehaviour animable){
        model = animable.transform.Find("Model");
        animator = model.GetComponent<Animator>();
        meshRenderer = model.GetComponent<MeshRenderer>();

        if(model == null) Debug.LogWarning("Animated object of " + animable.name + " does not have a model");
        if(animator == null) Debug.LogWarning("Animated object of " + animable.name + " does not have an animator in its model");
        if(meshRenderer == null) Debug.LogWarning("Animated object of " + animable.name + " does not have an meshRenderer in its model");
    }
}