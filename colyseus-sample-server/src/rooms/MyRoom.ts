import { Room, Client } from "@colyseus/core";
import { MyRoomState, User } from "./schema/MyRoomState";

export class MyRoom extends Room<MyRoomState> {
  maxClients = 4;

  onCreate (options: any) {
    this.setState(new MyRoomState());

    this.onMessage("simple-chat", (client, message) => {
      const user = this.state.users.get(client.sessionId);
      user.latestMessage = message;
      this.broadcast("simple-chat", {
        sessionId: client.sessionId,
        message: message,
      });
    });

    this.setMetadata({
      title: options.title,
      sceneName: options.sceneName,
    });
  }

  onJoin (client: Client, options: any) {
    console.log(client.sessionId, "joined!");
    this.state.users.set(client.sessionId, new User())
  }

  onLeave (client: Client, consented: boolean) {
    console.log(client.sessionId, "left!");
    this.state.users.delete(client.sessionId)
  }

  onDispose() {
    console.log("room", this.roomId, "disposing...");
  }

}
