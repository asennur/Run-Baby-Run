using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public void LoadMainScene()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
