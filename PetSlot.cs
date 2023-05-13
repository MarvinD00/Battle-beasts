using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PetSlot : MonoBehaviour
{
    public Image itemImage;
    public Text nameText;
    public Pet pet;
    private PetPanelManager petPanelManager;

    void Start() {
        petPanelManager = FindObjectOfType<PetPanelManager>();
    }

    public void setPetPanelPet() {
        petPanelManager.SetPet(this.pet);
        GameManager.selectedPet = this.pet;
    }

    public void SetPet(Pet pet)
    {
        this.pet = pet;
        itemImage.sprite = GetSpriteFromClassName(pet.Species.GetType().Name);
        nameText.text = pet.Name;
    }

    private Sprite GetSpriteFromClassName(string className)
    {
        string resourceName = "Images/" + className;
        Sprite sprite = Resources.Load<Sprite>(resourceName);

        if (sprite == null)
        {
            Debug.LogError("Sprite not found for class name: " + className + " " + resourceName);
        }
        return sprite;
    }
}
