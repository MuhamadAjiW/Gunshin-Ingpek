using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour{
    // Static instance
    public static EnvironmentManager instance;

    // Constructor
    protected void Awake(){
        instance = this;
    }
}
