using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Text.RegularExpressions;
using System.Linq;

public class BossCircle : MonoBehaviour
{
    public Image petImage;
    public Pet pet;

    public void SetPet(Pet pet)
    {
        petImage.sprite = GetSpriteFromClassName(pet.Species.GetType().Name);
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
