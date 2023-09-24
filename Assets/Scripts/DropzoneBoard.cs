using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro.EditorUtilities;

public class DropzoneBoard : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler {

    public GameObject panel; // Reference to your UI panel
    private Color originalColor; // Store the original color

    void Start() {
        panel = this.gameObject;
        Image panelImage = panel.GetComponent<Image>();
        if (panelImage != null) {
            originalColor = panelImage.color; // Store the original color
        }
    }
    public void changeColorGreen() {
        Image panelImage = panel.GetComponent<Image>();
        if (panelImage != null) {
            panelImage.color = new Color32(144, 238, 144, 90); // light green with some opacity
        }
    }
    public void changeColorBack() {
        Image panelImage = panel.GetComponent<Image>();
        if (panelImage != null) {
            panelImage.color = originalColor; // Restore the original color
        }
    }
    public void OnPointerEnter(PointerEventData eventData) {
    }
    public void OnPointerExit(PointerEventData eventData) {
    }
    public void OnDrop(PointerEventData eventData) {
        Debug.Log(eventData.pointerDrag.name + " dropped on " + gameObject.name);
        eventData.pointerDrag.GetComponentInParent<Draggable>().parentToReturnTo = this.transform.GetChild(0).transform;

    }
}
