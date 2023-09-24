using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Sacrifice : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {

    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot;
    private Color originalColor;
    public GameObject handlerSacrifice;
    public void OnPointerClick(PointerEventData eventData) {
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
        handlerSacrifice.GetComponent<HandleSacrifices>().sacrifice(this.gameObject);
    }

    public void OnPointerEnter(PointerEventData eventData) {
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        GetComponent<Image>().color = new Color32(255, 105, 97, 200); // red colour
    }

    public void OnPointerExit(PointerEventData eventData) {
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
        GetComponent<Image>().color = originalColor;
    }

    // Start is called before the first frame update
    void Start()
    {
        Image panelImage = GetComponent<Image>();
        if (panelImage != null) {
            originalColor = panelImage.color; // Store the original color
        }
        hotSpot = new Vector2(cursorTexture.width / 2f, cursorTexture.height / 2f);
        this.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
