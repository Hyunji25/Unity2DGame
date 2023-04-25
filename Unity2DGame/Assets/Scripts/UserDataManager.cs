using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Unity.VisualScripting;
using UnityEditor.PackageManager.Requests;
using UnityEngine.SceneManagement;
using Random = System.Random;
using System;
using System.Text;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

public class UserData
{
    public string order, result, pkstr;
}

public class UserDataManager : MonoBehaviour
{
    string url = "https://script.google.com/macros/s/AKfycbwCpTNgso3eB_KB8ZjAWl-zq9LLoJmUTLxmNh5w8xbwHXFmnZzmyv298Np31gUx3tTL0w/exec";
    public UserData UD;
    public InputField IDInput, PassInput;
    private string emailPattern = @"^[\w-.]+@([\w-]+.)+[\w-]{2,4}$";
    string id, pw, pk;
    public Text txt;

    void Start()
    {
        txt.text = "";
    }

    bool SetIDPass()
    {
        id = IDInput.text.Trim();
        pw = PassInput.text.Trim();

        if (id == "" || pw == "") return false;
        else return true;
    }

    void MakePK()
    {
        Random rand = new Random();
        string strRandomChar = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

        StringBuilder rs = new StringBuilder();

        //�Ű������� ���� numLength��ŭ �����͸� ���� �� �� �ֽ��ϴ�.
        for (int i = 0; i < 10; i++)
        {
            rs.Append(strRandomChar[(int)(rand.NextDouble() * strRandomChar.Length)]);
        }
        ControllerManager.GetInstance().pk = rs.ToString();
    }

    public void Register()
    {
        MakePK();

        if (!SetIDPass())
        {
            print("���̵� �Ǵ� ��й�ȣ�� ����ֽ��ϴ�");
            return;
        }

        if (Regex.IsMatch(id, emailPattern))
        {
            // true
            string password = Security(pw);
            pk = ControllerManager.GetInstance().pk;

            print(password);

            // login
            WWWForm form = new WWWForm();
            form.AddField("order", "register");
            form.AddField("id", id);
            form.AddField("password", password);
            form.AddField("pk", pk);

            StartCoroutine(Post(form));
        }
        else
        {
            // false
            txt.text = "email ������ �ٽ� Ȯ���ϼ���!";
        }
    }

    public void Login()
    {
        if (!SetIDPass())
        {
            print("���̵� �Ǵ� ��й�ȣ�� ����ֽ��ϴ�");
            return;
        }

        if (Regex.IsMatch(id, emailPattern))
        {
            // true
            string password = Security(pw);

            print(password);

            // login
            WWWForm form = new WWWForm();
            form.AddField("order", "login");
            form.AddField("id", id);
            form.AddField("password", password);

            StartCoroutine(Post(form));
        }
        else
        {
            // false
            txt.text = "email ������ �ٽ� Ȯ���ϼ���!";
        }
    }

    private IEnumerator Post(WWWForm form)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
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

        UD = JsonUtility.FromJson<UserData>(json);

        print(UD.result);

        switch(UD.result)
        {
            case "ID�� �ߺ�": txt.text = "�ߺ��� ���̵��Դϴ�."; break;
            case "��й�ȣ Ʋ��": txt.text = "Ʋ�� ��й�ȣ�Դϴ�."; break;
            case "ID ����": txt.text = "���� ���̵��Դϴ�."; break;
            case "�α��� ����":
                ControllerManager.GetInstance().pk = UD.pkstr;
                SceneManager.LoadScene("MainMenu");
                break;
        }
    }

    public string Security(string password)
    {
        if (string.IsNullOrEmpty(password))
        {
            // true
            txt.text = "password�� �ʼ� �Է� ���Դϴ�.";
            return "null";
        }
        else
        {
            // ��ȣȭ & ��ȣȭ
            // login
            SHA256 sha = new SHA256Managed();
            byte[] hash = sha.ComputeHash(Encoding.ASCII.GetBytes(password));
            StringBuilder stringBuilder = new StringBuilder();

            foreach (byte b in hash)
            {
                stringBuilder.AppendFormat("{0:x2}", b);
            }

            return stringBuilder.ToString();
        }
    }
}