using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameGlobalScript : MonoBehaviour
{
    public static GameGlobalScript Instance;
    public List<Phoenix.HSB96> UpColors = new List<Phoenix.HSB96>();
    public List<Phoenix.HSB96> PickedColors = new List<Phoenix.HSB96>();
    public int FinalScore;
    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        
        // first run language
        string language = PlayerPrefs.GetString("language", "null");
        if (language == "null")
        {
            switch (Application.systemLanguage)
            {
                case SystemLanguage.ChineseSimplified:
                    PlayerPrefs.SetString("language", SystemLanguage.ChineseSimplified.ToString());
                    break;
                default:
                    PlayerPrefs.SetString("language", SystemLanguage.English.ToString());
                    break;
            }
        }
    }
    void Start()
    {
        
    }
    void Update()
    {

    }
}
