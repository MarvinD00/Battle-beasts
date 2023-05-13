public class Species {
    public enum SpeciesType {
        FireDragon,
        FireLizard,
        Naga,
        Crab,
        Golem,
        Harpy,
        Griphon,
    }

    public enum Element {
        Fire,
        Water,
        Earth,
        Air,
        //Light,
        //Dark
    }

    private SpeciesType speciesType;
    private Element element;
    private int baseHealth;
    private int baseAttack;
    private int baseDefense;
    private int baseSpeed;
    private int healthPerLevel;
    private int attackPerLevel;
    private int defensePerLevel;
    private int speedPerLevel;

    public Species(SpeciesType speciesType, Element element, int baseHealth,
    int baseAttack, int baseDefense, int baseSpeed, int speedPerLevel,
    int healthPerLevel, int attackPerLevel, int defensePerLevel) {

        this.speciesType = speciesType;
        this.element = element;
        this.baseHealth = baseHealth;
        this.baseAttack = baseAttack;
        this.baseDefense = baseDefense;
        this.baseSpeed = baseSpeed;
        this.healthPerLevel = healthPerLevel;
        this.attackPerLevel = attackPerLevel;
        this.defensePerLevel = defensePerLevel;
        this.speedPerLevel = speedPerLevel;
    }

    public SpeciesType Type {
        get { return speciesType; }
    }

    public Element ElemType {
       get { return element; }
    }

    public int BaseHealth {
        get { return baseHealth; }
    }

    public int BaseAttack {
        get { return baseAttack; }
    }

    public int BaseDefense {
        get { return baseDefense; }
    }

    public int BaseSpeed {
        get { return baseSpeed; }
    }

    public int HealthPerLevel {
        get { return healthPerLevel; }
    }

    public int AttackPerLevel {
        get { return attackPerLevel; }
    }

    public int DefensePerLevel {
        get { return defensePerLevel; }
    }

    public int SpeedPerLevel {
        get { return speedPerLevel; }
    }
    
    public int ExperienceToLevelUp(int level) {
        return  (int)(10 * System.Math.Pow(1.1, level - 1));
    }
}

public class FireDragon : Species {
    public FireDragon() : base(SpeciesType.FireDragon, Element.Fire, 100, 20, 10, 5, 1, 10, 3, 2) {}
}

public class FireLizard : Species {
    public FireLizard() : base(SpeciesType.FireLizard, Element.Fire, 120, 18, 10, 5, 2, 12, 3, 2) {}
}

public class Naga : Species {
    public Naga() : base(SpeciesType.Naga, Element.Water, 140, 15, 12, 5, 2, 14, 3, 2) {}
}

public class Crab : Species {
    public Crab() : base(SpeciesType.Crab, Element.Water, 160, 12, 14, 3, 1, 16, 2, 3) {}
}

public class Golem : Species {
    public Golem() : base(SpeciesType.Golem, Element.Earth, 180, 10, 16, 2, 1, 18, 2, 3) {}
}

public class Harpy : Species {
    public Harpy() : base(SpeciesType.Harpy, Element.Air, 120, 17, 13, 7, 3, 12, 3, 2) {}
}

public class Gryphon : Species {
    public Gryphon() : base(SpeciesType.Griphon, Element.Air, 110, 18, 14, 6, 2, 11, 3, 2) {}
}