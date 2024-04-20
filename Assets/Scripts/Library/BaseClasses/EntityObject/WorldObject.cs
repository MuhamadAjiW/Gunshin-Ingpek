using UnityEngine;

public class WorldObject : MonoBehaviour{
    // Attributes
    private static int autoIncrement = 0;
    private int NumberId; 
    private string Prefix;
    public string Id;
    public Vector3 Front => transform.rotation * Vector3.forward;
    public Quaternion Rotation => transform.rotation;

    // Constructor
    protected void Awake(){
        NumberId = autoIncrement;
        Id = NumberId.ToString();
        autoIncrement++;
    }

    // Functions
    protected void SetIdPrefix(string prefix){
        Prefix = prefix;
        if(Prefix == "") Id = NumberId.ToString();
        else Id = Prefix + "_" + Id;
    }
}
