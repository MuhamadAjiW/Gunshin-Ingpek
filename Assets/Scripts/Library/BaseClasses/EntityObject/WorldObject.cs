using UnityEngine;

public class WorldObject : MonoBehaviour{
    private static int autoIncrement = 0;
    public string Id;

    protected void Awake(){
        Id = autoIncrement.ToString();
        autoIncrement++;
    }
}