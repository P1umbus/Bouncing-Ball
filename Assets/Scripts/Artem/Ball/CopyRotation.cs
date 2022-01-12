using UnityEngine;

public class CopyRotation : MonoBehaviour 
{
    public Transform CopyFrom;

    void LateUpdate() 
    {
        transform.rotation = CopyFrom.rotation;
    }
}
