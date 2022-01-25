using UnityEngine;

public class CopyRotation : MonoBehaviour 
{
    [SerializeField] private Transform _copyFrom;

    private void LateUpdate() 
    {
        transform.rotation = _copyFrom.rotation;
    }
}
