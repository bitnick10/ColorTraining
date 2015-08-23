using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using GoogleMobileAds.Api;

public class PanelMainScript : MonoBehaviour
{
    public GameObject PanelPick;
    public GameObject ButtonSlect;
    public GameObject BottomRect;
    public GameObject TextUp;
    public GameObject TextBottom;
    public Phoenix.HSB96 hsb;
    // Use this for initialization
    void Start()
    {
        PanelPick.SetActive(false);
        
        // random a color
        OnUpRectClick();
        BottomRect.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnButtonSelectClick()
    {
        gameObject.SetActive(false);
        PanelPick.SetActive(true);
        return;
    }
    public void OnUpRectClick()
    {
        // change color
        Image image = GameObject.Find("/Canvas/PanelMain/UpRect").GetComponent<Image>();
        hsb.H = Random.Range(0.0f, 359.9999f);
        hsb.S = Random.Range(0.0f, 1.0f);
        hsb.B = Random.Range(0.0f, 1.0f);
        TextUp.GetComponent<Text>().text = string.Format("H {0}\nS {1}\nB {2}", 0, 0, 0);
        TextBottom.GetComponent<Text>().text = string.Format("H {0}\nS {1}\nB {2}", "?", "?", "?");
        image.color = hsb.ToColor();
        BottomRect.SetActive(false);
        ButtonSlect.SetActive(true);
    }
}
