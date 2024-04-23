using UnityEngine;

public interface IAnimated
{
    // Set-Getters
    Transform Model {get;}
    MeshRenderer MeshRenderer {get;}
    Animator Animator {get;}
}