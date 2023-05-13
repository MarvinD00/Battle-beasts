using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static Battle battle;
    public static Player player;
    public static bool plantMode;
    public static Seed selectedSeed;
    public static Pet selectedPet = null;
    public static Pet playerPet;
    public static Pet enemyPet;
    public static ChildDungeon childDungeon;
    public static List<Plot> plots;
    public static bool plotsInitialized = false;
    public static bool isTimerRunning = false;
    public static int currentGoldReward;
    public static int currentXPReward; 
    public static Pet currentPetReward;
    public static Seed currentSeedReward;
    
    private static bool isInitialized = false;

    private void Awake()
    {
        if(!isInitialized) {
            DontDestroyOnLoad(gameObject);
            player = new Player();
            battle = new Battle();
            player.pets.Add(new Pet("Test Dragon", new FireDragon()));
            selectedPet = player.pets[0];
            for(int i=0; i<1000; i++) {
                player.inventory.AddItem(new WaterRose());  
                player.inventory.AddItem(new FireLily());
                player.inventory.AddItem(new ShadowRose());
            }  
            /*player.pets.Add(new Pet("Test Lizard", new FireLizard()));
            player.pets.Add(new Pet("Test Naga", new Naga()));
            player.pets.Add(new Pet("Test Golem", new Golem()));
            player.pets.Add(new Pet("Test Crab", new Crab()));
            player.pets.Add(new Pet("Test Harpy", new Harpy()));
            player.pets.Add(new Pet("Test Gryphon", new Gryphon()));*/
            isInitialized = true;
            childDungeon = new ChildDungeon();
            player.gold = 100000;
        } 
    }

    void Update() {
        //plot timers
        if(plotsInitialized && SceneManager.GetActiveScene().name != "FarmScene") {
            foreach(Plot plot in plots) {
                if(plot.isPlanted) {
                    plot.timer -= Time.deltaTime;

                    if(plot.timer < 0 && plot.plantStage<plot.plantStages.Length -1) {
                        plot.timer = plot.timeBetweenStages;
                        plot.plantStage++;
                        plot.updatePlant();
                    }
                }
            }  
        }
        //adventure timers
        foreach(Pet pet in player.pets) {
            if(pet.isOnAdventure) {
                if(pet.adventureTimer>0) {
                    pet.adventureTimer -= Time.deltaTime;
                } else {
                    pet.adventureTimer = 0;
                }
                
            }
        }
    }
}
