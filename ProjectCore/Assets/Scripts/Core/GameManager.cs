using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager instance = null;
    public static GameManager Instance
    {

        get
        {
            if (instance == null)
            {
                instance = new GameObject("GameManager").AddComponent<GameManager>();
            }
            return instance;
        }
    }

    private void OnEnable()
    {
        instance = this;
    }

    #endregion

    private Levels levels;
    private int currentLevel = 1;
    private int totalGold = 0;
    private bool _isSoundOn = true;
    private bool _isHapticOn = true;
    private bool _canPlayerInteract = true;
    [SerializeField] private Camera _cam;
    [SerializeField] private GameObject _pan;

    public Camera cam => this._cam;
    public GameObject pan => this._pan;
    public int level => this.currentLevel;
    public int gold
    {
        get { return this.totalGold; }
        set { this.totalGold = value; }
    }
    public bool canPlayerInteract
    {
        get { return this._canPlayerInteract; }
        set { this._canPlayerInteract = value; }
    }
    public bool isSoundOn
    {
        get { return this._isSoundOn; }
        set { this._isSoundOn = value; }
    }

    public bool isHapticOn
    {
        get { return this._isHapticOn; }
        set { this._isHapticOn = value; }
    }



    public void InitLevels()
    {
        this.levels = GetLevels.levels;
        Debug.Log("Levels Initialized!");
        // Debug.Log("3. Level first food : " + levels.levels[2].foods[0]);
    }

    public Level GetCurrentLevelRequests()
    {
        return this.levels.levels[currentLevel - 1];
    }

    public int GetMealCountInLevel()
    {
        return this.levels.levels[currentLevel - 1].foods.Length;
    }

    public void GoNextLevel()
    {
        this.currentLevel = this.currentLevel == this.levels.levels.Length ? this.currentLevel : ++this.currentLevel;
    }


}
