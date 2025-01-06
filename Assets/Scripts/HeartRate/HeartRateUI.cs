using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ExciteOMeter;

public class HeartRateUI : MonoBehaviour
{
    public TextMeshProUGUI labelText;
    public Image connectionStatusImage;
    public TextMeshProUGUI valueText;
    public GameObject tipsText;

    public Color connectedColor = new Color(0, 1, 0);
    public Color disconnectedColor = new Color(1, 0, 0);
    public Color dangerColor = new Color(1, 0, 0);

    private HeartRateManager heartRateScript;

    // Start is called before the first frame update
    void Start()
    {
        heartRateScript = GameObject.Find("HeartrateManager").GetComponent<HeartRateManager>();

        // Setup UI children
        if (labelText == null) labelText = transform.GetComponentInChildren<TextMeshProUGUI>();
        if (tipsText == null)
        {
            tipsText = GameObject.Find("tips");
            Debug.Log("tipsnull");
        }
        if (connectionStatusImage == null) connectionStatusImage = transform.GetComponentInChildren<Image>();

        labelText.text = heartRateScript.getLabelText();
    }

    // Update is called once per frame
    void Update()
    {
        if (heartRateScript.getCurrentlyConnected() == true)
        {
            connectionStatusImage.color = connectedColor;
        }
        else
        {
            connectionStatusImage.color = disconnectedColor;
        }

        if(heartRateScript.getHeartrate() >= 100)
        //if(100 >= 100)
        {
            connectionStatusImage.color = dangerColor;
            tipsText.SetActive(true);
        }
        else
        {
            connectionStatusImage.color= connectedColor;
            tipsText.SetActive(false);
        }

        valueText.text = heartRateScript.getHeartrate().ToString("F0");
    }
}
