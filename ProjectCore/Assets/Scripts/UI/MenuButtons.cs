using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour
{
    [SerializeField] private Sprite orangeBG, orangeDot, blueBG, blueDot;
    [SerializeField] private Image soundBG, soundDot, hapticBG, hapticDot;
    [SerializeField] private GameObject soundDotObj, hapticDotObj;
    [SerializeField] private Button chestButton;
    [SerializeField] private GameObject menuUI, levelUI, pan;
    [SerializeField] private InLevelUI inLevelUI;
    [SerializeField] private GameObject notEnoughMoneyText;
    public bool isChestOpened = false;
    private void Awake()
    {
        inLevelUI = this.gameObject.GetComponent<InLevelUI>();
    }

    public void ToggleSound()
    {
        if (GameManager.Instance.isSoundOn) TurnSoundOff();
        else TurnSoundOn();
    }

    public void ToggleHaptic()
    {
        if (GameManager.Instance.isHapticOn) TurnHapticOff();
        else TurnHapticOn();
    }

    private void TurnHapticOff()
    {
        LeanTween.move(hapticDotObj, hapticDotObj.transform.position - Vector3.right * .7f, .3f);
        hapticDot.sprite = orangeDot;
        hapticBG.sprite = orangeBG;
        GameManager.Instance.isHapticOn = false;
    }

    private void TurnHapticOn()
    {
        LeanTween.move(hapticDotObj, hapticDotObj.transform.position + Vector3.right * .7f, .3f);
        hapticDot.sprite = blueDot;
        hapticBG.sprite = blueBG;
        GameManager.Instance.isHapticOn = true;
    }

    private void TurnSoundOff()
    {
        LeanTween.move(soundDotObj, soundDotObj.transform.position - Vector3.right * .7f, .3f);
        soundDot.sprite = orangeDot;
        soundBG.sprite = orangeBG;
        GameManager.Instance.isSoundOn = false;
    }

    private void TurnSoundOn()
    {
        LeanTween.move(soundDotObj, soundDotObj.transform.position + Vector3.right * .7f, .3f);
        soundDot.sprite = blueDot;
        soundBG.sprite = blueBG;
        GameManager.Instance.isSoundOn = true;
    }

    public void ControlChest()
    {
        chestButton.interactable = GameManager.Instance.level % 3 == 0 && !isChestOpened;
    }

    public void OpenChest()
    {
        isChestOpened = true;
        GameManager.Instance.gold += 10;
        SoundManager.Instance.PlaySound(SoundManager.Sounds.Gold);
        chestButton.interactable = false;

    }

    public void PlayButtonClicked()
    {
        SoundManager.Instance.PlaySound(SoundManager.Sounds.Click);
        levelUI.SetActive(true);
        pan.SetActive(true);
        menuUI.SetActive(false);
        inLevelUI.HideWinScreen();
    }



    public void MainMenuButtonClicked()
    {
        SoundManager.Instance.PlaySound(SoundManager.Sounds.Click);
        this.gameObject.GetComponent<MenuTransitions>().GoSettingsPage();
        levelUI.SetActive(false);
        pan.SetActive(false);
        menuUI.SetActive(true);
    }

    public void RestartLevelButtonClicked()
    {
        SoundManager.Instance.PlaySound(SoundManager.Sounds.Click);
        Pan.Instance.RestartLevel();
        inLevelUI.HideLoseScreen();
    }

    public void ExtraTimeButtonClicked()
    {
        SoundManager.Instance.PlaySound(SoundManager.Sounds.Click);
        if (GameManager.Instance.gold < 5)
        {
            notEnoughMoneyText.SetActive(true);
            StartCoroutine(CloseTextAfterAnimation());
        }
        else
        {
            inLevelUI.GiveExtraTime();
            GameManager.Instance.gold -= 5;
            inLevelUI.UpdateGoldText();
        }
    }

    private IEnumerator CloseTextAfterAnimation()
    {
        yield return new WaitForSecondsRealtime(2.5f);
        notEnoughMoneyText.SetActive(false);
    }

}
