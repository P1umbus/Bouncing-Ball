using UnityEngine;
using UnityEngine.EventSystems;


public class TrainingLevel : MonoBehaviour, IDragHandler
{
    [SerializeField] private GameObject Ball;
    [SerializeField]private int MaxAttemptsNumbs;
    private int AttemptsNumbs;
    public void OnDrag(PointerEventData eventData)
    {
        AttemptsNumbs++;
        Debug.Log(AttemptsNumbs);
    }
}
