using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestBattle : MonoBehaviour
{
    ChildDungeon childDungeon;
    void Start() {
        childDungeon = GameManager.childDungeon;  
    }

    public void test() {
        // Set the pets to the corresponding PetPanelBattles    
        GameManager.playerPet = GameManager.selectedPet;
        GameManager.enemyPet = childDungeon.GetEnemies()[childDungeon.getCurrentIndex()];
        GameManager.childDungeon.StartBattle();
        SceneManager.LoadScene("BattleScene");
    }
}
