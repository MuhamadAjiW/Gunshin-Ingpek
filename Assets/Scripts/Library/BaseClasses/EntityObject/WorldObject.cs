using UnityEngine;

public class WorldObject : MonoBehaviour{
    // Attributes
    private static int autoIncrement = 0;
    private int NumberId; 
    private string Prefix;
    public string Id;

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
