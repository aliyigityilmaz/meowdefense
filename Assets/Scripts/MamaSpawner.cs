using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MamaSpawner : MonoBehaviour
{
    public GameObject mama;
    public int xPos;
    public int zPos;
    public int mamaCount;

    private void Start()
    {
        mamaCount = 0;
    }

    private void Update()
    {
        if (mamaCount < 10)
        {
            xPos = Random.Range(-60, 30);
            zPos = Random.Range(-50, 10);
            GameObject newMama = Instantiate(mama, new Vector3(xPos, (float)0.5, zPos), Quaternion.identity);
            StartCoroutine(SpawnMama(newMama));
            mamaCount += 1;
        }
    }

    public void DecreaseMamaCount()
    {
        mamaCount -= 1;
    }

    IEnumerator SpawnMama(GameObject gameObject)
    {
        yield return new WaitForSeconds(30);
        Destroy(gameObject);
        mamaCount--;
    }



}
