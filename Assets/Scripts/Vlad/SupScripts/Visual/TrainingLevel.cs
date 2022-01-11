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
        Anim.Play("Base Layer.ScrolRight");
        yield return new WaitForSeconds(3f);
        Anim.Play("Base Layer.ScrolLeft");
        yield return new WaitForSeconds(3f);
        Anim.Play("Base Layer.ScrolUp");
        yield return new WaitForSeconds(3f);
        Anim.Play("Base Layer.ScrolDown");
        yield return new WaitForSeconds(3f);
        CloseTrain();
    }
}
