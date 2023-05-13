using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Adventure : MonoBehaviour
{
    public Pet pet;
    public Battle battle;
    public Slider timeSlider;
    public Text timeText;

    void Start() {
        this.pet = GameManager.selectedPet;
        GameManager.battle.OnBattleWon += OnBattleWonHandler;
    }

    void Update() {
        pet = GameManager.selectedPet;
        if(pet.isOnAdventure && !timeSlider.gameObject.activeSelf) {
            timeSlider.gameObject.SetActive(true);
            timeText.gameObject.SetActive(true);
        } else if (!pet.isOnAdventure) {
            timeSlider.gameObject.SetActive(false);
            timeText.gameObject.SetActive(false);
        }
        timeSlider.value = (pet.adventureTimer);
        timeText.text = ((int) pet.adventureTimer).ToString();
    }

    public void click() {
        if(pet.isOnAdventure && pet.adventureTimer == 0) {
            GameManager.playerPet = pet;
            GameManager.enemyPet = new Pet("Crabby", new Crab(), pet.Level); //random pet
            SceneManager.LoadScene("BattleScene");
        } else if(!pet.isOnAdventure) {
            pet.isOnAdventure = true;
            pet.adventureTimer = 5F;
            timeSlider.maxValue = pet.adventureTimer;
            timeSlider.gameObject.SetActive(true);
            timeText.gameObject.SetActive(true);
        }
    }

    private void OnBattleWonHandler(Pet winningPet)
    {
        if(winningPet == pet) {
            int xpReward = (int) (pet.Species.ExperienceToLevelUp(pet.Level)/(1.2*pet.Level));
            pet.GainExperience(xpReward);
            int minValue = (int)(pet.Level * 20 / 1.1);
            int maxValue = (int)(pet.Level * 20 * 1.1);
            int goldReward = UnityEngine.Random.Range(minValue, maxValue);
            GameManager.player.gold += goldReward;
            GameManager.currentGoldReward = goldReward;
            GameManager.currentXPReward = xpReward;
            GameManager.currentPetReward = null;
            GameManager.currentSeedReward = null;
            pet.isOnAdventure = false;
            GameManager.battle.OnBattleWon -= OnBattleWonHandler;
        }
    }


}
