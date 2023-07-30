using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionFollower : MonoBehaviour
{
    float elapsedTime = 0f;
    float duration = 0.5f;
    float percComplete = 0;

    public float buttonPos;
    float originalPosition;
    public Vector2 newPos;

    RectTransform myRectTransform;

    void Start()
    {
        myRectTransform = this.GetComponent<RectTransform>();
        newPos.x = myRectTransform.localPosition.x;
    }

    public void GoToButton()
    {
        originalPosition = myRectTransform.localPosition.y;
        elapsedTime = 0;
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        percComplete = elapsedTime / duration;
        Mathf.Clamp(elapsedTime, 0f, duration);
        newPos.y = Mathf.Lerp(originalPosition, buttonPos, percComplete);
        myRectTransform.localPosition = newPos;
    }
}