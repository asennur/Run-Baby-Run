using UnityEngine;

public class RightTrigger : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.transform.CompareTag("Wall"))
        {
            if(transform.parent.eulerAngles.z == 0)
                transform.parent.GetComponent<CharacterMovement>()._rightTurnAngle = 270;
            else 
                transform.parent.GetComponent<CharacterMovement>()._rightTurnAngle = 
                    Mathf.Abs((int)(transform.parent.eulerAngles.z - 90));
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        transform.parent.GetComponent<CharacterMovement>()._rightTurnAngle = 11111;
    }
}
