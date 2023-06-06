# Create server project
- Install NodeJS
- `npm create colyseus-app@latest`

![](screenshots/1.png)

# Create client project
- Install Unity
- Create Unity Project

![](screenshots/2.png)

- Unity `.gitignore`
- Unity Menu -> `Window` -> `Package Manager`

![](screenshots/3.png)

- `Package Manager` -> `+` -> `Add package from git URL...`

![](screenshots/4.png)

- `https://github.com/colyseus/colyseus-unity3d.git#upm` -> `Add`

![](screenshots/5.png)

![](screenshots/6.png)

# State and room

![](screenshots/7.png)

![](screenshots/8.png)

# Schema generate

- Create folder `{Unity Project}` -> `Assets`: `Scripts` -> `Schema`
- `cd psr-server`
- `npx schema-codegen src/rooms/schema/MyRoomState.ts --csharp --output ..\psr-client\PSR\Assets\Scripts\Schema`

# Run server

- `cd psr-server`
- `npm run start`

![](screenshots/9.png)

# Client connection & room

- `ColyseusClient client = new ColyseusClient("ws://localhost:2567");`
- `ColyseusRoom<MyRoomState> room = await client.JoinOrCreate<MyRoomState>("my_room")`