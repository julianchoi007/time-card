using UnityEngine;
using System.Linq;
public class BattleLog : MonoBehaviour
{
    public GameObject[] allyDropzones;
    public GameObject[] enemyDropzonesRow1;
    public GameObject[] enemyDropzonesRow2;
    public CreateCard createCard;
    const int numDropzones = 5;
    // Start is called before the first frame update
    void Start()
    {
        allyDropzones = new GameObject[numDropzones];
        enemyDropzonesRow1 = new GameObject[numDropzones];
        enemyDropzonesRow2 = new GameObject[numDropzones];

        GameObject allyFields = GameObject.FindWithTag("Ally Field");
        GameObject field1 = GameObject.FindWithTag("Enemy Field Row 1");
        GameObject field2 = GameObject.FindWithTag("Enemy Field Row 2");
        for (int i = 0; i < numDropzones; i++) {

            GameObject child0 = allyFields.transform.GetChild(i).gameObject;
            allyDropzones[child0.transform.GetSiblingIndex()] = child0;

            GameObject child1 = field1.transform.GetChild(i).gameObject;
            enemyDropzonesRow1[child1.transform.GetSiblingIndex()] = child1;

            GameObject child2 = field2.transform.GetChild(i).gameObject;
            enemyDropzonesRow2[child2.transform.GetSiblingIndex()] = child2;
        }
    }


    public void SummonEnemies() {
        Debug.Log("Summoning Enemies");

        for (int i = 0; i < numDropzones; i++) {
            if (enemyDropzonesRow2[i].transform.childCount == 0) {
                if (Random.Range(0, 2) == 1) {
                    createCard.CreateEnemyCard(enemyDropzonesRow2[i]);
                }
            }
        }
    }

   public void MoveEnemies() {
        Debug.Log("Moving Enemies");

        for (int i = 0; i < numDropzones; i++) {
            if (enemyDropzonesRow2[i].transform.childCount > 0) {
                if (enemyDropzonesRow1[i].transform.childCount == 0) {
                    enemyDropzonesRow2[i].transform.GetChild(0).SetParent(enemyDropzonesRow1[i].transform);
                }
            }
        }
   }
    
    public void Battle() {

        for (int i = 0; i < numDropzones; i++) {
            int childrenAlly = allyDropzones[i].transform.childCount;
            int childrenEnemy = enemyDropzonesRow1[i].transform.childCount;

            if (childrenAlly > 0 && childrenEnemy > 0) {
                Stats childAlly = allyDropzones[i].transform.GetChild(0).GetComponent<Stats>();
                Stats childEnemy = enemyDropzonesRow1[i].transform.GetChild(0).GetComponent<Stats>();

                childAlly.health -= childEnemy.attack;
                childEnemy.health -= childAlly.attack;

                if (childAlly.health <= 0) {
                    childAlly.died();
                }

                if (childEnemy.health <= 0) {
                    childEnemy.died();
                }
            }
        }

    }
}
