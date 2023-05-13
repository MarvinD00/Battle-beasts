using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System.Linq;

public class ShopUI : MonoBehaviour
{
    public GameObject ShopSlotPrefab;
    public Transform shopPanel;
    public Vector2 slotOffset = new Vector2(0, -20); // offset between slots

    private List<Seed> availableSeeds;

    private void Start()
    {
        // Set up available seeds for sale
        availableSeeds = new List<Seed>();
        availableSeeds.Add(new FireLilySeed()); 
        availableSeeds.Add(new WaterRoseSeed());
        availableSeeds.Add(new StarLilySeed());
        availableSeeds.Add(new ShadowRoseSeed());
        

        // Fill out the shop panel with inventory slots
        Vector2 slotPosition = new Vector2(-430, 255); // starting position
        foreach (Seed seed in availableSeeds)
        {
            // Create a new slot object and set its position
            GameObject slotObj = Instantiate(ShopSlotPrefab, shopPanel);
            RectTransform slotRectTransform = slotObj.GetComponent<RectTransform>();
            slotRectTransform.anchoredPosition = slotPosition;

            // Increment the y position for the next slot
            slotPosition += slotOffset;

            // Set the item image for the slot
            ShopSlot slot = slotObj.GetComponent<ShopSlot>();
            slot.SetItem(seed);
        }
    }
}
