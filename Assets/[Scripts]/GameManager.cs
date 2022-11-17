using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{

    public GameObject timerTextGO;
    TextMeshProUGUI timerText;

    public int timer = 20;
    void Start()
    {
        timerText = timerTextGO.GetComponent<TextMeshProUGUI>();

        StartCoroutine(ReduceTime());
    }

    IEnumerator ReduceTime()
    {
        yield return new WaitForSeconds(1f);

        timer--;

        StartCoroutine(ReduceTime());
    }

    void Update()
    {
        timerText.SetText(timer.ToString());
    }
}
