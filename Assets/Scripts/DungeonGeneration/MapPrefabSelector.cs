using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPrefabSelector : MonoBehaviour
{
    public GameObject T, B, L, R,
                      TB, LR, TL, TR, BL, BR,
                      TBL, TLR, TBR, BLR, TBLR;
    public GameObject BLRuined, TBLRCenterHole, TLRRuined, TRRuined, TBRuined, LRRuined;
    public GameObject B_Healing;

    public bool up, down, left, right;
    public bool isMini = false;

    //Rate at which a ruined room will appear
    private float ruinRate = 0.5f;
    private float healRate = 0.33f;

    void Create(GameObject roomType)
    {
        GameObject map = Instantiate(roomType, transform.position, Quaternion.identity);
        if(this.gameObject.name == "MiniMapPrefab")
        {
            map.GetComponent<RoomMiniData>().playerInside = false;
            map.GetComponent<RoomMiniData>().hasEnteredMini = false;
        }
    }

    //Decides on the type of room to be created based on doors
    void PickRoom()
    {
        float swap = Random.Range(0.0f, 1.0f);

        if (up)
        {
            if (down)
            {
                if (right)
                {
                    if (left)
                    {
                        if (swap <= ruinRate && isMini == false) { Create(TBLRCenterHole); } else { Create(TBLR); }
                    }
                    else
                    {
                        Create(TBR);
                    }
                }
                else if (left)
                {
                    Create(TBL);
                }
                else
                {
                    if (swap <= ruinRate && isMini == false) { Create(TBRuined); } else { Create(TB); }
                }
            }
            else
            {
                if (right)
                {
                    if (left)
                    {
                        if (swap <= ruinRate && isMini == false) { Create(TLRRuined); } else { Create(TLR); }

                    }
                    else
                    {
                        if (swap <= ruinRate && isMini == false) { Create(TRRuined); } else { Create(TR); }

                    }
                }
                else if (left)
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
            }
            else if (left)
            {
                if (swap <= ruinRate && isMini == false) { Create(BLRuined); } else { Create(BL); }
            }
            else
            {
                if (swap <= healRate && isMini == false) { Create(B_Healing); } else { Create(B); }
            }
            return;
        }
        if (right)
        {
            if (left)
            {
                if (swap <= ruinRate && isMini == false) { Create(LRRuined); } else { Create(LR); }
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
        healRate = 1f;
        PickRoom();
        Destroy(this.gameObject);
    }
}
