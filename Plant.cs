public class Plant {

    public Quality quality {get; }
    public string effect {get; }

    public Plant(Seed seed) {
        quality = seed.quality;
        this.effect = seed.Effect;
    }

    public virtual bool UseOnPet(Pet pet) {
        return false;
    }
}

public class FireLily : Plant {
    public FireLily(FireLilySeed seed) : base(seed) {}
    public FireLily() :base(new FireLilySeed()) {}

    public override bool UseOnPet(Pet pet) {
        if(pet.Species.ElemType == Species.Element.Fire){
            pet.GainExperience(pet.Species.ExperienceToLevelUp(pet.Level) - pet.Experience);
            return true;
        } else return false;
    }
}

public class WaterRose : Plant {
    public WaterRose(WaterRoseSeed seed) : base(seed) {}
    public WaterRose() :base(new WaterRoseSeed()) {}

    public override bool UseOnPet(Pet pet) {
        if(pet.Species.ElemType == Species.Element.Water){
            pet.GainExperience(pet.Species.ExperienceToLevelUp(pet.Level) - pet.Experience);
            return true;
        } else return false;
    }
}

public class StarLily : Plant {
    public StarLily(StarLilySeed seed) : base(seed) {}
    public StarLily() :base(new StarLilySeed()) {}

    public override bool UseOnPet(Pet pet) {
        pet.GainExperience(1);
        return true;
    }
}

public class ShadowRose : Plant {
    public ShadowRose(ShadowRoseSeed seed) : base(seed) {}
    public ShadowRose() :base(new ShadowRoseSeed()) {}

    public override bool UseOnPet(Pet pet) {
        pet.IncreaseHealth(100);
        pet.increaseAttack(100);
        return true;
    }
}