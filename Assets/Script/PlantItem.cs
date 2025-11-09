using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlantItem : MonoBehaviour
{
    public PlantObject plant;

    public TMP_Text nameTxt;
    public TMP_Text volumeTxt;
    public Image icon;

    public Button btnSelect;

    //public Image btnImage;
    public TMP_Text btnTxt;

    FarmManager fm;

    // Start is called before the first frame update
    void Start()
    {
        fm = FindObjectOfType<FarmManager>();
        plant = Instantiate(plant); // tạo bản sao runtime

        InitializeUI();
    }

    public void BuyPlant()
    {
        Debug.Log("Bought " + plant.plantName);
        fm.SelectPlant(this);
    }

    void InitializeUI()
    {
        nameTxt.text = plant.plantName;
        volumeTxt.text = plant.amountSeeds.ToString();
        icon.sprite = plant.icon;
    }

}