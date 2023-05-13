using System.Collections.Generic;

public class Farm
{
    public List<Seed> seeds = new List<Seed>();

    public void plant(Seed seed) {
        seeds.Add(seed);
    }

    public void harvest(Seed seed) {
        if(seed.IsFullyGrown()){
            Plant plant = new Plant(seed);
            GameManager.player.inventory.AddItem(plant);
        }
    }
}
