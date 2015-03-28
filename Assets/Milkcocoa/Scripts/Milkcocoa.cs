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

using System;
using System.Collections;
using UnityEngine;
using SocketIO;
using System.Text;

public class Milkcocoa : MonoBehaviour
{
    private SocketIOComponent socket;
    public string appId = "io-xi0ze9taq";
    public string dataStorePath = "unity";

    bool connection = false;

    public void Awake()
    {
        Debug.Log("Awake Milkcocoa.");
        socket = GetComponent<SocketIOComponent>();
        socket.url = "wss://" + appId + ".mlkcca.com/socket.io/?EIO=2&transport=websocket";
    }
    public void Start()
    {

        socket.On("open", OnSocketOpen);
        socket.On("error", OnSocketError);
        socket.On("close", OnSocketClose);

        socket.Connect();

    }

    public void OnSocketOpen(SocketIOEvent e)
    {
        connection = true;
        Debug.Log("[SocketIO] Open received: " + e.name + " " + e.data);
    }

    public void OnSocketError(SocketIOEvent e)
    {
        connection = false;
        Debug.Log("[SocketIO] Error received: " + e.name + " " + e.data);
    }

    public void OnSocketClose(SocketIOEvent e)
    {
        connection = false;
        Debug.Log("[SocketIO] Close received: " + e.name + " " + e.data);
    }

    public void OnCallback(Action<SocketIOEvent> callback)
    {
        socket.On("a", callback);
    }
    public void OnPush(Action<SocketIOEvent> callback)
    {
        socket.On("b", callback);
        AddDataStoreEvent("push", dataStorePath);
    }
    public void OnSet(Action<SocketIOEvent> callback)
    {
        socket.On("c", callback);
        AddDataStoreEvent("set", dataStorePath);
    }
    public void OnRemove(Action<SocketIOEvent> callback)
    {
        socket.On("d", callback);
        AddDataStoreEvent("remove", dataStorePath);
    }
    public void OnSend(Action<SocketIOEvent> callback)
    {
        socket.On("e", callback);
        AddDataStoreEvent("send", dataStorePath);
    }
    private void AddDataStoreEvent(string eventname, string path)
    {
        StartCoroutine(waitDataStoreEvent(eventname, path));
    }

    private IEnumerator waitDataStoreEvent(string eventname, string path)
    {
        if (!connection)
        {
            yield return new WaitForSeconds(1.0f);
        }
        // add listener [send] event
        JSONObject jsonobj = new JSONObject(JSONObject.Type.OBJECT);
        jsonobj.AddField("event", eventname);
        jsonobj.AddField("path", path);
        socket.Emit("on", jsonobj);
        yield break;
    }

    public void Send(JSONObject jsonData)
    {
        if(connection)
        {
            JSONObject jsonobj = new JSONObject(JSONObject.Type.OBJECT);
            jsonobj.AddField("path", dataStorePath);
            jsonobj.AddField("value", jsonData);
            socket.Emit("send", jsonobj);
        }
        else
        {
            Debug.Log("[Milkcocoa] Not connected");
        }

    }
}
