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
    //IEnumerator TrainOne()
    //{
    //    Clue.text = "Scroll right and increase ball horizontal force";
    //    Anim.Play("Base Layer.ScrolRight");
    //    yield return new WaitForSeconds(4f);
    //    Clue.text = "Scroll left and reduce ball horizontal force";
    //    Anim.Play("Base Layer.ScrolLeft");
    //    yield return new WaitForSeconds(4f);
    //    CloseTrain();
    //    StartCoroutine(TrainTwo());
    //}
    //IEnumerator TrainTwo()
    //{
    //    Cursor.SetActive(true);
    //    Clue.text = "Scroll up and increase ball vertical force";
    //    Anim.Play("Base Layer.ScrolUp");
    //    yield return new WaitForSeconds(4f);
    //    Clue.text = "Scroll down and reduce ball vertical force";
    //    Anim.Play("Base Layer.ScrolDown");
    //    yield return new WaitForSeconds(4f);
    //    CloseTrain();
    //}

}
