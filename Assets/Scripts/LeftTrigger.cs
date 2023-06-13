using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftTrigger : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.transform.CompareTag("Wall"))
        {
            if(transform.parent.eulerAngles.z == 270)
                transform.parent.GetComponent<CharacterMovement>()._leftTurnAngle = 0;
            else 
                transform.parent.GetComponent<CharacterMovement>()._leftTurnAngle = 
                    Mathf.Abs((int)(transform.parent.eulerAngles.z + 90));
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        transform.parent.GetComponent<CharacterMovement>()._leftTurnAngle = 11111;
    }
}
