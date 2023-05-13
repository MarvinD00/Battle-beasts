using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory {
    public Dictionary<System.Type, int> plantCounts = new Dictionary<System.Type, int>();
    public Dictionary<System.Type, int> seedCounts = new Dictionary<System.Type, int>();
    
    public void AddItem(Plant plant) {
        if (plantCounts.ContainsKey(plant.GetType())) {
            plantCounts[plant.GetType()]++;
        } 
        else {
            plantCounts.Add(plant.GetType(), 1);
        }
    }
    
    public void AddItem(Seed seed) {
        if (seedCounts.ContainsKey(seed.GetType())) {
            seedCounts[seed.GetType()]++;
        } 
        else {
            seedCounts.Add(seed.GetType(), 1);
        }
    }

    public void RemoveItem(Plant plant) {
        if (plantCounts.ContainsKey(plant.GetType())) {
            if (plantCounts[plant.GetType()] > 1) {
                plantCounts[plant.GetType()]--;
            } else {
                plantCounts.Remove(plant.GetType());
            }
        } else {
            Debug.Log("Plant not found in inventory");
        }
    }

    public void RemoveItem(Seed seed) {
        if (seedCounts.ContainsKey(seed.GetType())) {
            if (seedCounts[seed.GetType()] > 1) {
                seedCounts[seed.GetType()]--;
            } else {
                seedCounts.Remove(seed.GetType());
            }
        } else {
            Debug.Log("Seed not found in inventory");
        }
    }

    public bool HasItem(Seed seed) {
        if (seedCounts.ContainsKey(seed.GetType())) {
            if (seedCounts[seed.GetType()] > 0) {
                return true;
            }
        }
        return false;
    }
}
