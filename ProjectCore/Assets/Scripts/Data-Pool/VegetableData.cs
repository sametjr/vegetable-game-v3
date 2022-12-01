using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VegetableData : MonoBehaviour
{
    [SerializeField] public Vegetable beet, brocoli, caper, carrot, corn, eggplant, mushroom, onion, pumpkin, tomato;

    [HideInInspector] public List<Vegetable> vegetables;
    private void Awake()
    {
        InitList();
    }

    private void InitList()
    {
        vegetables = new List<Vegetable>();
        vegetables.Add(beet);
        vegetables.Add(brocoli);
        vegetables.Add(caper);
        vegetables.Add(carrot);
        vegetables.Add(corn);
        vegetables.Add(eggplant);
        vegetables.Add(mushroom);
        vegetables.Add(onion);
        vegetables.Add(pumpkin);
        vegetables.Add(tomato);
    }

    public Sprite GetVegetableImage(string _name)
    {
        foreach (Vegetable s in vegetables)
        {
            // Debug.Log("Scanned name --> " + s.name + " Searched name --> " + _name);
            if (_name.CompareTo(s.name) == 0)
            {
                return s.image;
            }
        }

        return null;
    }

    public ObjectPool.Vegetables GetVegetableNumber(GameObject _veg)
    {
        return _veg.GetComponent<VegetableLinker>().@enum;
    }

    public string GetVegetableName(ObjectPool.Vegetables _veg)
    {
        return vegetables[((int)_veg)].vegetable_name;
    }




}
