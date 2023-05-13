using System;

public class Pet {

    public event Action<int> OnDamageTaken = delegate {};
    public int currentHealth;
    public bool isOnAdventure;
    public float adventureTimer;

    private string name;
    private Species species;
    private int level;
    private int experience;
    private int health;
    private int attack;
    private int defense;
    private int speed;
    private bool hasUsedSpecialAbility = false;


    public Pet(string name, Species species) {
        this.name = name;
        this.species = species;
        this.level = 1;
        this.experience = 0;
        this.health = species.BaseHealth;
        this.attack = species.BaseAttack;
        this.defense = species.BaseDefense;
        this.speed = species.BaseSpeed;
        this.currentHealth = this.health;
        this.isOnAdventure = false;
        OnDamageTaken = new Action<int>(OnDamageTaken);
    }

    public Pet(string name, Species species, int level) {
        this.name = name;
        this.species = species;
        this.level = level;
        this.experience = 0;
        this.health = species.BaseHealth + (species.HealthPerLevel*(level-1));
        this.attack = species.BaseAttack + (species.AttackPerLevel*(level-1));
        this.defense = species.BaseDefense + (species.DefensePerLevel*(level-1));
        this.speed = species.BaseSpeed + (species.SpeedPerLevel*(level-1));
        this.currentHealth = this.health;
        this.isOnAdventure = false;
        OnDamageTaken = new Action<int>(OnDamageTaken);
    }

    public string Name {
        get { return name; }
    }

    public Species Species {
        get { return species; }
    }

    public int Level {
        get { return level; }
    }

    public int Experience {
        get { return experience; }
    }

    public int Health {
        get { return health; }
    }

    public int CurrentHealth {
        get { return currentHealth; }
    }

    public int Attack {
        get { return attack; }
    }

    public int Defense {
        get { return defense; }
    }

    public int Speed {
        get { return speed; }
    }

    public Species.Element Element {
        get { return species.ElemType; }
    }

    public void UseSpecialAbility() {
        hasUsedSpecialAbility = true;
    }

    public void setSpecialAbility(bool value) {
        this.hasUsedSpecialAbility = value;
    }

    public bool HasUsedSpecialAbility() {
        return hasUsedSpecialAbility;
    }
    
    public void UpdateCurrentHealth(int newHealth) {
        currentHealth = newHealth;
    }

    public void GainExperience(int experience) {
        this.experience += experience;
        if (this.experience >= species.ExperienceToLevelUp(level)) {
            this.experience -= species.ExperienceToLevelUp(level);
            this.level++;
            this.health += species.HealthPerLevel;
            this.currentHealth = this.health;
            this.attack += species.AttackPerLevel;
            this.defense += species.DefensePerLevel;
            this.speed += species.SpeedPerLevel;
        }
    }

    public void IncreaseHealth(int inc) {
        this.health += inc;
        this.currentHealth = health;
    }

    public void increaseAttack(int inc) {
        this.attack += inc;
    }

    public void increaseDefense(int inc) {
        this.defense += inc;
    }

    public void increaseSpeed(int inc) {
        this.speed += inc;
    }
    
    public void TakeDamage(int damage) {
        currentHealth -= damage;
        OnDamageTaken.Invoke(damage);
        if (currentHealth < 0) {
            currentHealth = 0;
        }
    }

    public bool IsDefeated() {
        return currentHealth == 0;
    }
}

