using UnityEngine;
using System.Collections;
using System;

public class Battle {

    public event Action<Pet> OnBattleWon;
    bool playerGoesFirst;

    public IEnumerator StartBattle(Pet playerPet, Pet npcPet) {
        // Determine which pet goes first
        if(playerPet.Speed >= npcPet.Speed) {
            playerGoesFirst = true; 
        }
        
        // Battle loop
        while (playerPet.CurrentHealth > 0 && npcPet.CurrentHealth > 0) {
            if (playerGoesFirst) {
                // Player's pet attacks npcPet's pet
                int damage = getDamage(playerPet, npcPet);
                if (damage < 0) {
                    damage = 0;
                }
                npcPet.TakeDamage(damage);
            } else {
                // npcPet's pet attacks player's pet
                int damage = getDamage(npcPet, playerPet);
                if (damage < 0) {
                    damage = 0;
                }
                playerPet.TakeDamage(damage);
            }

            // Switch turns
            playerGoesFirst = !playerGoesFirst;

            // Pause for 1 second before the next loop iteration
            yield return new WaitForSeconds(1);
        }

        // Determine winner and give rewards
        if (playerPet.CurrentHealth > 0) {
            OnBattleWon(playerPet);
            playerPet.UpdateCurrentHealth(playerPet.Health);
            playerPet.setSpecialAbility(false);
        } else {
            OnBattleWon(npcPet);
            npcPet.UpdateCurrentHealth(npcPet.Health);
            playerPet.UpdateCurrentHealth(playerPet.Health);
            playerPet.setSpecialAbility(false);
        }
        
    }


    private int getDamage(Pet Attacker, Pet target) {
        int attack = Attacker.Attack;
        int defense = (int)(target.Defense * target.Level / 8);
        int damage = (int)(attack * (1 - Math.Min(0.5f, defense/(4.0f*attack))));

        // Apply special abilities
        switch (Attacker.Species.Type) {
            case Species.SpeciesType.FireDragon:
            case Species.SpeciesType.FireLizard:
                // Deal 20 damage at the start of the fight
                if(!Attacker.HasUsedSpecialAbility()) {
                    damage *= 3;
                    Debug.Log("fireSpecial triple Damage"); 
                    Attacker.UseSpecialAbility();
                }
                break;
            case Species.SpeciesType.Golem:
                if (UnityEngine.Random.value < 0.1f) {
                    // 10% chance to throw a rock and do double damage of a normal attack
                    damage *= 2;
                    Debug.Log("Golem double Attack");
                }
                break;
            case Species.SpeciesType.Harpy:
                if (UnityEngine.Random.value < 0.2f) {
                    // 20% chance to attack twice in a row
                    damage *= 2;
                    Debug.Log("Harpy double Attack");
                }
                break;
            case Species.SpeciesType.Griphon:
                if (UnityEngine.Random.value < 0.2f) {
                    // 20% chance to attack twice in a row
                    damage *= 2;
                    Debug.Log("Griphon double Attack");
                }
                break;
        }
        switch (target.Species.Type) {
            case Species.SpeciesType.Crab:
                // Take 20% less damage from any attack
                damage = Mathf.FloorToInt(damage * 0.8f);
                break;
            case Species.SpeciesType.Naga:
                if (UnityEngine.Random.value < 0.25f) {
                    // 25% chance to evade the attack
                    damage = 0;
                    Debug.Log("Naga evaded Attack");
                }
                break;
        }
        return damage;
    }

    //private int getDamage(Pet Attacker, Pet target) {
    //    int attack = Attacker.Attack;
    //    int defense = (int)(target.Defense*target.Level/4);
    //    return (int)(attack-defense);
    //}
}