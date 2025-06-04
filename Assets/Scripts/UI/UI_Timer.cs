using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Timer : MonoBehaviour
{
    public static UI_Timer instance;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private float timeLimit = 120;
    private float timeRemaining;
    // Start is called before the first frame update
    void Awake()
    {
        timeRemaining = timeLimit;
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;
    }

    // Update is called once per frame
    void Update()
    {

        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }
        else
        {
            timeRemaining = 0;
        }
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);


        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public float getTime()
    {
        return timeRemaining;
    }
}
