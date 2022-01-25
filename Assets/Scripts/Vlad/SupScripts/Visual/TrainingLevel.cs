using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class TrainingLevel : MonoBehaviour
{
    [SerializeField] private GameObject _cursor;
    private Text _clue;
    private Animator _anim;
    private const string SCROL_RIGHT = "Base Layer.ScrolRight";
    private const string SCROL_LEFT = "Base Layer.ScrolLeft";
    private const string SCROL_UP = "Base Layer.ScrolUp";
    private const string SCROL_DOWN = "Base Layer.ScrolDown";
    private void Awake()
    {
        _clue = GetComponentInChildren<Text>();
    }
    private void Start()
    {
        _anim = _cursor.GetComponent<Animator>();
        StartCoroutine(TrainOne());
    }

    public void CloseTrain()
    {
        _cursor.SetActive(false);
        _clue.text = "";
    }
    IEnumerator TrainOne()
    {
        var waitTime = new WaitForSeconds(3f);

        _anim.Play(SCROL_RIGHT);
        yield return waitTime;

        _anim.Play(SCROL_LEFT);
        yield return waitTime;

        _anim.Play(SCROL_UP);
        yield return waitTime;

        _anim.Play(SCROL_DOWN);
        yield return waitTime;
        CloseTrain();
    }
}
