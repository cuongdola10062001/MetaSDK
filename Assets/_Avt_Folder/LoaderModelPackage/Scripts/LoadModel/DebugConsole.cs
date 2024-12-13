using UnityEngine;
using UnityEngine.UI;

public class DebugConsole : MonoBehaviour
{
    //public RectTransform displayRect;
    public Text consoleText; 
    private string log = "";
    //float initHeight;

    void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        log += $"{type}: {logString}\n";
        if (consoleText != null)
        {
            consoleText.text = log;
        }

        //initHeight=displayRect.anchoredPosition.y;
    }

   /* public void ChangeDisplayPosition(float newPos)
    {
        displayRect.anchoredPosition = new Vector2(displayRect.anchoredPosition.x, initHeight + newPos);
    }*/
}
