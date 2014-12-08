unity-milkcocoa-primitive
=========================

Unity application connecting to Milkcocoa on socket.io 1.0

Includes  [fpanettieri/unity-socket.io](https://github.com/fpanettieri/unity-socket.io)  

## Usage

1. Get milkcocoa account [https://mlkcca.com/](https://mlkcca.com/)  
2. Create new milkcocoa application and get **app_id**  
3. Open Unity project `Assets/Milkcocoa/Scenes/MilkcocaTest.unity`
4. Click **SocketIO** in Hierarchy tab and Edit `Url` in **SocketIOComponent** below  
  wss://**your app_id**.mlkcca.com/socket.io/?EIO=2&transport=websocket  
5. Edit `testtool/milkcocoatest.html` (line:82) **appId** insert your **app_id**  
6. run `testtool/milkcocoatest.html`
7. run `Assets/Milkcocoa/Scenes/MilkcocaTest.unity`

