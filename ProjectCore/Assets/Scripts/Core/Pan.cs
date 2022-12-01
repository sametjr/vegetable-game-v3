using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Pan : MonoBehaviour
{
    #region Singleton
    private static Pan instance = null;
    public static Pan Instance
    {

        get
        {
            if (instance == null)
            {
                instance = new GameObject("Pan").AddComponent<Pan>();
            }
            return instance;
        }
    }

    private void OnEnable()
    {
        instance = this;
        objectSpawner.SpawnObjects();
        DetectCurrentRequests();
        ui.UpdateUI();
        ui.InitCountdown();
    }

    #endregion
    [HideInInspector] public int currentMealIndex = 0;
    [SerializeField] private VegetableData vegetableData;
    [SerializeField] private InLevelUI ui;
    [SerializeField] private GameObject gold;
    [SerializeField] private GameObject goldSection;
    [SerializeField] private ObjectSpawner objectSpawner;
    private List<GameObject> insidePan;
    private List<string> insidePanNames;
    private string currentMeal;
    private List<string> currentVegetableRequests;
    private List<int> currentVegetableCounts;

    private void Awake()
    {
        InitVariables();
    }

    private void InitVariables()
    {
        insidePan = new List<GameObject>();
        insidePanNames = new List<string>();
        currentVegetableRequests = new List<string>();
        currentVegetableCounts = new List<int>();
    }

    public void DetectCurrentRequests()
    {
        currentMeal = GameManager.Instance.GetCurrentLevelRequests().foods[currentMealIndex];
        currentVegetableRequests = FoodIngredients.foodNamePairs[currentMeal].Keys.ToList<string>();
        currentVegetableCounts = FoodIngredients.foodNamePairs[currentMeal].Values.ToList<int>();

        foreach (string s in currentVegetableRequests)
        {
            Debug.Log("current meal requests -> " + s + " Its count -> " + currentVegetableCounts[currentVegetableRequests.IndexOf(s)]);
        }
    }

    public void SendObjectToPan(GameObject _veg)
    {
        string vegName = vegetableData.GetVegetableName(_veg.GetComponent<VegetableLinker>().@enum);
        Debug.Log("Clicked Object --> " + vegName);
        if (currentVegetableRequests.Contains(vegName))
        {
            if (CheckIfEnough(vegName))
            {
                LeanTween.move(_veg, GameManager.Instance.pan.transform.position, .5f).setEasePunch();
            }
            else
            {
                _veg.transform.GetChild(0).GetComponent<TouchMechanism>().isActive = false;
                insidePan.Add(_veg);
                insidePanNames.Add(vegName);
                LeanTween.move(_veg, GameManager.Instance.pan.transform.position + Vector3.back, .5f);
                Debug.Log("Eklendi!");
                if (ControlIfMealIsDone())
                {
                    Debug.Log("MEAL IS DONE!");
                    GoNextMeal();
                }
            }
        }
        else
        {
            LeanTween.move(_veg, GameManager.Instance.pan.transform.position, .5f).setEasePunch();
            Debug.Log("Kabul Etme");
        }
    }

    private void GoNextMeal()
    {
        GameManager.Instance.gold += 10;
        currentMealIndex++;

        if (currentMealIndex >= GameManager.Instance.GetMealCountInLevel())
        {
            currentMealIndex = 0;
            GameManager.Instance.GoNextLevel();
            PerformLevelFinishAnimation();
            ui.StopCountdown();
        }
        else
        {
            PerformMealFinishAnimation();
            DetectCurrentRequests();
            ui.UpdateUI();

        }
    }

    public void TimeOver()
    {
        // objectSpawner.ClearScene();
        GameManager.Instance.canPlayerInteract = false;
        ui.ShowLoseScreen();
    }

    private void PerformLevelFinishAnimation()
    {
        foreach (GameObject veg in insidePan)
        {
            LeanTween.move(veg, Vector2.zero, 1f);
            LeanTween.rotateAroundLocal(veg, Vector3.up, 720, 1f);
        }

        StartCoroutine(CleanMessLevel());
    }

    private IEnumerator CleanMessLevel()
    {
        yield return new WaitForSecondsRealtime(1f);
        foreach (GameObject _veg in insidePan) ObjectPool.SendObjectBackToQueue(_veg);
        insidePan.Clear();
        insidePanNames.Clear();

        objectSpawner.ClearScene();
        ui.ShowWinScreen();

        PerformGoldAnimation();
    }

    private void PerformMealFinishAnimation()
    {
        foreach (GameObject veg in insidePan)
        {
            LeanTween.move(veg, Vector2.zero, 1f);
            LeanTween.rotateAroundLocal(veg, Vector3.up, 720, 1f);
        }

        StartCoroutine(CleanMessMeal());

    }

    private IEnumerator CleanMessMeal()
    {
        yield return new WaitForSecondsRealtime(1f);
        foreach (GameObject _veg in insidePan) ObjectPool.SendObjectBackToQueue(_veg);
        insidePan.Clear();
        insidePanNames.Clear();

        PerformGoldAnimation();
    }

    private void PerformGoldAnimation()
    {
        gold.transform.position = Vector3.zero;
        gold.transform.localScale = Vector3.zero;
        gold.SetActive(true);
        SoundManager.Instance.PlaySound(SoundManager.Sounds.Gold);
        LeanTween.move(gold, new Vector2(-2.28f, 5.24f), 1f);
        LeanTween.rotateAroundLocal(gold, Vector3.up, 720, 1f);
        LeanTween.scale(gold, Vector3.one, .5f).setOnComplete(() => LeanTween.scale(gold, Vector3.zero, .5f));
    }

    private bool ControlIfMealIsDone()
    {
        bool flag = true;

        foreach (string _veg in currentVegetableRequests)
        {
            if (GetHowManyInPan(_veg) != currentVegetableCounts[currentVegetableRequests.IndexOf(_veg)]) flag = false;
        }

        return flag;
    }

    private int GetHowManyInPan(string veg)
    {
        int count = 0;
        foreach (string s in insidePanNames)
        {
            if (s.CompareTo(veg) == 0) count++;
        }
        return count;
    }

    private bool CheckIfEnough(string _veg)
    {
        int counter = 0;
        foreach (string s in insidePanNames)
        {
            if (s.CompareTo(_veg) == 0)
            {
                counter++;
            }
        }

        return (counter >= currentVegetableCounts[currentVegetableRequests.IndexOf(_veg)]);

        // if(counter >= currentVegetableCounts[currentVegetableRequests.IndexOf(_veg)]) return true; else return false;
    }

    // private void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.G)) DetectCurrentRequests();
    // }

    public void NextLevelClicked()
    {
        ui.HideWinScreen();
        DetectCurrentRequests();
        objectSpawner.SpawnObjects();
        ui.UpdateUI();
        ui.InitCountdown();
        GameObject.FindObjectOfType<MenuButtons>().isChestOpened = false;
    }

    public void RestartLevel()
    {
        foreach (GameObject _veg in insidePan)
        {
            ObjectPool.SendObjectBackToQueue(_veg);
        }
        insidePan.Clear();
        insidePanNames.Clear();
        objectSpawner.ClearScene();
        objectSpawner.SpawnObjects();
        currentMealIndex = 0;
        DetectCurrentRequests();
        ui.InitCountdown();
        ui.UpdateUI();
        GameManager.Instance.canPlayerInteract = true;

    }

}
