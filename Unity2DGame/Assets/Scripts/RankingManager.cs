using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RankData
{
    public string order, result;
}

public class RankingManager : MonoBehaviour
{
    string url = "https://script.google.com/macros/s/AKfycbwCpTNgso3eB_KB8ZjAWl-zq9LLoJmUTLxmNh5w8xbwHXFmnZzmyv298Np31gUx3tTL0w/exec";
    public RankData RD;
    string pk, rp, uin, d;  // rank, userindex, cleardate
    double tm;

    public void Rank()
    {
        pk = ControllerManager.GetInstance().pk;

        WWWForm form = new WWWForm();
        form.AddField("order", "rank");
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

        RD = JsonUtility.FromJson<RankData>(json);
        print(RD.result);
    }
}
