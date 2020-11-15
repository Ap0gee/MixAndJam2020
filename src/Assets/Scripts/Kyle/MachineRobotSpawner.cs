using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineRobotSpawner : BaseMachine
{
    protected GameObject robotToSpawn = null;
    public Transform InitialRobotLocation = null;

    protected override void DestroyItem(GameObject go)
    {
        // Write down what the robot type was before we destroy this object
        ItemCrateScript script = go.GetComponent<ItemCrateScript>();
        this.robotToSpawn = script.RobotToSpawn;
        base.DestroyItem(go);
    }

    protected override void SpawnItem()
    {
        GameObject spawnedItem = Instantiate(this.robotToSpawn);
        RobotScript robot = spawnedItem.GetComponent<RobotScript>();
        robot.Load(this.SpawnLocation.position);
        robot.MoveTarget = InitialRobotLocation;
    }
}
