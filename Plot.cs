using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot : MonoBehaviour
{
    public int id;
    public Vector3 position;
    public bool isPlanted = false;
    public SpriteRenderer plant;
    BoxCollider2D plantCollider;
    public Seed seed;

    public Sprite[] plantStages;
    public int plantStage = 0;
    public float timeBetweenStages;
    public float timer;

    void Start() {
        plant = transform.GetChild(0).GetComponent<SpriteRenderer>();
        plantCollider = transform.GetChild(0).GetComponent<BoxCollider2D>();
    }

    void Update() {
       if(isPlanted) {
            timer -= Time.deltaTime;

            if(timer < 0 && plantStage<plantStages.Length -1) {
                timer = timeBetweenStages;
                plantStage++;
                updatePlant();
            }
        }
    }

    public void SetSeed(Seed seed) {

        if(seed != null) {
            this.seed = seed;
            timeBetweenStages = seed.GrowthTime/2;  
        }        
    }

    private void OnMouseDown() {
        if (isPlanted) {
            if (plantStage == plantStages.Length -1) {
                Harvest();
            }
        } else if(GameManager.plantMode){
            SetSeed(GameManager.selectedSeed);
            Plant();
        }
    }

    private void Harvest() {
        isPlanted = false;
        plant.gameObject.SetActive(false);
        GameManager.player.inventory.AddItem(seed.createPlant());
        this.seed = null;
    }

    public void Plant() {
        if(GameManager.player.inventory.HasItem(seed)) {
            isPlanted = true;
            plantStage = 0;
            updatePlant();
            timer = timeBetweenStages;
            plant.gameObject.SetActive(true);
            GameManager.player.inventory.RemoveItem(seed);
        }
    }

    public void Replant(float timer,int plantStage) {
        isPlanted = true;
        this.plantStage = plantStage;
        updatePlant();
        this.timer = timer;
        plant.gameObject.SetActive(true);
    }

    public void updatePlant() {
        plant.sprite = plantStages[plantStage];
    }
}
