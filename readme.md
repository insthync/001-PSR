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

- การต่อไปที่ Server จะทำได้โดยสร้าง Instance ของ Client ก่อน, โดยใช้โค้ด `ColyseusClient client = new ColyseusClient("ws://localhost:2567");` เพื่อสร้าง Instance ที่จะต่อไปที่ Server ที่ `localhost:2567`
- สามารถเข้าห้องหรือสร้างห้องใหม่ ถ้าไม่มีห้องว่างเลย, โดย้ใช้โค้ด `ColyseusRoom<MyRoomState> room = await client.JoinOrCreate<MyRoomState>("my_room")` เพื่อเข้าไปห้องชื่อ `my_room`