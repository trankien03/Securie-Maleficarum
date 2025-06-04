using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class StagePoint : MonoBehaviour , ISaveManager
{
    public static StagePoint instance;
    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;

    }

    [SerializeField] public string stageID;
    [SerializeField] public int numberOfStage = 5;

    public SerializableDictionary<string, float> highgestPointDictionary;
    public void LoadData(GameData _data)
    {
        if (_data.highgestPoint == null || _data.highgestPoint.Count < numberOfStage)
        {
            highgestPointDictionary = new SerializableDictionary<string, float>();
            for (int i = 0; i<numberOfStage; i++)
            {
                highgestPointDictionary.Add("stage" + (i + 1), 0f);
            }
        }
        else
        {
            highgestPointDictionary = new SerializableDictionary<string, float>();
            foreach (KeyValuePair<string, float> pair in _data.highgestPoint)
            {
                highgestPointDictionary.Add(pair.Key, pair.Value);
            }
        }
        //throw new System.NotImplementedException();
    }
    public void SaveData(ref GameData _data)
    {
        _data.highgestPoint.Clear();
        //throw new System.NotImplementedException();
        foreach (KeyValuePair<string, float> pair in highgestPointDictionary) 
        {
            _data.highgestPoint.Add(pair.Key, pair.Value);
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }


    public bool applyHighestPoint(string _StageID, float newScore)
    {
        if (newScore <= highgestPointDictionary[_StageID]) return false;
        highgestPointDictionary[_StageID] = newScore;
        return true;
    }

    public float getCurrentPoint(string _StageID)
    {
        return highgestPointDictionary[_StageID];
    }
}
