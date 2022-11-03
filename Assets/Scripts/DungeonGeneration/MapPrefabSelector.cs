using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPrefabSelector : MonoBehaviour
{
    public GameObject T, B, L, R,
                      TB, LR, TL, TR, BL, BR,
                      TBL, TLR, TBR, BLR, TBLR;
    public bool up, down, left, right;

    void Create(GameObject roomType)
    {
        Instantiate(roomType, transform.position, Quaternion.identity);
    }
    void PickRoom()
    {
        if (up)
        {
            if (down)
            {
                if (right)
                {
                    if (left)
                    {
                        Create(TBLR);
                    }
                    else
                    {
                        Create(TBR);
                    }
                } else if (left)
                {
                    Create(TBL);
                }
                else
                {
                    Create(TB);
                }
            } else
            {
                if (right)
                {
                    if (left)
                    {
                        Create(TLR);
                    }
                    else
                    {
                        Create(TR);
                    }
                } else if (left)
                {
                    Create(TL);
                }
                else
                {
                    Create(T);
                }
            }
            return;
        }
        if (down)
        {
            if (right)
            {
                if (left)
                {
                    Create(BLR);
                }
                else
                {
                    Create(BR);
                }
            } else if (left)
            {
                Create(BL);
            }
            else
            {
                Create(B);
            }
            return;
        }
        if (right)
        {
            if (left)
            {
                Create(LR);
            }
            else
            {
                Create(R);
            }
        }
        else
        {
            Create(L);
        }
    }
    void Start()
    {
        PickRoom();
        Destroy(this.gameObject);
    }
}
