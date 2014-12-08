#region License
/*
 * Milkcocoa.cs
 *
 * The MIT License
 *
 * Copyright (c) 2014 m2wasabi
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */
#endregion

using System.Collections;
using UnityEngine;
using SocketIO;

public class milkcocoa : MonoBehaviour
{
    private SocketIOComponent socket;

    public void Start()
    {
        GameObject go = GameObject.Find("SocketIO");
        socket = go.GetComponent<SocketIOComponent>();

        socket.On("open", OnSocketOpen);
        socket.On("error", OnSocketError);
        socket.On("close", OnSocketClose);
        socket.On("a", OnMcCallback);
        socket.On("b", OnMcPush);
        socket.On("c", OnMcSet);
        socket.On("d", OnMcRemove);
        socket.On("e", OnMcSend);

        StartCoroutine("AddEventEmitter");


//        StartCoroutine("BeepBoop");
    }

    private IEnumerator BeepBoop()
    {
        // wait 1 seconds and continue
        yield return new WaitForSeconds(1);

        socket.Emit("beep");

        // wait 3 seconds and continue
        yield return new WaitForSeconds(3);

        socket.Emit("beep");

        // wait 2 seconds and continue
        yield return new WaitForSeconds(2);

        socket.Emit("beep");

        // wait ONE FRAME and continue
        yield return null;

        socket.Emit("beep");
        socket.Emit("beep");
    }

    public void OnSocketOpen(SocketIOEvent e)
    {
        Debug.Log("[SocketIO] Open received: " + e.name + " " + e.data);
    }

    public void OnSocketError(SocketIOEvent e)
    {
        Debug.Log("[SocketIO] Error received: " + e.name + " " + e.data);
    }

    public void OnSocketClose(SocketIOEvent e)
    {
        Debug.Log("[SocketIO] Close received: " + e.name + " " + e.data);
    }
    public void OnMcCallback(SocketIOEvent e)
    {
        Debug.Log("[SocketIO] McCallback received: " + e.name + " " + e.data);

        if (e.data == null) { return; }

        Debug.Log(
            "#####################################################" +
            "THIS: " + e.data.GetField("this").str +
            "#####################################################"
        );
    }
    public void OnMcPush(SocketIOEvent e)
    {
        Debug.Log("[SocketIO] McPush received: " + e.name + " " + e.data);

        if (e.data == null) { return; }

        Debug.Log(
            "#####################################################" +
            "THIS: " + e.data.GetField("this").str +
            "#####################################################"
        );
    }
    public void OnMcSet(SocketIOEvent e)
    {
        Debug.Log("[SocketIO] McSet received: " + e.name + " " + e.data);

        if (e.data == null) { return; }

        Debug.Log(
            "#####################################################" +
            "THIS: " + e.data.GetField("this").str +
            "#####################################################"
        );
    }
    public void OnMcRemove(SocketIOEvent e)
    {
        Debug.Log("[SocketIO] McRemove received: " + e.name + " " + e.data);

        if (e.data == null) { return; }

        Debug.Log(
            "#####################################################" +
            "THIS: " + e.data.GetField("this").str +
            "#####################################################"
        );
    }
    public void OnMcSend(SocketIOEvent e)
    {
        Debug.Log("[SocketIO] McSend received: " + e.name + " " + e.data);

        if (e.data == null) { return; }

        Debug.Log(
            "#####################################################" +
            "THIS: " + e.data.GetField("this").str +
            "#####################################################"
        );
    }
    private IEnumerator AddEventEmitter()
    {
        // wait 1 seconds and continue
        yield return new WaitForSeconds(1);
        string path = "unitytest";
        AddDataStoreEvent("push", path);
        AddDataStoreEvent("set", path);
        AddDataStoreEvent("remove", path);
        AddDataStoreEvent("send", path);
    }

    private void AddDataStoreEvent(string eventname, string path)
    {
        // add listener [send] event
        JSONObject jsonobj = new JSONObject(JSONObject.Type.OBJECT);
        jsonobj.AddField("event", eventname);
        jsonobj.AddField("path", path);
        socket.Emit("on", jsonobj);
    }
}
