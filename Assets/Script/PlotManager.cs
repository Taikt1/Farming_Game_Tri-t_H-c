using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
public class PlotManager : MonoBehaviour
{
    bool isPlanned = false;
    SpriteRenderer plant;
    BoxCollider2D plantCollider;

    int plantStage = 0;
    float timeBtwStages = 2f;
    float timer;

    public PlantObject selectedPlant;


    void Start()
    {
        plant = transform.GetChild(0).GetComponent<SpriteRenderer>();
        plantCollider = transform.GetChild(0).GetComponent<BoxCollider2D>();
    }

     void Update()
    {
        if(isPlanned)
        {
            timer -= Time.deltaTime;
            if (timer < 0 && plantStage < selectedPlant.plantStages.Length-1)
            {
                timer = selectedPlant.timeBtwStages;
                plantStage++;
                UpdatePlant();
            }
        }
    }

    private void OnMouseDown()
    {
        if (plantStage == selectedPlant.plantStages.Length-1)
        {
            Harvest();
        }
        else
        {
            Plant();
        }

        Debug.Log("Plot clicked");

    }

    void Harvest()
    {
        Debug.Log("Harvesting");
        isPlanned = false;
        plant.gameObject.SetActive(false);
    }

    void Plant()
    {
        Debug.Log("Planting");
        isPlanned = true;
        plantStage = 0;
        UpdatePlant();
        timer = selectedPlant.timeBtwStages;
        plant.gameObject.SetActive(true);
        
    }

    void UpdatePlant()
    {
       plant.sprite = selectedPlant.plantStages[plantStage];
        plantCollider.size = plant.sprite.bounds.size; 
        plantCollider.offset = new Vector2(0, plant.sprite.bounds.size.y / 2);
    }
}