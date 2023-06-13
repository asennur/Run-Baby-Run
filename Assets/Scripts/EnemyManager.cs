using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<GameObject> enemyCars = new List<GameObject>();
    private int i = 0;
    
    private void Start()
    {
        StartCoroutine(EnemyActivate());
    }

    private IEnumerator EnemyActivate()
    {
        yield return new WaitForSeconds(5f);
        while (i < enemyCars.Count)
        {
            yield return new WaitForSeconds(5f);
            enemyCars[i].SetActive(true);
            i++;
        }
    }
}
