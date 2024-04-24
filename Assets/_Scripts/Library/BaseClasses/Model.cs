using UnityEngine;

public class Model : MonoBehaviour
{
    // Attributes
    public Vector3 staticWeaponPivot;
    public new Renderer renderer;
    public float bottomOffset;
    
    // Set-getters
    public Vector3 WeaponPivot => staticWeaponPivot;
    public Vector3 Bottom => new(transform.position.x, transform.position.y - bottomOffset, transform.position.z);

    // Constructors
    protected void Start(){
        Renderer renderer = GetComponent<Renderer>();
    
        #if STRICT
        if(renderer == null)
        {
            Debug.LogError($"Did you seriously just create a model {name} without a renderer? How to resolve: Add a renderer (meshrenderer or skinnedmeshrenderer) to the model");
        }
        #endif
        bottomOffset = renderer.bounds.extents.y;
    }
}
