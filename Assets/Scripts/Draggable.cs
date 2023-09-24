using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

    public Transform parentToReturnTo = null;
    GameObject placeholder = null;
    public BattleLog battleLog;
    public GameObject hand;
    void Start() {
        battleLog = GameObject.FindFirstObjectByType<BattleLog>();
        hand = GameObject.FindWithTag("Hand");
    }

    public void OnBeginDrag(PointerEventData eventData) {
        Debug.Log("OnBeginDrag");

        placeholder = new GameObject();
        placeholder.transform.SetParent(this.transform.parent);
        LayoutElement le = placeholder.AddComponent<LayoutElement>();
        le.preferredWidth = this.GetComponent<LayoutElement>().preferredWidth;
        le.preferredHeight = this.GetComponent<LayoutElement>().preferredHeight;
        le.flexibleHeight = 0;
        le.flexibleWidth = 0;

        placeholder.transform.SetSiblingIndex(this.transform.GetSiblingIndex());

        placeholder.transform.SetParent(this.transform.parent);

        parentToReturnTo = this.transform.parent;
        this.transform.SetParent(this.transform.parent.parent);

        GetComponent<CanvasGroup>().blocksRaycasts = false;
        int cost = GetComponent<Stats>().cost;
        if (cost > 0 ) {
            DropzoneBoard board = GameObject.FindFirstObjectByType<DropzoneBoard>();
            board.GetComponent<DropzoneBoard>().enabled = true;
            board.GetComponent<DropzoneBoard>().changeColorGreen();
            board.GetComponent<Image>().raycastTarget = true;
        } else {
            Dropzone[] zones = GameObject.FindObjectsOfType<Dropzone>();
            foreach (Dropzone zone in zones) {
                zone.changeColorGreen();
            }
        }
    }


    public void OnDrag(PointerEventData eventData) {
        this.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData) {
        Debug.Log("OnEndDrag");
        this.transform.SetParent(parentToReturnTo);
        this.transform.SetSiblingIndex(placeholder.transform.GetSiblingIndex());
        GetComponent<CanvasGroup>().blocksRaycasts = true;

        int cost = GetComponent<Stats>().cost;
        if (cost > 0) {
            DropzoneBoard board = GameObject.FindFirstObjectByType<DropzoneBoard>();
            board.GetComponent<DropzoneBoard>().changeColorBack();
            board.GetComponent<Image>().raycastTarget = false;
            if (placeholder.transform.parent != this.transform.parent) {
                board.GetComponent<HandleSacrifices>().beginSacrifice(this.gameObject);
            }
            board.GetComponent<DropzoneBoard>().enabled = false;
            
        } else {
            Transform parent = this.transform.parent;
            Dropzone[] zones = GameObject.FindObjectsOfType<Dropzone>();
            foreach (Dropzone zone in zones) {
                zone.changeColorBack();
            }
            if (placeholder.transform.parent != parent) {

                GetComponent<Draggable>().enabled = false;
                GetComponentInParent<Dropzone>().enabled = false;
                foreach (Draggable draggable in hand.GetComponentsInChildren<Draggable>()) {
                    draggable.enabled = true;
                }
            }
        }
        Destroy(placeholder);

    }
}
