using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CreateCard : MonoBehaviour {
    public Button yourButton;
    public GameObject allyCard;
    public GameObject enemyCard;
    public GameObject hand;

    void Start() {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(CreateAllyCard);
    }

    public void CreateAllyCard() {
        GameObject newCard = Instantiate(allyCard);
        newCard.transform.SetParent(hand.transform);
    }

    public void CreateEnemyCard(GameObject dropzone) {
        GameObject newCard = Instantiate(enemyCard);
        newCard.transform.SetParent(dropzone.transform);
    }
}