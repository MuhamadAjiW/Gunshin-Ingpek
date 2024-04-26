using UnityEngine;

public interface IAnimated
{
    // Set-Getters
    Transform Model {get;}
    Renderer renderer {get;}
    Animator Animator {get;}
}