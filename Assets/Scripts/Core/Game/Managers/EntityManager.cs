using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : MonoBehaviour{
    // Static instance
    public static EntityManager instance;

    // Constructor
    protected void Awake(){
        instance = this;
    }
}
