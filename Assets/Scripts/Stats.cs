using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    // Start is called before the first frame update
    public int attack;
    public int health;
    public int cost;
    public TMPro.TextMeshProUGUI cardTextStats;
    public TMPro.TextMeshProUGUI cardTextCost;

    void Start()
    {
        attack = Random.Range(0, 3);
        health = Random.Range(1, 4);
        cost = Random.Range(0, 3);
        cardTextCost.text = cost.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        cardTextStats.text = attack + " / " + health;
    }

    public void died() {
        GetComponentInParent<Dropzone>().used = false;
        GetComponentInParent<Dropzone>().enabled = true;
        
        Destroy(gameObject);
    }
}
