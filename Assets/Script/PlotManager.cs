using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
public class PlotManager : MonoBehaviour
{
    bool isPlanted = false;
    SpriteRenderer plant;
    BoxCollider2D plantCollider;

    int plantStage = 0;
    float timeBtwStages = 2f;
    float timer;

    PlantObject selectedPlant;


    FarmManager fm;

    void Start()
    {
        plant = transform.GetChild(0).GetComponent<SpriteRenderer>();
        plantCollider = transform.GetChild(0).GetComponent<BoxCollider2D>();
        fm = FindObjectOfType<FarmManager>();
    }

     void Update()
    {
        if(isPlanted)
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
        if (isPlanted)
        {
            if (plantStage == selectedPlant.plantStages.Length - 1 && !fm.isPlanting)
            {
                Debug.Log("Ready to harvest");
                Harvest();
            }
        }
      
        else if (fm.isPlanting && fm.selectPlant.plant.amountSeeds > 0)
        {
            Debug.Log("Ready to plant");
            Plant(fm.selectPlant.plant);
        }

        Debug.Log("Plot clicked");

    }

    void Harvest()
    {
        Debug.Log("Harvesting");
        isPlanted = false;
        plant.gameObject.SetActive(false);
        fm.Transaction(selectedPlant.sellPrice);
    }

    void Plant(PlantObject plantObject)
    {
        selectedPlant = plantObject;
        Debug.Log("Planting");
        isPlanted = true;

        fm.Seeds(1);

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