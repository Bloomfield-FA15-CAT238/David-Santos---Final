using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour
{
    private Text hudText;
    private Text messageText;

    private float timeToHideMessage;

    void Update()
    {
        if (Time.fixedTime > timeToHideMessage)
        {
            HideMessage();
        }
    }

    public void FindUIComponents()
    {
        if (messageText == null)
        {
            messageText = GameObject.Find("MessageText").GetComponent<Text>();
        }
        if (hudText == null)
        {
            hudText = GameObject.Find("HUDText").GetComponent<Text>();
        }
    }

    public void ShowMessage(string message, float timeout)
    {
        if (messageText)
        {
            messageText.text = message;
            timeToHideMessage = Time.fixedTime + timeout;
            messageText.enabled = true;
        }
    }

    public void HideMessage()
    {
        if (messageText)
        {
            messageText.enabled = false;
        }
    }

    public void UpdateHUDText(PlayerData playerData)
    {
        if (hudText)
        {
            string hud = "Score: " + playerData.score;
            if (playerData.deaths > 0)
            {
                hud += "\n" + "Deaths: " + playerData.deaths;
            }

            hudText.text = hud;
        }
    }
}