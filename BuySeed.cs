using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class BuySeed : MonoBehaviour
{
    private Player player;

    public void clickSeed()
    {
        player = GameManager.player;
        // Get the child image component of the slot
        Image slotImage = gameObject.GetComponentInChildren<Image>();
        // Get the sprite from the image component
        Sprite sprite = slotImage.sprite;
        if (sprite == null) {
            Debug.LogError("No sprite assigned to image component.");
            return;
        }
        // Get the file name of the sprite
        string className = sprite.texture.name;
        // Remove the file extension
        className = Path.GetFileNameWithoutExtension(className);

        // Find the type with the matching name
        System.Type seedType = System.Type.GetType(className);
        if (seedType == null || !typeof(Seed).IsAssignableFrom(seedType))
        {
            Debug.LogError("Invalid seed type: " + className);
            return;
        }

        // Create a new instance of the seed type
        Seed seed = System.Activator.CreateInstance(seedType) as Seed;
        if (seed == null)
        {
            Debug.LogError("Failed to create instance of seed type: " + className);
            return;
        }

        // Check if the player has enough gold to buy the seed
        if (player.gold < seed.Price)
        {
            Debug.Log("Not enough gold to buy " + seed.GetType().Name);
            return;
        }

        // Add the seed to the player's inventory and deduct the cost from their gold
        player.inventory.AddItem(seed);
        player.gold -= seed.Price;
    }
}
