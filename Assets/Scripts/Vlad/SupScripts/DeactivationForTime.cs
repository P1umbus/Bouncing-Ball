using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivationForTime : MonoBehaviour
{
    [SerializeField] private float DeactivationTime;
    private void Start()
    {
        StartCoroutine(Deactivation());
    }
    IEnumerator Deactivation()
    {
        yield return new WaitForSeconds(DeactivationTime);
        this.gameObject.SetActive(false);
    }


}
