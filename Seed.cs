using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed
{
    public Quality quality;
    public int price;
    public bool isWatered;
    public float growthTime;
    public string effect;

    private float currentTime = 0f;

    public Seed(Quality quality, int price, float growthTime, string effect) {
        this.quality = quality;
        isWatered = false;
        this.growthTime = growthTime;
        this.price = price;
        this.effect = effect;
    }

    public virtual Plant createPlant() {
        return new Plant(this);
    }

    public int Price {
        get { return price; }
    }

    public float GrowthTime {
       get { return growthTime; }
    }

    public string Effect {
        get { return effect; }
    }

    public void Water() {
        isWatered = true;
    }

    public bool IsFullyGrown() {
        return currentTime >= growthTime;
    }

    public void Grow(float deltaTime) {
        if (IsFullyGrown()) {
            return;
        }

        if (isWatered) {
            currentTime += deltaTime * 2f;
        } else {
            currentTime += deltaTime;
        }

        if (IsFullyGrown()) {
        }
    }
}

public class FireLilySeed : Seed {
    public FireLilySeed() : base(Quality.mythical,10000,14400F,"Increase Fire Pet Level by 1") {}
    public override Plant createPlant(){
        return new FireLily(this);
    }
}

public class WaterRoseSeed : Seed {
    public WaterRoseSeed() : base(Quality.legendary,10000,14400F,"increase Water Pet Level by 1"){}
    public override Plant createPlant() {
        return new WaterRose(this);
    }
}

public class StarLilySeed : Seed {
    public StarLilySeed() : base(Quality.average,50,4F,"Give 1 xp to Pet") {}
    public override Plant createPlant(){
        return new StarLily(this);
    }
}

public class ShadowRoseSeed : Seed {
    public ShadowRoseSeed() : base(Quality.great,2000,600F,"Increase any Pet Attack and Speed by 1") {}
    public override Plant createPlant(){
        return new ShadowRose(this);
    }
}