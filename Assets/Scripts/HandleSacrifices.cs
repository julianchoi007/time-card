using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HandleSacrifices : MonoBehaviour 
{
    public BattleLog battleLog;
    public GameObject currentCard;
    public int sacrificesCount;
    // Start is called before the first frame update
    void Start()
    {
        battleLog = GameObject.FindFirstObjectByType<BattleLog>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void beginSacrifice(GameObject current) {
        currentCard = current;
        sacrificesCount = currentCard.GetComponent<Stats>().cost;
        Draggable[] draggables = GameObject.FindObjectsOfType<Draggable>();
        foreach (Draggable draggable in draggables) {
            draggable.enabled = false;
            draggable.GetComponent<Sacrifice>().handlerSacrifice = this.gameObject;
        }
        foreach (GameObject dropzone in battleLog.allyDropzones) {
            if (dropzone.transform.childCount > 0) {
                dropzone.GetComponentInChildren<Sacrifice>().enabled = true;
            }
        }

    }

    public void endSacrifice() {
        foreach (GameObject dropzone in battleLog.allyDropzones) {
            if (dropzone.transform.childCount > 0) {
                dropzone.GetComponentInChildren<Sacrifice>().enabled = false;
            }
        }
        currentCard.GetComponent<Draggable>().enabled = true;
        currentCard.GetComponent<Stats>().cost = 0;
    }

    public void sacrifice(GameObject card) {
        sacrificesCount--;
        if (sacrificesCount == 0) {
            endSacrifice();
        }
        card.GetComponent<Stats>().died();
    }
}
