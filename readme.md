# เกี่ยวกับ Colyseus
Colyseus เป็น Networking Framework สำหรับ NodeJS, ที่มีระบบ Match Making ที่ทำให้สามารถสร้างห้อง, เข้าร่วมห้องได้. 

สามารถส่ง Message จาก Server ไปหา Client หรือ จาก Client ไปหา Server ได้โดย Function ส่ง Message ต่างๆ, สามารถส่ง Message ประเภทใดก็ได้ (แต่ตอนกำหนด Event ตอนรับข้อมูลต้องกำหนดประเภทข้อมูลให้ตรงกัน)

นอกจากการส่ง Message โดย Function ส่ง Message ต่างๆ แล้ว มันยังมีระบบ State Synchronization, สามารถกำหนด State ให้กับห้องได้, State คือข้อมูลที่ถูกจัดการโดย Server เท่านั้น เมื่อมีการเปลี่ยนแปลงจะทำการ Sync ไปให้ Client, โดยจะ Sync เฉพาะข้อมูลที่มีการเปลี่ยนแปลงเท่านั้น ทำให้ข้อมูลเล็ก/ใหญ่ตามจำนวนข้อมูลที่มีการเปลี่ยนแปลง

# สร้างโปรเจคของ Server
- ติดตั้ง [NodeJS](https://nodejs.org/)
- สร้างโปรเจคของ Server โดยใช้ cmd หรือ terminal, โดยใช้คำสั่ง `npm create colyseus-app@latest`

![](screenshots/1.png)

# สร้างโปรเจคของ Client
- ติดตั้ง [Unity](https://unity.com/)
- สร้างโปรเจคใหม่

![](screenshots/2.png)

- เปิดโปรเจคที่สร้างไว้
- ติดตั้ง Library Colyseus ของ Unity โดยใช้ Package Manager, สามารถเปิดได้จากเมนู `Window` -> `Package Manager`

![](screenshots/3.png)

- ใน `Package Manager`, กดปุ่มเมนู `+` -> `Add package from git URL...`

![](screenshots/4.png)

- ใส่ `https://github.com/colyseus/colyseus-unity3d.git#upm`, แล้วกด `Add`

![](screenshots/5.png)

![](screenshots/6.png)

# เกี่ยวกับ State Schema และ Room
- สามารถกำหนดห้องกี่ประเภทก็ได้ แต่ต้องสร้าง Class ของห้องแต่ละประเภทก่อน
- สามารถกำหนด State Schema ให้กับห้องแต่ละประเภทได้ (หรือจะไม่กำหนดก็ได้ เป็นห้องแบบที่ไม่มี State), State คือข้อมูลที่ถูกจัดการโดย Server เท่านั้น เมื่อมีการเปลี่ยนแปลงจะทำการ Sync ไปให้ Client, โดยจะ Sync เฉพาะข้อมูลที่มีการเปลี่ยนแปลงเท่านั้น ทำให้ข้อมูลเล็ก/ใหญ่ตามจำนวนข้อมูลที่มีการเปลี่ยนแปลง
- สามารถดูตัวอย่างการสร้างห้อง/กำหนดห้อง/กำหนด State Schema ได้จาก เมื่อสร้างโปรเจคแล้วจะสังเกตได้ว่ามันมี Class ชื่อ `MyRoom` มาให้, อันนี้คือ Class ของห้อง
- แล้วก็มันมีการกำหนด State Schema Class ชื่อ `MyRoomState`

![](screenshots/7.png)

- แล้วก็จะสังเกตมันมีการกำหนดห้อง โดยใช้ชื่อว่า `my_room`

![](screenshots/8.png)

- ดังนั้น Server ในตอนนี้มีห้อง แค่แบบเดียวเป็นแบบ `MyRoom` มีชื่อว่า `my_room`, ดังนั้นเวลาต่อห้องชื่อ `my_room`, กิจกรรมต่างๆ ในห้อง จะถูกจัดการโดย `MyRoom`

# การสร้าง Schema (Generate) สำหรับ Client

- ใช้ cmd หรือ terminal เปิดเข้าไปในโฟลเดอร์โดยใช้คำสั่ง `cd colyseus-sample-server`
- ใช้คำสั่ง `npx schema-codegen src/rooms/schema/MyRoomState.ts --csharp --output ..\colyseus-sample-client\PSR\Assets\Scripts\Schema` เพื่อสร้าง Schema ในโฟลเดอร์ `..\colyseus-sample-client\PSR\Assets\Scripts\Schema`
- *จะสร้าง Class Schema เองก็ได้ แต่สั่งให้มัน Generate จะสะดวกกว่า*

# เปิด Server

- ใช้ cmd หรือ terminal เปิดเข้าไปในโฟลเดอร์โดยใช้คำสั่ง `cd colyseus-sample-server`
- ใช้คำสั่ง `npm run start` ในการเปิด Server

![](screenshots/9.png)

# การเชื่อมต่อโดย Client

- การต่อไปที่ Server จะทำได้โดยสร้าง Instance ของ Client ก่อน, โดยใช้โค้ด `ColyseusClient Client = new ColyseusClient("ws://localhost:2567");` เพื่อสร้าง Instance ที่จะต่อไปที่ Server ที่ `localhost:2567`
- สามารถเข้าห้องหรือสร้างห้องใหม่ ถ้าไม่มีห้องว่างเลย, โดย้ใช้โค้ด `ColyseusRoom<MyRoomState> Room = await client.JoinOrCreate<MyRoomState>("my_room")` เพื่อเข้าไปห้องชื่อ `my_room`

# การส่ง Message โดย Client

- หลังจากเข้าห้องแล้วสามารถส่ง Message ไปหา Server โดยใช้โค้ด `Room.Send(type, data)`
- `type` ต้องเป็น `String` หรือ `Byte` เท่านั้น
- `data` สามารถเป็นข้อมูลประเภทไหนก็ได้
- ตัวอย่างเช่น `await Room.Send("simple-chat", "hello!!");`, จะเป็นการส่ง Message ที่มี `type` เป็น `"simple-chat"`, และ `data` เป็น `"hello!!"`

# การรับ Message ที่ส่งมาจาก Client

- ต้องกำหนดการรับ Message ตาม `type` ต่างๆ ก่อน
- สามารถกำหนดการรับ Message ได้ใน Function `onCreate`, โดยใช้ Function `onMessage` ตามตัวอย่างนี้

```
    this.onMessage("simple-chat", (client, message) => {
      console.log("Recv@Server: " + message);
    });
```

- จากตัวอย่างการกำหนดการรับ Message ด้านบน, ถ้า Client ส่ง Message โดยใช้โค้ด `await Room.Send("simple-chat", "hello!!");`, มันจะเขียน Log ที่ Server ว่า `Recv@Server: hello!!`

# การส่ง Message โดย Server

- สามารถส่ง Message ไปหา Client **ที่เฉพาะเจาะจง**ได้โดยใช้ Function `send(client, type, data, options?)`, ตัวอย่างเช่น `this.send(client, "simple-chat", "hello back");` จะเป็นการส่ง Message ที่มี `type` เป็น `"simple-chat"`, และ `data` เป็น `"hello back"` ไปหา Client
- สามารถส่ง Message ไปหา Client **ทั้งหมดในห้อง**โดยใช้ Function `broadcast(type, data, options?)`, ตัวอย่างเช่น `this.broadcast("simple-chat", "hello all");` จะเป็นการส่ง Message ที่มี `type` เป็น `"simple-chat"`, และ `data` เป็น `"hello all"` ไปหา Client ทั้งหมดที่อยู่ในห้อง
- สามารถกำหนด Client ที่ไม่ต้องการส่งได้ใน Function `broadcast(type, data, options?)` ได้ผ่าน Option ชื่อ `except`, ตัวอย่างเช่น `this.broadcast("simple-chat", "hello all", { except: client });`, `this.broadcast("simple-chat", "hello all", { except: [client1, client2, client3] });`
- `type` ต้องเป็น `String` หรือ `Byte` เท่านั้น
- `data` สามารถเป็นข้อมูลประเภทไหนก็ได้

# การรับ Message ที่ส่งมาจาก Server

- ต้องกำหนดการรับ Message ตาม `type` ต่างๆ ก่อน
- สามารถกำหนดการรับ Message ได้ในหลังจากที่เข้าห้องแล้ว, โดยใช้ Function `Room.OnMessage<TypeOfData>(type, data);` ตามตัวอย่างนี้

```
    Room.OnMessage<string>("simple-chat", (message) => {
        Debug.Log("Recv@Client: " + message)
    });
```

- จากตัวอย่างการกำหนดการรับ Message ด้านบน, ถ้า Server ส่ง Message โดยใช้โค้ด `this.send(client, "simple-chat", "hello back");`, มันจะเขียน Log ที่ Client ว่า `Recv@Client: hello back`