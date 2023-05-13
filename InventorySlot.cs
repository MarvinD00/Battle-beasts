using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Text.RegularExpressions;
using System.Linq;

public class InventorySlot : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler
{

    public Image borderImage;
    public Image itemImage;
    public Text countText;
    public Seed seed;
    public Plant plant;
    public GameObject infoPanel;
    public Text nameText;
    public Text growthTimeText;
    public Text effectText;
    public bool plantActive = false;
    public bool seedActive = false;

    
    public void SetItem() {
        itemImage.sprite = null;
        borderImage.color = Color.white;
        countText.text = "";
        this.seed = null;
        this.plant = null;
    }

    public void SetItem(Plant plant)
    {
        itemImage.sprite = GetSpriteFromClassName(plant.GetType().Name);
        borderImage.color = GetColorFromQuality(plant.quality);
        plantActive = true;
        seedActive = false;
        this.plant = plant;
        this.seed = null;
    }

    public void SetCount(int count) {
        countText.text = count.ToString();
    }

    public void SetItem(Seed seed)
    {
        itemImage.sprite = GetSpriteFromClassName(seed.GetType().Name);
        borderImage.color = GetColorFromQuality(seed.quality);
        seedActive = true;
        plantActive = false;
        this.seed = seed;
        this.plant = null;
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

    private Color GetColorFromQuality(Quality quality)
    {
        // Set the border color based on the quality field
        switch (quality)
        {
            case Quality.bad:
                return Color.gray;
            case Quality.average:
                return Color.white;
            case Quality.good:
                return Color.green;
            case Quality.great:
                return Color.blue;
            case Quality.legendary:
                return Color.yellow;
            case Quality.mythical:
                return new Color(0.5f, 0, 0.5f);
            default:
                return Color.black;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (seed != null)
        {
            infoPanel.SetActive(true);

            // Split the class name into separate words by matching capital letters
            string[] words = Regex.Matches(seed.GetType().Name, @"[A-Z][a-z]*")
                .Cast<Match>()
                .Select(m => m.Value)
                .ToArray();

            // Join the words with spaces to create the display name
            string displayName = string.Join(" ", words);
            nameText.text = displayName;
            growthTimeText.text = FormatTime(seed.GrowthTime);
            effectText.text = seed.Effect;
        }
        if (plant != null) {
            infoPanel.SetActive(true);

            // Split the class name into separate words by matching capital letters
            string[] words = Regex.Matches(plant.GetType().Name, @"[A-Z][a-z]*")
                .Cast<Match>()
                .Select(m => m.Value)
                .ToArray();

            // Join the words with spaces to create the display name
            string displayName = string.Join(" ", words);
            nameText.text = displayName;
            growthTimeText.text = "";
            effectText.text = plant.effect;
        }
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        infoPanel.SetActive(false);
    }

    public string FormatTime(float seconds)
    {
        int hours = (int)(seconds / 3600);
        int minutes = (int)((seconds % 3600) / 60);
        int remainingSeconds = (int)(seconds % 60);

        string timeString = "";

        if (hours > 0)
        {
            timeString += hours + " hour" + (hours > 1 ? "s" : "") + ", ";
        }

        if (hours > 0 || minutes > 0)
        {
            timeString += minutes + " minute" + (minutes > 1 ? "s" : "") + ", ";
        }

        timeString += remainingSeconds + " second" + (remainingSeconds > 1 ? "s" : "");

        return timeString;
    }
}
