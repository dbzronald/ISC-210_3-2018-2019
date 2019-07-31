using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    // Start is called before the first frame update

    private void OnTriggerStay(Collider other)
    {
        if (other.tag != "EssenceItem" || !Input.GetButtonDown("Submit"))
        {
            return;
        }
        other.SendMessage("OpenChest");
    }
}
