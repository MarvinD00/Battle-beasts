using System;
using UnityEngine;
using System.Collections;

public class Dungeon {
    

    protected Pet[] enemies;
    protected Battle battle;
    protected int goldReward;
    protected Pet petReward;
    protected Seed seedReward;
    protected int minGoldReward = 100;
    protected int maxGoldReward = 200;
    protected float seedProbability = 0.1f;
    protected int currentIndex = 0;

    public Dungeon( Pet[] enemies, int numEnemies, int goldReward, Pet petReward, Seed seedReward) {
        enemies = new Pet[numEnemies];
        this.goldReward = goldReward;
        this.petReward = petReward;
        this.seedReward = seedReward;
        this.enemies = new Pet[numEnemies];
        for(int i=0; i<numEnemies; i++) {
            this.enemies[i] = enemies[i];
        }
    }

    public int GetGoldReward() {
        return goldReward;
    }

    public Pet GetPetReward() {
        return petReward;
    }

    public Seed GetSeedReward() {
        return seedReward;
    }

    public Pet[] GetEnemies() {
        return enemies;
    }

    public void StartBattle() {
        // Get the current enemy
        Pet currentEnemy = enemies[currentIndex];
        // Create a new instance of the Battle class
        battle = new Battle();
        // Subscribe to the OnBattleWon event
        battle.OnBattleWon += OnBattleWonHandler;
        GameManager.battle = battle;
    }

    public Pet getCurrentEnemy() {
        return enemies[currentIndex];
    }

    public int getCurrentIndex() {
        return currentIndex;
    }

    public Pet[] Enemies {
        get {return enemies;}
    }

    protected void RewardPlayer(int goldReward, Pet petReward, Seed seedReward, int xpReward) {
        // Add the gold reward to the player's inventory
        GameManager.player.gold += goldReward;
        GameManager.selectedPet.GainExperience(xpReward);
        // Add the pet and seed rewards to their respective inventories
        if (petReward != null) {
            GameManager.player.pets.Add(petReward);
        }
        if (seedReward != null) {
            GameManager.player.inventory.AddItem(seedReward);
        }
        GameManager.currentGoldReward = goldReward;
        GameManager.currentXPReward = xpReward;
        GameManager.currentPetReward = petReward;
        GameManager.currentSeedReward = seedReward;
    }
    
    protected int GetRandomGoldReward() {
        // Generate a random gold reward within the specified range
        return UnityEngine.Random.Range(minGoldReward, maxGoldReward);
    }

    protected Pet GetRandomPetReward() {
        // Generate a random pet reward from the list of enemies
        // ...
        return null;
    }

    protected Seed GetRandomSeedReward() {
        // Generate a random seed reward with a low probability
        if (UnityEngine.Random.value < seedProbability) {
            return null;
        }
        return null;
    }

    protected (int, Pet, Seed) GetRandomReward() {
        // Generate random rewards
        int goldReward = GetRandomGoldReward();
        Pet petReward = GetRandomPetReward();
        Seed seedReward = GetRandomSeedReward();

        return (goldReward, petReward, seedReward);
    }

    protected void OnBattleWonHandler(Pet winningPet)
    {
        if(winningPet == GameManager.selectedPet) {
            (var gold, var pet, var seed) = GetRandomReward();
            Pet currentEnemy = enemies[currentIndex];
            int xpReward = currentEnemy.Species.ExperienceToLevelUp(currentEnemy.Level)/2;
            RewardPlayer(gold, pet, seed, xpReward);
            this.currentIndex += 1;
            battle.OnBattleWon -= OnBattleWonHandler;
        }
    }
}

public class ChildDungeon : Dungeon {
    
    public ChildDungeon() : base(null, 10, 0, null, null) {
        // Set values "by hand"
        this.enemies = new Pet[] {
            new Pet("Enemy 1", new FireDragon(), 10),
            new Pet("Enemy 2", new Crab(), 20),
            new Pet("Enemy 3", new Golem(), 30),
            new Pet("Enemy 4", new FireLizard(), 35),
            new Pet("Boss 1", new Harpy(), 50),
            new Pet("Enemy 6", new Gryphon(), 55),
            new Pet("Enemy 7", new Naga(), 60),
            new Pet("Enemy 8", new FireDragon(), 65),
            new Pet("Enemy 9", new Crab(), 70),
            new Pet("Boss 2", new Naga(), 80)
        };
        this.goldReward = 100;
        this.petReward = null;
        this.seedReward = null;
    }
}
