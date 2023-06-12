import { Schema, Context, type, MapSchema } from "@colyseus/schema";

export class User extends Schema {
  @type("string") latestMessage: string = "";
}

export class MyRoomState extends Schema {

  @type("string") mySynchronizedProperty: string = "Hello world";
  @type({ map: User }) users = new MapSchema<User>();
}
