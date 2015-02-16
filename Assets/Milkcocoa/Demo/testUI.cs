using UnityEngine;
using System;
using System.Collections;
using SocketIO;

public class testUI : MonoBehaviour {
    Milkcocoa milkcocoa;
    string contentTextArea = "";
    string contentTextField = "";
    string userName = "名無しさん";
	// Use this for initialization
	void Start () {
        milkcocoa = FindObjectOfType<Milkcocoa>();
        milkcocoa.OnSend(milkcocoaEventHandler);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 50, 30), "Name");
        userName = GUI.TextField(new Rect(50, 10, 200, 20), userName);
        if (GUI.Button(new Rect(10, 50, 100, 50), "Hoge"))
        {
            milkcocoa.Send(createMessageJSON("Hoge"));
        }
        if (GUI.Button(new Rect(120, 50, 100, 50), "日本語てすと"))
        {
            milkcocoa.Send(createMessageJSON("日本語てすと"));
        }
        contentTextField = GUI.TextField(new Rect(10, 130, 400, 20), contentTextField);
        if (GUI.Button(new Rect(410, 130, 50, 20), "送信"))
        {
            milkcocoa.Send(createMessageJSON(contentTextField));
            contentTextField = "";
        }
        contentTextArea = GUI.TextArea(new Rect(10, 250, 400, 200), contentTextArea);
    }

    private JSONObject createMessageJSON(string str)
    {
        // JSONObject で階層構造を作る
        JSONObject jsonobj = new JSONObject(delegate(JSONObject values)
        {
            values.AddField("chat", delegate(JSONObject chat)
            {
                chat.AddField("name", Uri.EscapeDataString(userName));
                chat.AddField("message", Uri.EscapeDataString(str));
            });
        });
        return jsonobj;
    }
    public void milkcocoaEventHandler(SocketIOEvent e)
    {
        // e.data は JSONObjectです
        if (e.data.GetField("value").HasField("chat"))
        {
            string message = e.data.GetField("value").GetField("chat").GetField("message").str;
            string username = e.data.GetField("value").GetField("chat").GetField("name").str;
            contentTextArea = Uri.UnescapeDataString(username) + " : " + Uri.UnescapeDataString(message) + "\n" + contentTextArea;
        }
    }
}
