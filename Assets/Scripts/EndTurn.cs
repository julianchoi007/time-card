using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndTurn : MonoBehaviour
{
    public BattleLog battleLog;
    public Button yourButton;
    // Start is called before the first frame update
    void Start()
    {
        battleLog = FindFirstObjectByType<BattleLog>();
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TurnEnd);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TurnEnd() {
        Debug.Log("Turn Ended");
        battleLog.MoveEnemies();
        battleLog.SummonEnemies();
        battleLog.Battle();
    }
}
