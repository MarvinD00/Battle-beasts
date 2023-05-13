using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotManager : MonoBehaviour
{
    public List<Plot> plots;

    void Start() {
        if(!GameManager.plotsInitialized) {
            GameManager.plots = new List<Plot>(FindObjectsOfType<Plot>());
            foreach (Plot plot in GameManager.plots) {
                plot.position = plot.transform.position;
            }
            GameManager.plotsInitialized = true;
        }
        int index = 0;
        plots = new List<Plot>(FindObjectsOfType<Plot>());
        foreach (Plot plot in plots) {
            plot.id = index;
            if (GameManager.plots[index].seed != null) {
                plot.SetSeed(GameManager.plots[index].seed);
                plot.Replant(GameManager.plots[index].timer, GameManager.plots[index].plantStage);
            }
            plot.transform.position = GameManager.plots[index].position; 
            index++;
        }
    }

    void OnDestroy()
    {
        // Save Plots to GameManager
        for (int i = 0; i < plots.Count; i++)
        {
            GameManager.plots[i].seed = plots[i].seed;
            GameManager.plots[i].timer = plots[i].timer;
            GameManager.plots[i].position = plots[i].transform.position; 
            GameManager.plots[i].plantStage = plots[i].plantStage; 
            GameManager.plots[i].isPlanted = plots[i].isPlanted;
             GameManager.plots[i].timeBetweenStages = plots[i].timeBetweenStages; 
        }
    }
}
