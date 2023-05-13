using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System.Linq;

public class PetPanelManager : MonoBehaviour
{
    public Text nameText;
    public Text hpText;
    public Text attText;
    public Text defText;
    public Text levelText;
    public Text speciesText;
    public Text speedText;
    public Image petImage;

    public Slider xpSlider;
    public Text xpText;

    public Pet pet;

    void Start() {
        SetPet(GameManager.player.pets[0]);
    }

    void Update() {
        nameText.text = pet.Name;
        hpText.text = "HP: " + pet.Health;
        attText.text = "ATT: " + pet.Attack;
        defText.text = "DEF: " + pet.Defense;
        levelText.text = "Level: " + pet.Level;
        speedText.text = "Speed: " + pet.Speed;
        // Split the class name into separate words by matching capital letters
        string[] words = Regex.Matches(pet.Species.GetType().Name, @"[A-Z][a-z]*")
            .Cast<Match>()
            .Select(m => m.Value)
            .ToArray();

        // Join the words with spaces to create the display name
        string displayName = string.Join(" ", words);

        speciesText.text = displayName;

        petImage.sprite = GetSpriteFromClassName(pet.Species.GetType().Name);

        xpSlider.maxValue = pet.Species.ExperienceToLevelUp(pet.Level);
        xpSlider.value = pet.Experience;
        xpText.text = pet.Experience + " / " + pet.Species.ExperienceToLevelUp(pet.Level);

        GameManager.selectedPet = pet;
    }

    public void SetPet(Pet pet) {
        this.pet = pet;
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
