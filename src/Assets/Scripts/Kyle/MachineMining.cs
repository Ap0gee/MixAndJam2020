using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineMining : MonoBehaviour
{
    public GameObject IngotPrefab;
    public float SpawnTimer = 15;
    public Transform SpawnLocation;
    public Vector3 ThrowDirection = new Vector3(-145, 1, 0);

    private float LastIngot;

    // Start is called before the first frame update
    void Start()
    {
        LastIngot = Time.fixedTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Time.fixedTime >= SpawnTimer + LastIngot)
        {
            GameObject newIngot = GameObject.Instantiate(IngotPrefab);
            newIngot.transform.position = SpawnLocation.position;
            
            Rigidbody ingotRb = newIngot.GetComponent<Rigidbody>();

            ingotRb.AddForce(ThrowDirection);

            LastIngot = Time.fixedTime;
        }
    }
}
