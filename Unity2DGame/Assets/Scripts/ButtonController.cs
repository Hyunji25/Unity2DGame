using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Text text;
    private RectTransform rectTransform;

    private Color OldColor;

    private void Awake()
    {
        text = GetComponent<Text>();
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        text.color = OldColor;
        switch (text.text)
        {
            case "Game Start":
                SceneManager.LoadScene("Progress");
                break;
            case "Tutorial":
                SceneManager.LoadScene(text.text);
                break;
            case "Ranking":
                SceneManager.LoadScene(text.text);
                break;
            case "End Game":
                GameQuit();
                break;
            case "Game Main":
                SceneManager.LoadScene("MainMenu");
                break;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OldColor = text.color;
        text.color = Color.white;
    }

    public void GameQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 어플리케이션 종료
#endif
    }
}
