using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.VisualScripting;
using UnityEditor.PackageManager.Requests;
using UnityEngine.Networking;
using System;

public class TimeData
{
    public string order, result;
}

public class TimeSend : MonoBehaviour
{
    string url = "https://script.google.com/macros/s/AKfycbwCpTNgso3eB_KB8ZjAWl-zq9LLoJmUTLxmNh5w8xbwHXFmnZzmyv298Np31gUx3tTL0w/exec";
    public TimeData TD;
    double tm;
    string pk;

    public void Timer()
    {
        tm = ControllerManager.GetInstance().CountTime;
        pk = ControllerManager.GetInstance().pk;

        WWWForm form = new WWWForm();
        form.AddField("order", "time");
        form.AddField("time", tm.ToString());
        form.AddField("pk", pk);

        StartCoroutine(Post(form));
    }

    private IEnumerator Post(WWWForm form)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            www.downloadHandler.Dispose();

            yield return www.SendWebRequest();
            if (www.isDone)
                Response(www.downloadHandler.text);
            else
                print("Error");
            www.Dispose();
        }
    }

    void Response(string json)
    {
        if (string.IsNullOrEmpty(json)) return;

        TD = JsonUtility.FromJson<TimeData>(json);
        print(TD.result);
    }
}
