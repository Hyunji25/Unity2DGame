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

// ȸ������
// �α���

public class ExampleManager : MonoBehaviour
{
    string URL = "https://script.google.com/macros/s/AKfycbw59VPTH0Tn65YyJeq0UoBQVHPduXTb87J0ANnEhgbgjq1SCVx9eRr3lW8_emDMerQ/exec";

    IEnumerator Start()
    {
        // ��û�� �ϱ� ���� �۾�
        //UnityWebRequest request = UnityWebRequest.Get(URL);

        MemberForm member = new MemberForm("�����", 45);

        WWWForm form = new WWWForm();

        form.AddField("Name", member.Name);
        form.AddField("Age", member.Age);

        using (UnityWebRequest request = UnityWebRequest.Post(URL, form))
        {
            yield return request.SendWebRequest();

            // ���信 ���� �۾�
            print(request.downloadHandler.text);

            request.Dispose();
        }
    }
}

