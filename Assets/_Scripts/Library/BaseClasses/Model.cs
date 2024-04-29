using System;
using UnityEngine;

public class Model : MonoBehaviour
{
    // Attributes
    public Transform dynamicBottomPoint;
    [HideInInspector] public new Renderer renderer;
    [HideInInspector] public float bottomOffset;
    
    // Set-getters
    public Vector3 Bottom => dynamicBottomPoint == null? new(transform.position.x, transform.position.y - bottomOffset, transform.position.z) : dynamicBottomPoint.position;

    // Constructors
    protected void Start(){
        renderer = GetComponent<Renderer>();

        #if STRICT
        if (renderer == null)
        {
            Debug.LogError($"Did you seriously just create a model {name} without a renderer? How to resolve: Add a renderer (meshrenderer or skinnedmeshrenderer) to the model or in its children");
        }
        #endif
        bottomOffset = renderer.bounds.extents.y;
    }
}
