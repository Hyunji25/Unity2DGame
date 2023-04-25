using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using Unity.VisualScripting;

public class ProgressBar : MonoBehaviour
{
    private AsyncOperation asyncOperation;
    public Text text;
    public Text messagetext;

    public Image Loading;

    private void Awake()
    {
        Loading.fillAmount = 0;
    }

    IEnumerator Start()
    {
        //EditorApplication.isPaused = true;
        asyncOperation = SceneManager.LoadSceneAsync("Game Start");
        asyncOperation.allowSceneActivation = false;

        while (!asyncOperation.isDone)
        {
            float progress = asyncOperation.progress / 0.9f * 100f;
            text.text = progress.ToString() + "%";

            yield return null;

            Loading.fillAmount = progress;

            if (asyncOperation.progress > 0.7f)
            {
                //yield return new WaitForSeconds(2.5f);
                yield return null;

                messagetext.gameObject.SetActive(true);

                if (Input.GetMouseButtonDown(0))
                    asyncOperation.allowSceneActivation = true;
            }
        }
    }
}