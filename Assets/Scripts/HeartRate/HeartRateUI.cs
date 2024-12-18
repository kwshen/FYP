using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using ExciteOMeter;

public class HeartRateUI : MonoBehaviour
{
    public TextMeshProUGUI labelText;
    public Image connectionStatusImage;
    public TextMeshProUGUI valueText;

    public Color connectedColor = new Color(0, 1, 0);
    public Color disconnectedColor = new Color(1, 0, 0);

    public HeartRateManager heartRateScript;
    private string heartRateManagerName = "HeartrateManager";

    // Start is called before the first frame update
    void Start()
    {
        // Setup UI children
        if (labelText == null) labelText = transform.GetComponentInChildren<TextMeshProUGUI>();
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


        valueText.text = heartRateScript.getHeartrate().ToString("F0");
    }
}
