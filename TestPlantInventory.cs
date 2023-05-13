using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlantInventory : MonoBehaviour
{
    public InventorySlot inventorySlot;

    public void Test()
    {
        inventorySlot.SetItem(new FireLily(new FireLilySeed()));
    }
}
