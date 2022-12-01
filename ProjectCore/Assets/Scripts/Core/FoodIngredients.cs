using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodIngredients : MonoBehaviour
{
    // public enum Foods
    // {
    //     Soup, Beef, Loaf, Salad, Sphagetti, Sandwich, Cookie, Muffin, Chips, Hotdog
    // }
    public static Dictionary<string, Dictionary<string, int>> foodNamePairs;
    private void Start()
    {
        foodNamePairs = new Dictionary<string, Dictionary<string, int>>();
        foodNamePairs["Soup"] = Soup;
        foodNamePairs["Beef"] = Beef;
        foodNamePairs["Loaf"] = Loaf;
        foodNamePairs["Salad"] = Salad;
        foodNamePairs["Sphagetti"] = Sphagetti;
        foodNamePairs["Sandwich"] = Sandwich;
        foodNamePairs["Cookie"] = Cookie;
        foodNamePairs["Muffin"] = Muffin;
        foodNamePairs["Chips"] = Chips;
        foodNamePairs["Hotdog"] = Hotdog;
    }
    public static Dictionary<string, int> Soup
    {
        get
        {
            Dictionary<string, int> ingredients = new Dictionary<string, int>();
            ingredients["Beet"] = 2;
            ingredients["Caper"] = 1;
            return ingredients;
        }
    }

    public static Dictionary<string, int> Beef
    {
        get
        {
            Dictionary<string, int> ingredients = new Dictionary<string, int>();
            ingredients["Beet"] = 3;
            ingredients["Carrot"] = 2;
            return ingredients;
        }
    }

    public static Dictionary<string, int> Loaf
    {
        get
        {
            Dictionary<string, int> ingredients = new Dictionary<string, int>();
            ingredients["Brocoli"] = 1;
            ingredients["Caper"] = 1;
            ingredients["Mushroom"] = 3;
            return ingredients;
        }
    }

    public static Dictionary<string, int> Salad
    {
        get
        {
            Dictionary<string, int> ingredients = new Dictionary<string, int>();
            ingredients["Brocoli"] = 3;
            ingredients["Caper"] = 2;
            ingredients["Mushroom"] = 2;
            return ingredients;
        }
    }

    public static Dictionary<string, int> Sphagetti
    {
        get
        {
            Dictionary<string, int> ingredients = new Dictionary<string, int>();
            ingredients["Corn"] = 1;
            ingredients["Mushroom"] = 1;
            ingredients["Tomato"] = 2;
            return ingredients;
        }
    }

    public static Dictionary<string, int> Sandwich
    {
        get
        {
            Dictionary<string, int> ingredients = new Dictionary<string, int>();
            ingredients["Brocoli"] = 2;
            ingredients["Onion"] = 2;
            ingredients["Tomato"] = 3;
            return ingredients;
        }
    }

    public static Dictionary<string, int> Cookie
    {
        get
        {
            Dictionary<string, int> ingredients = new Dictionary<string, int>();
            ingredients["Caper"] = 3;
            ingredients["Eggplant"] = 3;
            ingredients["Onion"] = 1;
            return ingredients;
        }
    }

    public static Dictionary<string, int> Muffin
    {
        get
        {
            Dictionary<string, int> ingredients = new Dictionary<string, int>();
            ingredients["Corn"] = 4;
            ingredients["Pumpkin"] = 3;
            return ingredients;
        }
    }

    public static Dictionary<string, int> Chips
    {
        get
        {
            Dictionary<string, int> ingredients = new Dictionary<string, int>();
            ingredients["Eggplant"] = 4;
            ingredients["Onion"] = 3;
            ingredients["Brocoli"] = 4;
            return ingredients;
        }
    }

    public static Dictionary<string, int> Hotdog
    {
        get
        {
            Dictionary<string, int> ingredients = new Dictionary<string, int>();
            ingredients["Beet"] = 1;
            ingredients["Mushroom"] = 4;
            ingredients["Corn"] = 2;
            return ingredients;
        }
    }



}
