using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class TrainingLevel : MonoBehaviour
{
    [SerializeField] private GameObject Cursor;
    private Text Clue;
    private Animator Anim;
    private const string SCROL_RIGHT = "Base Layer.ScrolRight";
    private const string SCROL_LEFT = "Base Layer.ScrolLeft";
    private const string SCROL_UP = "Base Layer.ScrolUp";
    private const string SCROL_DOWN = "Base Layer.ScrolDown";
    private void Awake()
    {
        Clue = GetComponentInChildren<Text>();
    }
    private void Start()
    {
        Anim = Cursor.GetComponent<Animator>();
        StartCoroutine(TrainOne());
    }

    public void CloseTrain()
    {
        Cursor.SetActive(false);
        Clue.text = "";
    }
    IEnumerator TrainOne()
    {
        var waitTime = new WaitForSeconds(3f);

        Anim.Play(SCROL_RIGHT);
        yield return waitTime;

        Anim.Play(SCROL_LEFT);
        yield return waitTime;

        Anim.Play(SCROL_UP);
        yield return waitTime;

        Anim.Play(SCROL_DOWN);
        yield return waitTime;
        CloseTrain();
    }
}
