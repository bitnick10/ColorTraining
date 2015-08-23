using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameMainScript : MonoBehaviour
{
    public GameObject CanvasSettings;
    public GameObject CanvasMain;
    private int life;
    private int score;
    private int highest;
    public int Life
    {
        get { return life; }
        set
        {
            life = value;
            GameObject.Find("/Canvas/PanelLife/Number").GetComponent<Text>().text = Life.ToString();
        }
    }
    public int Score
    {
        get { return score; }
        set
        {
            score = value;
            GameObject.Find("/Canvas/TopRightGroup/PanelScore/Number").GetComponent<Text>().text = Score.ToString();
        }
    }
    public int Highest
    {
        get { return highest; }
        set
        {
            highest = value;
            GameObject.Find("/Canvas/TopRightGroup/PanelHighest/Number").GetComponent<Text>().text = Highest.ToString();
            PlayerPrefs.SetInt("highest", highest);
            Social.ReportScore(highest, "highest", result =>
            {
                if (result)
                {
                    Debug.Log("score submission successful");
                }
                else
                {
                    Debug.Log("score submission failed");
                }
            });
        }
    }

    void Start()
    {
        UpdateLanguage();

        Social.localUser.Authenticate(result => { });
        Life = 100;
        Score = 0;
        Highest = PlayerPrefs.GetInt("highest", 0);

        CanvasSettings.SetActive(false);
        CanvasMain.SetActive(true);
    }

    void Update()
    {

    }
    public void OnShowLeaderboardClick()
    {
        Social.ShowLeaderboardUI();
    }
    public void OnSettingsClick()
    {
        CanvasSettings.SetActive(true);
        CanvasMain.SetActive(false);
    }
    public void OnResumeClick()
    {
        CanvasSettings.SetActive(false);
        CanvasMain.SetActive(true);
    }
    public void OnSimplifiedChineseClick()
    {
        PlayerPrefs.SetString("language", SystemLanguage.ChineseSimplified.ToString());
        UpdateLanguage();
    }
    public void OnEnglishClick()
    {
        PlayerPrefs.SetString("language", SystemLanguage.English.ToString());
        UpdateLanguage();
    }
    void UpdateLanguage()
    {
        string language = PlayerPrefs.GetString("language", SystemLanguage.ChineseSimplified.ToString());
        Color SelectedColor = new Color();
        Color UnselectedColor = new Color();
        Color.TryParseHexString("D38D54FF", out SelectedColor);
        Color.TryParseHexString("A08D81FF", out UnselectedColor);
        if (language == SystemLanguage.English.ToString())
        {
            CanvasSettings.transform.FindChild("ButtonSimplifiedChinese").GetComponent<Image>().color = UnselectedColor;
            CanvasSettings.transform.FindChild("ButtonEnglish").GetComponent<Image>().color = SelectedColor;

            Font englishFont = (Font)Resources.Load("Arial");
            CanvasMain.transform.FindChild("PanelLife/Title").GetComponent<Text>().text = "LIFE";
            CanvasMain.transform.FindChild("PanelLife/Title").GetComponent<Text>().font = englishFont;
            CanvasMain.transform.FindChild("TopRightGroup/PanelScore/Title").GetComponent<Text>().text = "SCORE";
            CanvasMain.transform.FindChild("TopRightGroup/PanelScore/Title").GetComponent<Text>().font = englishFont;
            CanvasMain.transform.FindChild("TopRightGroup/PanelHighest/Title").GetComponent<Text>().text = "BEST";
            CanvasMain.transform.FindChild("TopRightGroup/PanelHighest/Title").GetComponent<Text>().font = englishFont;
            CanvasMain.transform.FindChild("Button/Text").GetComponent<Text>().text = "LEADERBOARD";
            CanvasMain.transform.FindChild("Button/Text").GetComponent<Text>().font = englishFont;

            CanvasMain.transform.FindChild("PanelPick/Button/Text").GetComponent<Text>().text = "OK";
            CanvasMain.transform.FindChild("PanelPick/Button/Text").GetComponent<Text>().font = englishFont;

            CanvasSettings.transform.FindChild("ButtonResume/Text").GetComponent<Text>().text = "RESUME";
            CanvasSettings.transform.FindChild("ButtonResume/Text").GetComponent<Text>().font = englishFont;
        }
        else
        {
            CanvasSettings.transform.FindChild("ButtonSimplifiedChinese").GetComponent<Image>().color = SelectedColor;
            CanvasSettings.transform.FindChild("ButtonEnglish").GetComponent<Image>().color = UnselectedColor;
        }

    }
}
