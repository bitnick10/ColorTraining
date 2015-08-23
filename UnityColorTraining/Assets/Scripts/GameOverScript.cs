using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using GoogleMobileAds.Api;

public class GameOverScript : MonoBehaviour
{
    BannerView bannerView;
    //InterstitialAd interstitial;
    public int Score
    {
        set
        {
            GameObject.Find("/Canvas/PanelScore/Number").GetComponent<Text>().text = value.ToString();
        }
    }
    void Start()
    {
        UpdateLanguage();

        bannerView = new BannerView("ca-app-pub-5734115412286766/9250985339", AdSize.Banner, AdPosition.Bottom);
        AdRequest request = new AdRequest.Builder().Build();
        bannerView.LoadAd(request);

        //interstitial = new InterstitialAd("ca-app-pub-5734115412286766/5423314134");
        //interstitial.AdLoaded += new System.EventHandler<System.EventArgs>((sender,e) =>{
        //    interstitial.Show();
        //});
        //AdRequest request = new AdRequest.Builder().Build();
        //interstitial.LoadAd(request);
        //if (interstitial.IsLoaded())
        //{
        //    interstitial.Show();
        //}

        Debug.Log("GmeOverScript Start");

        RawImage leftImage = GameObject.Find("/Canvas/RawImageLeft").GetComponent<RawImage>();
        RawImage rightImage = GameObject.Find("/Canvas/RawImageRight").GetComponent<RawImage>();

        // for debug data
        //PersistentObjectScript persistentScript = new PersistentObjectScript();
        //persistentScript.UpColors.Add(new Phoenix.HSB96(0, 0.5f, 0.5f));
        //persistentScript.UpColors.Add(new Phoenix.HSB96(60, 0.5f, 0.5f));
        //persistentScript.UpColors.Add(new Phoenix.HSB96(120, 0.5f, 0.5f));
        //persistentScript.UpColors.Add(new Phoenix.HSB96(180, 0.5f, 0.5f));
        //persistentScript.UpColors.Add(new Phoenix.HSB96(240, 0.5f, 0.5f));
        //persistentScript.UpColors.Reverse();

        Rect leftRect = new Rect(0, 0, 100, 261);
        Texture2D leftTexture = new Texture2D((int)leftRect.width, (int)leftRect.height);
        Texture2D rightTexture = new Texture2D((int)leftRect.width, (int)leftRect.height);
        var leftRects = leftRect.Splite(GameGlobalScript.Instance.UpColors.Count);
        leftRects.Reverse();
        for (int i = 0; i < GameGlobalScript.Instance.UpColors.Count; i++)
        {
            leftTexture.DrawSolidRect(leftRects[i], GameGlobalScript.Instance.UpColors[i]);
            rightTexture.DrawSolidRect(leftRects[i], GameGlobalScript.Instance.PickedColors[i]);
        }
        leftTexture.Apply();
        rightTexture.Apply();
        leftImage.texture = leftTexture;
        rightImage.texture = rightTexture;

        Score = GameGlobalScript.Instance.FinalScore;

        GameGlobalScript.Instance.UpColors.Clear();
        GameGlobalScript.Instance.PickedColors.Clear();

    }
    void Update()
    {

    }
    public void OnRestartClick()
    {
        Application.LoadLevel("Game");
    }
    public void UpdateLanguage()
    {
        string language = PlayerPrefs.GetString("language", "Simpified Chinese");
        Color SelectedColor = new Color();
        Color UnselectedColor = new Color();
        Color.TryParseHexString("D38D54FF", out SelectedColor);
        Color.TryParseHexString("A08D81FF", out UnselectedColor);
        if (language == "English")
        {
            var CanvasMain = GameObject.Find("Canvas");

            Font englishFont = (Font)Resources.Load("Arial");
            CanvasMain.transform.FindChild("PanelScore/Title").GetComponent<Text>().text = "SCORE";
            CanvasMain.transform.FindChild("PanelScore/Title").GetComponent<Text>().font = englishFont;
            CanvasMain.transform.FindChild("Button/Text").GetComponent<Text>().text = "RESTART";
            CanvasMain.transform.FindChild("Button/Text").GetComponent<Text>().font = englishFont;
        }
    }
}
