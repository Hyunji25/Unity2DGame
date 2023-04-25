using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public class MemberForm
{
    public string Name;
    public int Age;

    public MemberForm(string name, int age)
    {
        Name = name;
        Age = age;
    }
}

// 회원가입
// 로그인

public class ExampleManager : MonoBehaviour
{
    string URL = "https://script.google.com/macros/s/AKfycbw59VPTH0Tn65YyJeq0UoBQVHPduXTb87J0ANnEhgbgjq1SCVx9eRr3lW8_emDMerQ/exec";

    IEnumerator Start()
    {
        // 요청을 하기 위한 작업
        //UnityWebRequest request = UnityWebRequest.Get(URL);

        MemberForm member = new MemberForm("변사또", 45);

        WWWForm form = new WWWForm();

        form.AddField("Name", member.Name);
        form.AddField("Age", member.Age);

        using (UnityWebRequest request = UnityWebRequest.Post(URL, form))
        {
            yield return request.SendWebRequest();

            // 응답에 대한 작업
            print(request.downloadHandler.text);

            request.Dispose();
        }
    }
}

