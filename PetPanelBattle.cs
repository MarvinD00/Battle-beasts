using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System.Linq;

public class PetPanelBattle : MonoBehaviour
{
    public Text nameText;
    public Text attText;
    public Text defText;
    public Text levelText;
    public Text damageText;
    public Text speedText;

    public Image petImage;

    public Slider hpSlider;
    public Text hpText;

    public Pet pet;

    void Start() {
    }

    void Update() {
        nameText.text = pet.Name;
        attText.text = "ATT: " + pet.Attack;
        defText.text = "DEF: " + pet.Defense;
        speedText.text = "Speed: " + pet.Speed;

        // Split the class name into separate words by matching capital letters
        string[] words = Regex.Matches(pet.Species.GetType().Name, @"[A-Z][a-z]*")
            .Cast<Match>()
            .Select(m => m.Value)
            .ToArray();

        // Join the words with spaces to create the display name
        string displayName = string.Join(" ", words);

        levelText.text = "Level: " + pet.Level + " " + displayName;
        petImage.sprite = GetSpriteFromClassName(pet.Species.GetType().Name);

        hpSlider.maxValue = pet.Health;
        hpSlider.value = pet.CurrentHealth;
        hpText.text = pet.CurrentHealth + "/" + pet.Health;

        GameManager.selectedPet = pet;
    }

    public void SetPet(Pet pet) {
        this.pet = pet;
        pet.OnDamageTaken += OnDamageTaken;
    }

    private void OnDamageTaken(int damage) {
        if (damageText != null) {
            damageText.text = "-" + damage;
            StartCoroutine(HideDamageText());
        }
    }


    private IEnumerator HideDamageText() {
        yield return new WaitForSeconds(0.75f);
        damageText.text = "";
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

    private void OnDestroy() {
        StopCoroutine(HideDamageText());
    }
}
