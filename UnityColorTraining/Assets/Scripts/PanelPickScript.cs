using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PanelPickScript : MonoBehaviour
{
    public GameObject PanelMain;
    Phoenix.HSB96 hsb;
    Vector3 lastMousePosition;
    // Use this for initialization
    void Start()
    {
        PaintPickerHue();
        PaintPickerSB();
    }
    void PaintPickerHue()
    {
        var tmpTexture = new Texture2D(100, 10);
        for (int y = 0; y < tmpTexture.height; y++)
        {
            for (int x = 0; x < tmpTexture.width; x++)
            {
                Phoenix.HSB96 hsb = new Phoenix.HSB96();
                hsb.H = (float)x / 99 * 360;
                if (hsb.H >= 360)
                {
                    hsb.H = 0;
                }
                hsb.S = 1.0f;
                hsb.B = 1.0f;
                //filling the temporary texture with the target texture
                tmpTexture.SetPixel(x, y, hsb.ToColor());
            }
        }
        tmpTexture.Apply();
        RawImage rawImageHue = transform.GetChild(2).gameObject.GetComponent<RawImage>();
        rawImageHue.texture = tmpTexture;
    }
    void PaintPickerSB()
    {
        var tmpTexture = new Texture2D(256, 256);
        for (int y = 0; y < tmpTexture.height; y++)
        {
            for (int x = 0; x < tmpTexture.width; x++)
            {
                Phoenix.HSB96 hsb = new Phoenix.HSB96();
                hsb.H = this.hsb.H;
                hsb.S = (float)x / 255;
                hsb.B = (float)y / 255;
                //filling the temporary texture with the target texture
                tmpTexture.SetPixel(x, y, hsb.ToColor());
            }
        }
        tmpTexture.Apply();
        RawImage rawImageSB = transform.GetChild(1).gameObject.GetComponent<RawImage>();
        rawImageSB.texture = tmpTexture;
    }
    void PaintPreview()
    {
        Image image = transform.GetChild(0).gameObject.GetComponent<Image>();
        image.color = hsb.ToColor();
    }
    public void OnOkClick()
    {
        gameObject.SetActive(false);
        PanelMain.SetActive(true);
        PanelMainScript panelMainScript = PanelMain.GetComponent<PanelMainScript>();
        PanelMain.GetComponent<PanelMainScript>().ButtonSlect.SetActive(false);
        PanelMain.GetComponent<PanelMainScript>().BottomRect.SetActive(true);
        PanelMain.GetComponent<PanelMainScript>().BottomRect.GetComponent<Image>().color = hsb.ToColor();

        PanelMain.GetComponent<PanelMainScript>().TextUp.GetComponent<Text>().text = string.Format("H {0}\nS {1}%\nB {2}%", (int)panelMainScript.hsb.H, (int)(panelMainScript.hsb.S * 100), (int)(panelMainScript.hsb.B * 100));
        PanelMain.GetComponent<PanelMainScript>().TextBottom.GetComponent<Text>().text = string.Format("H {0}\nS {1}%\nB {2}%", (int)hsb.H, (int)(hsb.S * 100), (int)(hsb.B * 100));

        GameMainScript mainScript = GameObject.Find("GameMain").GetComponent<GameMainScript>();

        GameGlobalScript.Instance.UpColors.Add(panelMainScript.hsb);
        GameGlobalScript.Instance.PickedColors.Add(hsb);

        float gap = Mathf.Abs(panelMainScript.hsb.H - hsb.H) + Mathf.Abs(panelMainScript.hsb.S - hsb.S) * 100 + Mathf.Abs(panelMainScript.hsb.B - hsb.B) * 100;
        if (gap < 15)
        {
            mainScript.Score += 3;
            mainScript.Life -= 10;
        }
        else if (gap < 30)
        {
            mainScript.Score += 2;
            mainScript.Life -= 10;
        }
        else if (gap < 45)
        {
            mainScript.Score += 1;
            mainScript.Life -= 30;
        }
        else
        {
            mainScript.Score += 0;
            mainScript.Life -= 40;
        }
        if (mainScript.Score > mainScript.Highest) {
            mainScript.Highest = mainScript.Score;
        }
        if (mainScript.Life <= 0)
        {
            mainScript.Life = 0;
            Debug.Log("game over");
            GameGlobalScript.Instance.FinalScore = mainScript.Score;
            Application.LoadLevel("GameOver");
        }
    }
    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Time.deltaTime);
        if (Input.GetMouseButton(0))
        {
            if (lastMousePosition != Input.mousePosition)
            {
                Rect rectSB = transform.GetChild(1).gameObject.GetComponent<RectTransform>().ToScreenRect();
                Rect rectHue = transform.GetChild(2).gameObject.GetComponent<RectTransform>().ToScreenRect();
                if (rectSB.Contains(Input.mousePosition))
                {
                    Vector2 pos = rectSB.PointPosInPercentage(Input.mousePosition);
                    hsb.S = pos.x;
                    hsb.B = pos.y;
                    PaintPreview();
                }
                if (rectHue.Contains(Input.mousePosition))
                {
                    hsb.H = (float)(rectHue.PointPosInPercentage(Input.mousePosition).x * 359.999999);
                    PaintPickerSB();
                    PaintPreview();
                }
                lastMousePosition = Input.mousePosition;
            }
        }
    }

}
