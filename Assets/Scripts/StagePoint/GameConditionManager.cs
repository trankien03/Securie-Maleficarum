using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameConditionManager : MonoBehaviour
{
    public static GameConditionManager instance;

    [SerializeField] private GameObject winUI;
    [SerializeField] private TextMeshProUGUI takenHit;
    [SerializeField] private TextMeshProUGUI timeRemaining;
    [SerializeField] private TextMeshProUGUI bonus;
    [SerializeField] private TextMeshProUGUI total;
    [SerializeField] private TextMeshProUGUI stageRating;


    public string stageIndex;
    public int enemyDeathCount = 0;
    public int maxEnemyCondition = 15;

    public int bossDeathCount = 0;
    public int maxBossCondition = 0;
    private float timer;
    public int hitTaken;
    // Start is called before the first frame update

    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;

    }

    void Start()
    {
       
        enemyDeathCount = 0;
        hitTaken = 0;
        timer = 3f;
    }

    // Update is called once per frame
    public void addingEnemyCount()
    {
        enemyDeathCount++;
    }
    public void addingBossCount()
    {
        bossDeathCount++;
    }
    public void takeHit()
    {
        hitTaken++;
    }

    private void ShowResult()
    {
        winUI.SetActive(true);

        float totalPoint = 1000;
        GameManager.instance.PauseGame(true);
        //TimeRemainning
        float timeRe = UI_Timer.instance.getTime();
        if (timeRe > 120)
        {
            totalPoint += 1000;
            timeRemaining.text = "Time remaining >120 sec: " + Mathf.FloorToInt(timeRe) + " (+1000)";
        }
        else
        {
            timeRemaining.text = "Time remaining >120 sec: " + Mathf.FloorToInt(timeRe) + " (Failed)";
        }
        //Hit Taken
        if (hitTaken <= 20)
        {
            totalPoint += 1000;
            takenHit.text = "Take less than 20 hit: " + hitTaken + "/20 (+1000)";
        }
        else
        {
            takenHit.text = "Take less than 20 hit: " + hitTaken + "/20 (Failed)";
        }
        //Bonus
        float bonusPoint = (100 - hitTaken * 5) + (timeRe * 3);
        totalPoint += bonusPoint;
        bonus.text = "Bonus: " + Mathf.FloorToInt(bonusPoint);
        //Total
        if (totalPoint > StagePoint.instance.getCurrentPoint(stageIndex))
        {
            StagePoint.instance.applyHighestPoint(stageIndex, totalPoint);
            total.text = "Total: " + Mathf.FloorToInt(totalPoint) + " (NEW)";
            SaveManager.instance.SaveGame();
        }
        else
        {
            total.text = "Total: " + Mathf.FloorToInt(totalPoint);
        }
        //Rating
        string rating = "F";

        if (totalPoint < 2000)
        {
            rating = "C";
        }
        if (totalPoint >= 2000 && totalPoint < 2500)
        {
            rating = "B";
        }
        if (totalPoint >= 2500 && totalPoint < 3000)
        {
            rating = "A";
        }
        if (totalPoint >= 3000 && totalPoint < 3500)
        {
            rating = "S";
        }
        if (totalPoint >= 3500)
        {
            rating = "SS";
        }


        stageRating.text = "Rating: " + rating;
    } 

    public string getCondition()
    {
        if (maxBossCondition == 0)
        {
            string str = "DEFEATED: " + enemyDeathCount + "/" + maxEnemyCondition;
            return str;
        }
        string str1 = "BOSS: " + bossDeathCount + "/" + maxBossCondition;
        return str1;
    }
    void FixedUpdate()
    {
        if ((maxBossCondition == 0 && enemyDeathCount >= maxEnemyCondition) 
            || (maxBossCondition != 0 && bossDeathCount >= maxBossCondition))
        {
            timer = timer - Time.deltaTime;
            if (timer <=0 ) ShowResult();
        }
    }


}
