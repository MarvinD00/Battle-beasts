using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public PetPanelBattle playerPetPanel;
    public PetPanelBattle enemyPetPanel;
    public GameObject rewardPanel;
    
    private Battle battle;

    void Start()
    {
        StartBattle();
    }

    public void StartBattle()
    {
        // Set the pets to the corresponding PetPanelBattles
        playerPetPanel.SetPet(GameManager.playerPet);
        enemyPetPanel.SetPet(GameManager.enemyPet);
        battle = GameManager.battle;
        battle.OnBattleWon += OnBattleWonHandler;
        StartCoroutine(battle.StartBattle(GameManager.playerPet, GameManager.enemyPet));
    }

    private void OnBattleWonHandler(Pet winningPet) {
        Transform rewardPanelTransform = transform.Find("rewardPanel");
        rewardPanelTransform.gameObject.SetActive(true);
        Transform goldRewardText = rewardPanel.transform.Find("goldRewardText");
        Transform xpRewardText = rewardPanel.transform.Find("xpRewardText");
        Transform petImage = rewardPanel.transform.Find("petImage");
        Transform seedImage = rewardPanel.transform.Find("seedImage");
        if(winningPet == GameManager.selectedPet) {
            goldRewardText.GetComponent<Text>().text = "Gold gained: " + GameManager.currentGoldReward;
            xpRewardText.GetComponent<Text>().text = "Xp gained: " + GameManager.currentXPReward;
            if(GameManager.currentPetReward != null) {
                string className = GameManager.currentPetReward.GetType().Name;
                string resourceName = "Images/" + className;
                Sprite sprite = Resources.Load<Sprite>(resourceName);
                petImage.GetComponent<Image>().sprite = sprite;
            } else {
                petImage.gameObject.SetActive(false);
            }
            if(GameManager.currentSeedReward != null) {
                string className = GameManager.currentSeedReward.GetType().Name;
                string resourceName = "Images/" + className;
                Sprite sprite = Resources.Load<Sprite>(resourceName);
                seedImage.GetComponent<Image>().sprite = sprite;  
            } else {
                seedImage.gameObject.SetActive(false);
            }
        } else {
            goldRewardText.gameObject.SetActive(false);
            xpRewardText.gameObject.SetActive(false);
            petImage.gameObject.SetActive(false);
            seedImage.gameObject.SetActive(false);
        }
        battle.OnBattleWon -= OnBattleWonHandler;
    }
}
