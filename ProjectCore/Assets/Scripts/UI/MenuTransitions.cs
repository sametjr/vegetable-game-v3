using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuTransitions : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject pages;
    [SerializeField] private GameObject shopBtn, homeBtn, settingsBtn;
    [SerializeField] private GameObject toggles, buttons;
    [SerializeField] private GameObject playBtn;
    [SerializeField] private Text levelText;
    private float resolutionX;
    private Vector3 playButtonDefaultScale;
    private float pagesAnimationTime = .6f;
    private float buttonsAnimationTime = .2f;
    private float defaultTogglesYPos = 575f;
    private float defaultButtonsYPos = -150f;
    private void Start()
    {
        resolutionX = canvas.GetComponent<CanvasScaler>().referenceResolution.x;
        playButtonDefaultScale = playBtn.GetComponent<RectTransform>().localScale;
        playBtn.GetComponent<RectTransform>().localScale = new Vector3(0, playBtn.GetComponent<RectTransform>().localScale.y, playBtn.GetComponent<RectTransform>().localScale.z);
    }


    public void GoShopPage()
    {
        SoundManager.Instance.PlaySound(SoundManager.Sounds.Click);
        LeanTween.rotateAroundLocal(shopBtn, Vector3.forward, 360, buttonsAnimationTime).setEaseInBounce();
        LeanTween.moveLocalX(pages, 0, pagesAnimationTime).setEaseOutElastic();

        playBtn.GetComponent<RectTransform>().localScale = new Vector3(0, playBtn.GetComponent<RectTransform>().localScale.y, playBtn.GetComponent<RectTransform>().localScale.z);
    }

    public void GoHomePage()
    {
        SoundManager.Instance.PlaySound(SoundManager.Sounds.Click);
        levelText.text = "LEVEL " + GameManager.Instance.level.ToString();
        GetComponent<MenuButtons>().ControlChest();
        LeanTween.rotateAroundLocal(homeBtn, Vector3.forward, 360, buttonsAnimationTime).setEaseInBounce();

        LeanTween.moveLocalX(pages, -resolutionX, pagesAnimationTime).setEaseOutElastic().setOnComplete(() =>
        {
            LeanTween.scaleX(playBtn, playButtonDefaultScale.x, .5f).setEaseInOutElastic();
        });

        LeanTween.moveLocalY(toggles, 1128, .5f).setEaseOutBack();
        LeanTween.moveLocalY(buttons, -1520, .5f).setEaseOutBack();
    }

    public void GoSettingsPage()
    {
        SoundManager.Instance.PlaySound(SoundManager.Sounds.Click);
        LeanTween.rotateAroundLocal(settingsBtn, Vector3.forward, 360, buttonsAnimationTime).setEaseInBounce();
        LeanTween.moveLocalX(pages, -resolutionX * 2, pagesAnimationTime).setEaseOutElastic();

        LeanTween.moveLocalY(toggles, defaultTogglesYPos, .5f).setEaseOutBack();
        LeanTween.moveLocalY(buttons, defaultButtonsYPos, .5f).setEaseOutBack();

        playBtn.GetComponent<RectTransform>().localScale = new Vector3(0, playBtn.GetComponent<RectTransform>().localScale.y, playBtn.GetComponent<RectTransform>().localScale.z);

    }
}
