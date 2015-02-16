unity-milkcocoa-primitive
=========================

Unity application connecting to Milkcocoa on socket.io 1.0

Includes  [fpanettieri/unity-socket.io](https://github.com/fpanettieri/unity-socket.io)  

## Usage

1. Get milkcocoa account [https://mlkcca.com/](https://mlkcca.com/)  
2. Create new milkcocoa application and get **app_id**  
3. Open Unity project `Assets/Milkcocoa/Demo/MilkcocaTest.unity`
4. Drag and drop prefab "Milkcocoa" into Your project hierarchy.
5. Edit GameObject Milkcocoa AppId and  DataStorePath
6. Edit your game code!

## Reference

<dl>
<dt>milkcocoa.OnSend(milkcocoaEventHandler)</dt>
<dd>Set enenthandler function.<br>
Now yet, eventhandler function chan defined only ONE function.</dd>
<dt>milkcocoa.Send(JSONObject)</dt>
<dd>Send message to milkcocoa server.</dd>
</dl>
