using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Vegetable", menuName = "Vegetable")]
public class Vegetable : ScriptableObject
{
    public string vegetable_name;
    public GameObject obj;
    public Sprite image;

    // public Vegetable(string _name, GameObject _obj, Sprite _img)
    // {
    //     this.vegetable_name = _name;
    //     this.obj = _obj;
    //     this.image = _img;
    // }
}
