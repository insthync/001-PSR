using Colyseus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PSR
{
    public class GameInstance : MonoBehaviour
    {
        ColyseusClient client;
        ColyseusRoom<MyRoomState> room;

        // Start is called before the first frame update
        void Start()
        {
            client = new ColyseusClient("ws://localhost:2567");
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                JoinOrCreate();
            }
        }

        async void JoinOrCreate()
        {
            room = await client.JoinOrCreate<MyRoomState>("my_room");
            Debug.Log("[GameInstance] Joined: " + room.RoomId);
        }
    }
}