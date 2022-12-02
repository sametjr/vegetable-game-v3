using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    #region Singleton
    private static SoundManager instance = null;
    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("SoundManager").AddComponent<SoundManager>();
            }
            return instance;
        }
    }

    private void OnEnable()
    {
        instance = this;
    }

    #endregion
    [SerializeField] private AudioClip failSound, successSound;
    [SerializeField] private AudioClip clickSound, switchSound;
    [SerializeField] private AudioClip rolloverSound;
    [SerializeField] private AudioClip goldSound;
    [SerializeField] private AudioSource audioSource;
    private List<AudioClip> clips;
    public enum Sounds
    {
        Success, Fail, Click, Switch, Rollover, Gold
    }
    private void Start()
    {
        clips = new List<AudioClip>();
        clips.Add(successSound);
        clips.Add(failSound);
        clips.Add(clickSound);
        clips.Add(switchSound);
        clips.Add(rolloverSound);
        clips.Add(goldSound);
    }
    public void PlaySound(Sounds _sound)
    {
        if (GameManager.Instance.isSoundOn) audioSource.PlayOneShot(clips[((int)_sound)]);
    }

}

