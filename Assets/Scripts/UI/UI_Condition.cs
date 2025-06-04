using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Condition : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI condition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        condition.text = GameConditionManager.instance.getCondition();
    }
}
