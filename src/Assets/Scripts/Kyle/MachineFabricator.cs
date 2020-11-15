using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineFabricator : BaseMachine
{
    public GameObject RobotType;

    protected override void SpawnItem()
    {
        // This will try and spawn a crate type object
        GameObject spawnedItem = Instantiate(this.ActiveRecipe.Produces);

        spawnedItem.GetComponent<ItemCrateScript>().RobotToSpawn = this.RobotType;
        spawnedItem.transform.position = this.SpawnLocation.position;
    }
}
