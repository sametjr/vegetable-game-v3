using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InLevelUI : MonoBehaviour
{
    [SerializeField] private Text levelText;
    [SerializeField] private Text countDownText;
    [SerializeField] private Text goldText;
    [SerializeField] private Text finalFoodName;
    [SerializeField] private Text firstVegetableCount, secondVegetableCount, thirdVegetableCount;
    [SerializeField] private Image firstVegetableImage, secondVegetableImage, thirdVegetableImage;
    [SerializeField] private GameObject lastPlusSign;
    [SerializeField] private GameObject background;
    [SerializeField] private Pan pan;
    [SerializeField] private VegetableData vegetableData;
    [SerializeField] private GameObject winScreen, LoseScreen;
    private bool shouldCountDown = true;
    private Level currentLevel;
    private string currentMeal;
    private int seconds;

    public void UpdateUI()
    {
        LeanTween.rotateAroundLocal(background, Vector3.right, 360f, .5f);
        GetCurrentLevelData();
        UpdateGoldText();
        UpdateLevelText();
        UpdateVegetableImagesAndCounts();
    }

    public void StopCountdown()
    {
        shouldCountDown = false;
    }

    public void GiveExtraTime()
    {
        seconds = 30;
        HideLoseScreen();
        GameManager.Instance.canPlayerInteract = true;
        InitCountdown(seconds);
    }

    public void InitCountdown()
    {
        seconds = currentLevel.seconds;
        UpdateCountdown();
        shouldCountDown = true;
        StartCoroutine(DecreaseTime());
    }

    public void InitCountdown(int secs)
    {
        seconds = secs;
        UpdateCountdown();
        shouldCountDown = true;
        StartCoroutine(DecreaseTime());
    }

    private void UpdateCountdown()
    {
        var mins = seconds / 60;
        var secs = seconds % 60;

        countDownText.text = secs < 10 ? mins + ":0" + secs : mins + ":" + secs;

    }

    private IEnumerator DecreaseTime()
    {
        WaitForSecondsRealtime wait = new WaitForSecondsRealtime(1f);
        while (seconds > 0 && shouldCountDown)
        {
            yield return wait;
            --seconds;
            UpdateCountdown();
        }
        if (seconds == 0) Pan.Instance.TimeOver();
        Debug.Log("Time Finished!");
    }

    private void GetCurrentLevelData()
    {
        currentLevel = GameManager.Instance.GetCurrentLevelRequests();
        currentMeal = currentLevel.foods[pan.currentMealIndex];
    }

    private void UpdateVegetableImagesAndCounts()
    {
        Debug.Log("CURRENT MEAL -----> " + FoodIngredients.foodNamePairs[currentMeal].Keys.ToList<string>()[0]);
        List<string> vegetables = FoodIngredients.foodNamePairs[currentMeal].Keys.ToList<string>();
        List<int> counts = FoodIngredients.foodNamePairs[currentMeal].Values.ToList<int>();

        firstVegetableImage.sprite = vegetableData.GetVegetableImage(vegetables[0]);
        secondVegetableImage.sprite = vegetableData.GetVegetableImage(vegetables[1]);

        firstVegetableCount.text = "x" + counts[0].ToString();
        secondVegetableCount.text = "x" + counts[1].ToString();

        if (vegetables.Count == 2)
        {
            thirdVegetableCount.gameObject.SetActive(false);
            lastPlusSign.gameObject.SetActive(false);
            thirdVegetableImage.gameObject.SetActive(false);
        }
        else
        {
            thirdVegetableCount.gameObject.SetActive(true);
            lastPlusSign.gameObject.SetActive(true);
            thirdVegetableImage.gameObject.SetActive(true);


            thirdVegetableImage.sprite = vegetableData.GetVegetableImage(vegetables[2]);
            thirdVegetableCount.text = "x" + counts[2].ToString();
        }

        finalFoodName.text = currentMeal;

    }

    private void UpdateLevelText()
    {
        levelText.text = "LEVEL" + GameManager.Instance.level.ToString();
    }

    public void UpdateGoldText()
    {
        goldText.text = GameManager.Instance.gold.ToString();
    }

    // private void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.U)) UpdateUI();
    //     if (Input.GetKeyDown(KeyCode.K)) ShowLoseScreen();
    //     if (Input.GetKeyDown(KeyCode.L)) HideLoseScreen();
    // }

    public void ShowWinScreen()
    {

        LeanTween.moveLocalX(winScreen, 0, .5f).setEaseOutBounce();
        SoundManager.Instance.PlaySound(SoundManager.Sounds.Success);
    }

    public void HideWinScreen()
    {
        LeanTween.moveLocalX(winScreen, 1160, .2f);
    }

    public void ShowLoseScreen()
    {
        LeanTween.moveLocalX(LoseScreen, 0, .5f).setEaseOutBounce();
        SoundManager.Instance.PlaySound(SoundManager.Sounds.Fail);
    }

    public void HideLoseScreen()
    {
        LeanTween.moveLocalX(LoseScreen, -1160, .2f);
    }

}
