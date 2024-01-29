using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocker : MonoBehaviour
{
    public int shieldHealth;

    void Update()
    {
        transform.localEulerAngles += new Vector3(0,0, 500 * Time.deltaTime);
    }
}
