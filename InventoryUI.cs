using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class InventoryUI : MonoBehaviour
{
    public GameObject inventorySlotPrefab;
    public Transform inventoryPanel;
    public bool plantMode = false;
    public bool seedMode = false;

    private Inventory inventory;
    private List<GameObject> inventorySlots = new List<GameObject>();

    private void Start()
    {
        inventory = GameManager.player.inventory;
        GetInventorySlots();
    }

    void Update() {
        if(seedMode){
            SetSeedSprites();
        } else if(plantMode) {
            SetPlantSprites();
        }
    }

    public void setPlantMode() {
        plantMode = true;
        seedMode = false;
    }

    public void setSeedMode(){
        plantMode = false;
        seedMode = true;
    }

    private void GetInventorySlots()
    {
        // Loop through all the child objects of the inventory panel
        foreach (Transform child in inventoryPanel.transform)
        {
            // Check if the child is an inventory slot
            if (child.gameObject.GetComponent<InventorySlot>())
            {
                inventorySlots.Add(child.gameObject);
            }
        }
    }

    public void SetPlantSprites()
    {
        int index = 0;
        // Set the sprite of all slot objects in the array
        foreach (GameObject slotObj in inventorySlots)
        {
            InventorySlot slot = slotObj.GetComponent<InventorySlot>();
            List<Type> plantTypes = new List<Type>(inventory.plantCounts.Keys);
            if (plantTypes.Count > index)
            {
                Type plantType = plantTypes[index];
                Plant plant = (Plant)Activator.CreateInstance(plantType);
                int count = inventory.plantCounts[plantType];
                slot.SetItem(plant);
                slot.SetCount(count);
            }
            else {
                slot.SetItem();
            }
            index++;
        }
    }
    
    public void SetSeedSprites()
    {        
        int index = 0;
        // Set the sprite of all slot objects in the array
        foreach (GameObject slotObj in inventorySlots)
        {
            InventorySlot slot = slotObj.GetComponent<InventorySlot>();
            List<Type> seedTypes = new List<Type>(inventory.seedCounts.Keys);
            if (seedTypes.Count > index)
            {
                Type seedType = seedTypes[index];
                Seed seed = (Seed)Activator.CreateInstance(seedType);
                int count = inventory.seedCounts[seedType];
                slot.SetItem(seed);
                slot.SetCount(count);
            }
            else {
                slot.SetItem();
            }
            index++;
        }
    }
}
