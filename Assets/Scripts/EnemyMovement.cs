using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyMovement : MonoBehaviour
{
    private int i = 0;
    private float speed = 4.5f;
    private void Update()
    {
        transform.Translate(new Vector3(1,0,0)*speed*Time.deltaTime);

        if (Vector3.Distance(CharacterMovement.Instance.movementMemory[i].Item1, transform.position) < 0.1f)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 
                CharacterMovement.Instance.movementMemory[i].Item2);
            i++;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("MainCar"))
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
            SceneManager.LoadScene("LostScene");
        }
        else if (other.CompareTag("EnemyCar"))
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
