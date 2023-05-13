using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class PetListManager : MonoBehaviour
{
    public GameObject petSlotPrefab;
    public Transform petSlotPanel;

    private List<GameObject> petSlots = new List<GameObject>();

    private void Start()
    {
        // Loop through all the child objects of the inventory panel
        foreach (Transform child in petSlotPanel.transform)
        {
            // Check if the child is an inventory slot
            if (child.gameObject.GetComponent<PetSlot>())
            {
                petSlots.Add(child.gameObject);
            }
        }
    }

    void Update() {
        updatePetList();
    }

    private void updatePetList() {
        int index = 0;
        foreach (GameObject slotObj in petSlots)
            {
                PetSlot slot = slotObj.GetComponent<PetSlot>();
                if (GameManager.player.pets.Count > index)
                {
                    slotObj.SetActive(true);
                    slot.SetPet(GameManager.player.pets[index]);
                }
                else {
                    slotObj.SetActive(false);
                }
                index++;
            }
    }
}
