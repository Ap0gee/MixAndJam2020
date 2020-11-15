using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineRobotSpawner : BaseMachine
{
    private void SpawnItem()
    {
        GameObject spawnedItem = Instantiate(this.ActiveRecipe.Produces);
        spawnedItem.transform.position = this.SpawnLocation.position;
    }
}
