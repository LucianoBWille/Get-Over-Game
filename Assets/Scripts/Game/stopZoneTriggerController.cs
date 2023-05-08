using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stopZoneTriggerController : MonoBehaviour
{

    // on trigger exit send a message to the object (the player) to unland (use )
    void OnTriggerExit2D(Collider2D other)
    {
        // send massage to other object (the player) to unland
        other.gameObject.SendMessage("unland");
    }
}
