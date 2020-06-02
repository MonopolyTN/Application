using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceSide : MonoBehaviour
{
    private bool onGround;
    public int sideValue;

    private void OnTriggerStay(Collider col)
    {
        if (col.tag == "center")
        {
            onGround = true;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.tag == "center")
        {
            onGround = false;
        }
    }

    public bool OnGround()
    {
        return onGround;
    }
}
