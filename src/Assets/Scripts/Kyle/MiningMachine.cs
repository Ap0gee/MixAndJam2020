using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningMachine : MonoBehaviour
{
    public GameObject IngotPrefab;
    public float SpawnTimer = 15;
    public Transform SpawnLocation;
    public Vector3 ThrowDirection = new Vector3(-145, 1, 0);

    private float Counter = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Counter);


        if (Counter >= SpawnTimer)
        {
            GameObject newIngot = GameObject.Instantiate(IngotPrefab);
            newIngot.transform.position = SpawnLocation.position;
            
            Rigidbody ingotRb = newIngot.GetComponent<Rigidbody>();

            ingotRb.AddForce(ThrowDirection);

            Counter = 0;
        }

        Counter += Time.deltaTime;
        
    }
}
