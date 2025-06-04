using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_MainMenu : MonoBehaviour
{
    [SerializeField] private string sceneName = "MainScene";
    [SerializeField] private GameObject selectStageUI;
    [SerializeField] private GameObject mainMenuUI;
    [SerializeField] private GameObject tutorialMenuUI;
    [SerializeField] UI_FadeScreen fadeScreen;



    [SerializeField] private TextMeshProUGUI stage1;
    [SerializeField] private TextMeshProUGUI stage2;
    [SerializeField] private TextMeshProUGUI stage3;
    [SerializeField] private TextMeshProUGUI stage4;
    [SerializeField] private TextMeshProUGUI stage5;
    private void Start()
    {
        selectStageUI.SetActive(false);
        tutorialMenuUI.SetActive(false);
        mainMenuUI.SetActive(true);
        SaveManager.instance.findAndLoad();
    }

    public void ClearData()
    {

        SaveManager.instance.DeleteSavedData();
    }
    private string getIndex(float x)
    {
        if (x == 0) return "";
        if (x < 2500) return "B";
        if (x <= 3000) return "A";
        if (x <= 3500) return "S";
        return "SS";
    }

    private string getPoint(float x)
    {
        string str = Mathf.FloorToInt(x) + " (" + getIndex(x) + ")";
        return str;
    }
    public void FixedUpdate()
    {
        SaveManager.instance.findAndLoad();
        Debug.Log(StagePoint.instance.getCurrentPoint("stage1"));
        stage1.text = getPoint(StagePoint.instance.getCurrentPoint("stage1"));

        stage2.text = getPoint(StagePoint.instance.getCurrentPoint("stage2"));
        stage3.text = getPoint(StagePoint.instance.getCurrentPoint("stage3"));
        stage4.text = getPoint(StagePoint.instance.getCurrentPoint("stage4"));
        stage5.text = getPoint(StagePoint.instance.getCurrentPoint("stage5"));

    }
    public void ExitGame()
    {
        Debug.Log("Exit game");
        Application.Quit();
    }

    public void SelectStage()
    {
        selectStageUI.SetActive(true);
        mainMenuUI.SetActive(false);
        tutorialMenuUI.SetActive(false);
    }
    public void Back()
    {
        selectStageUI.SetActive(false);
        tutorialMenuUI.SetActive(false);
        mainMenuUI.SetActive(true);
    }
    public void Tutorial()
    {
        selectStageUI.SetActive(false);
        tutorialMenuUI.SetActive(true);
        mainMenuUI.SetActive(false);
    }
    public void Stage1()
    {
        SaveManager.instance.SaveGame();
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }
    public void Stage2()
    {
        SaveManager.instance.SaveGame();
        Time.timeScale = 1f;
        SceneManager.LoadScene(2);
    }
    public void Stage3()
    {
        SaveManager.instance.SaveGame();
        Time.timeScale = 1f;
        SceneManager.LoadScene(3);
    }
    public void Stage4()
    {
        SaveManager.instance.SaveGame();
        Time.timeScale = 1f;
        SceneManager.LoadScene(4);
    }
    public void Stage5()
    {
        SaveManager.instance.SaveGame();
        Time.timeScale = 1f;
        SceneManager.LoadScene(5);
    }

    IEnumerator LoadSceneWithFadeEffect(float _delay)
    {
        fadeScreen.FadeOut();

        yield return new WaitForSeconds(_delay);

        SceneManager.LoadScene(sceneName);
    }
}
