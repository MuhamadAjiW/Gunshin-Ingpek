using UnityEngine;

public interface IAnimated{
    Transform Model {get;}
    MeshRenderer MeshRenderer {get;}
    Animator Animator {get;}
}