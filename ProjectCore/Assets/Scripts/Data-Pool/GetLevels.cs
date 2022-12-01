using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
public class GetLevels : MonoBehaviour
{
    public string URL = "https://react-level-editor.samedatac.repl.co/data";
    public static Levels levels;
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Text loadingScreenText;
    [SerializeField] private GameObject defaultButton;
    [SerializeField] private TextAsset jsonFile;
    private bool flag = false;
    public void Start()
    {
        StartCoroutine(FetchData());
    }
    public IEnumerator FetchData()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(URL))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(request.error);
            }
            else
            {
                flag = true;
                levels = new Levels();
                levels = JsonUtility.FromJson<Levels>(request.downloadHandler.text);
            }

        }
        if (flag)
        {
            GameManager.Instance.InitLevels();
            loadingScreen.SetActive(false);
        }
        else
        {
            loadingScreenText.text = "CHECK YOUR CONNECTION!";
            defaultButton.SetActive(true);
        }
        Debug.Log("Test to see if this line executed before 'LEVELS INITIALIZED'."); // It is not!!
    }

    public void defaultButtonClicked()
    {
        levels = new Levels();
        levels = JsonUtility.FromJson<Levels>(jsonFile.text);
        GameManager.Instance.InitLevels();
        loadingScreen.SetActive(false);
    }
}



[System.Serializable]
public class Level
{
    public int id;
    public string[] foods;
    public int seconds;
}

[System.Serializable]
public class Levels
{
    public Level[] levels;
}
