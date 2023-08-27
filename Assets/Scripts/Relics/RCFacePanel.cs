using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RCFacePanel : MonoBehaviour
{
    public GameObject rcMenu;
    // Update is called once per frame
    void Update()
    {
        transform.position = rcMenu.transform.position;
    }
}
