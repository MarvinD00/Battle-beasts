using System.Collections.Generic;

public class Player {
    public List<Pet> pets;
    public Inventory inventory;
    public int gold;

    public Player() {
        pets = new List<Pet>();
        inventory = new Inventory();
        gold = 0;
    }
}