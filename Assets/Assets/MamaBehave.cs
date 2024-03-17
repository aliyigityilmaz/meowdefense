using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MamaBehave : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<CharacterEnergy>().IncreaseEnergy(30);
            GameObject.Find("GameManager").GetComponent<MamaSpawner>().DecreaseMamaCount();
            Destroy(gameObject);
        }
    }
}
