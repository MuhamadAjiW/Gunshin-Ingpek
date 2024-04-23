using UnityEngine;

public class WorldObject : MonoBehaviour
{
    // Attributes
    private static int autoIncrement = 0;
    private int numberId; 
    private string prefix;
    public string id;

    // Set-Getters
    public Vector3 Front => transform.rotation * Vector3.forward;
    public Quaternion Rotation => transform.rotation;

    // Constructor
    protected void Awake()
    {
        numberId = autoIncrement;
        id = numberId.ToString();
        autoIncrement++;
    }

    // Functions
    protected void SetIdPrefix(string prefix)
    {
        this.prefix = prefix;
        if(this.prefix == "")
        {
            id = numberId.ToString();
        } 
        else
        {
            id = this.prefix + "_" + id;
        }
    }
}
