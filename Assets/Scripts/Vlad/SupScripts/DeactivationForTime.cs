using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class DeactivationForTime : MonoBehaviour
{
    [FormerlySerializedAs("DeactivationTime")] [SerializeField] private float _deactivationTime;
    private void Start()
    {
        StartCoroutine(Deactivation());
    }
    IEnumerator Deactivation()
    {
        yield return new WaitForSeconds(_deactivationTime);
        this.gameObject.SetActive(false);
    }


}
