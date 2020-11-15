using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ReceipeItem
{
    public ItemType Item;
    public int Quantity;
}

[System.Serializable]
public struct Receipe
{
    public GameObject Produces;
    public List<ReceipeItem> receipeItems;
}

public class BaseMachine : MonoBehaviour
{
    public Receipe ActiveRecipe;
    public Dictionary<ItemType, int> Counter = new Dictionary<ItemType, int>();
    public Transform SpawnLocation;
    public float ThrowForce = 2f;
    public bool IsEnabled = false;

    private void Start()
    {
        ResetCounter();
    }
    

    public virtual void SetReceipe(Receipe receipe)
    {
        this.ActiveRecipe = receipe;
        this.ResetCounter();
    }

    public virtual void ResetCounter()
    {
        Debug.Log("Resetting Counter");
        foreach(ReceipeItem item in this.ActiveRecipe.receipeItems)
        {
            if (Counter.ContainsKey(item.Item))
            {
                Counter[item.Item] += item.Quantity;
            } else
            {
                Counter[item.Item] = item.Quantity;
            }
        }
    }

    // Update is called once per frame
    public void FixedUpdate()
    {
        bool ShouldCreate = true;

        foreach (ReceipeItem receipeItem in ActiveRecipe.receipeItems)
        {
            if (!ShouldCreate)
            {
                continue;
            }

            if (Counter[receipeItem.Item] < receipeItem.Quantity)
            {
                ShouldCreate = false;
            }
        }

        // Todo: Need some processing time
        if (ShouldCreate)
        {
            foreach (ReceipeItem receipeItem in ActiveRecipe.receipeItems)
            {
                int amountRemaining = Counter[receipeItem.Item] - receipeItem.Quantity;
                Counter[receipeItem.Item] = (amountRemaining > 0) ? amountRemaining : 0;
            }

            SpawnItem();
        }
    }

    protected virtual void SpawnItem()
    {
        GameObject spawnedItem = Instantiate(this.ActiveRecipe.Produces);
        spawnedItem.transform.position = this.SpawnLocation.position;
    }

    protected virtual void DestroyItem(GameObject go)
    {
        Destroy(go);
    }

    protected void OnCollisionEnter(Collision collision)
    {
        if (!this.IsEnabled)
        {
            return;
        } 

        GameObject go = collision.gameObject;
        // This is an item
        if (go.layer == 15)
        {
            ItemScript item = go.GetComponent<ItemScript>();

            foreach(ReceipeItem receipe in ActiveRecipe.receipeItems)
            {
                if (receipe.Item == item.ItemType)
                {
                    this.Counter[item.ItemType] += 1;
                    this.DestroyItem(go);
                }
            }
        }
    }
}
