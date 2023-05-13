using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ClickItem : MonoBehaviour
{
    private Player player;
    private Plot plot;

    void Start() {
        plot = FindObjectOfType<Plot>();
        GameManager.plantMode = false;
    }

    public void click()
    {
        if (SceneManager.GetActiveScene().name == "FarmScene") {
            player = GameManager.player;

            // Get the child image component of the slot
            Image slotImage = gameObject.GetComponentInChildren<Image>();

            // Get the sprite from the image component
            Sprite sprite = slotImage.sprite;

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

            GameManager.plantMode = true;
            GameManager.selectedSeed = seed;
        } else if (SceneManager.GetActiveScene().name == "PetScene") {
            // Get the child image component of the slot
            Image slotImage = gameObject.GetComponentInChildren<Image>();

            // Get the sprite from the image component
            Sprite sprite = slotImage.sprite;

            // Get the file name of the sprite
            string className = sprite.texture.name;

            // Remove the file extension
            className = Path.GetFileNameWithoutExtension(className);

            // Find the type with the matching name
            System.Type plantType = System.Type.GetType(className);
            if (plantType == null || !typeof(Plant).IsAssignableFrom(plantType))
            {
                Debug.LogError("Invalid plant type: " + className);
                return;
            }

            // Create a new instance of the seed type
            Plant plant = System.Activator.CreateInstance(plantType) as Plant;
            if (plant == null)
            {
                Debug.LogError("Failed to create instance of plant type: " + className);
                return;
            }

            if(plant.UseOnPet(GameManager.selectedPet)) {
                GameManager.player.inventory.RemoveItem(plant);
            }
            
        }
    }
}
