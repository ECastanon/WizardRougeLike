using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventChecking : MonoBehaviour, ISelectHandler, IPointerEnterHandler
{
    RectTransform buttonTransform;
    [SerializeField] private SelectionFollower arrowPointer;

    void Start()
    {
        buttonTransform = GetComponent<RectTransform>();
    }

    public void OnSelect(BaseEventData eventData)
    {
        arrowPointer.buttonPos = buttonTransform.localPosition.y;
        arrowPointer.GoToButton();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        arrowPointer.buttonPos = buttonTransform.localPosition.y;
        arrowPointer.GoToButton();
    }
}