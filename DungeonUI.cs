using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class DungeonUI : MonoBehaviour
{
    public List<GameObject> enemyCircles;
    public List<GameObject> bossCircleObjects;
    public List<BossCircle> bossCircles;
    public Material grayOverlay;

    private int numberOfEnemies;

    void Start()
    {
        // Find all instances of the EnemyCircle prefab
        GameObject[] enemyCircleArray = GameObject.FindGameObjectsWithTag("EnemyCircle");
        enemyCircles = new List<GameObject>(enemyCircleArray);

        // Find all instances of the BossCircle prefab
        GameObject[] bossCircleArray = GameObject.FindGameObjectsWithTag("BossCircle");
        bossCircleObjects = new List<GameObject>(bossCircleArray);
        foreach(GameObject bossCircleObject in bossCircleObjects) {
            bossCircles.Add(bossCircleObject.GetComponent<BossCircle>());
        }

        foreach(BossCircle bossCircle in bossCircles) {
            bossCircle.SetPet(new Pet("", new Crab()));
        }

        numberOfEnemies = bossCircles.Count + enemyCircles.Count;
    }

    void Update() {  
        for(int i=0;i < GameManager.childDungeon.getCurrentIndex(); i++) {
            if(i == numberOfEnemies/2 - 1 || i == numberOfEnemies) {
                GameObject petImage = bossCircleObjects[(i/4)-1].transform.Find("petImage").gameObject;
                Image image = petImage.GetComponent<Image>();
                image.material = grayOverlay;
            } else if(i < numberOfEnemies/2 - 1) {
                Image circleImage = enemyCircles[i].GetComponent<Image>();
                circleImage.color = Color.red;
            } else if(i > numberOfEnemies/2 - 1) {
                Image circleImage = enemyCircles[i-1].GetComponent<Image>();
                circleImage.color = Color.red;
            }
            
        }
    }
}

