using Colyseus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PSR
{
    public class GameInstance : MonoBehaviour
    {
        public FlashMessage SuccessMessage;
        public FlashMessage ErrorMessage;
        public GameObject[] NotJoinedObjects = new GameObject[0];
        public GameObject[] JoinedObjects = new GameObject[0];

        ColyseusClient _client;
        ColyseusRoom<MyRoomState> _room;

        void Start()
        {
            CreateClient();
            UpdateJoinedObjects();
        }

        public void UpdateJoinedObjects()
        {
            foreach (var obj in NotJoinedObjects)
            {
                obj.SetActive(_room == null);
            }
            foreach (var obj in JoinedObjects)
            {
                obj.SetActive(_room != null);
            }
        }

        public void CreateClient()
        {
            _client = new ColyseusClient("ws://localhost:2567");
        }

        private void SetupRoomEvents()
        {
            _room.OnLeave += _room_OnLeave;
        }

        private void ClearRoomEvents()
        {
            _room.OnLeave -= _room_OnLeave;
        }

        private void _room_OnLeave(int code)
        {
            ClearRoomEvents();
            _room = null;
            ErrorMessage.Show($"Leave From The Room: {code}");
            UpdateJoinedObjects();
        }

        public bool ShowAlreadyJoinedMessage()
        {
            if (_room != null)
            {
                ErrorMessage.Show($"Already Joined: {_room.RoomId}");
                return true;
            }
            return false;
        }

        public async void JoinOrCreateRoom()
        {
            if (ShowAlreadyJoinedMessage())
                return;
            try
            {
                _room = await _client.JoinOrCreate<MyRoomState>("my_room");
            }
            catch (System.Exception ex)
            {
                ErrorMessage.Show(ex.Message);
                Debug.LogError($"[GameInstance->JoinOrCreateRoom] Cannot Join: {ex.Message}");
                Debug.LogException(ex);
                return;
            }
            SetupRoomEvents();
            UpdateJoinedObjects();
            SuccessMessage.Show($"Joined: {_room.RoomId}");
            Debug.Log($"[GameInstance] Joined: {_room.RoomId}");
        }

        public async void JoinRoom()
        {
            if (ShowAlreadyJoinedMessage())
                return;
            try
            {
                _room = await _client.Join<MyRoomState>("my_room");
            }
            catch (System.Exception ex)
            {
                ErrorMessage.Show(ex.Message);
                Debug.LogError($"[GameInstance->JoinRoom] Cannot Join: {ex.Message}");
                Debug.LogException(ex);
                return;
            }
            SetupRoomEvents();
            UpdateJoinedObjects();
            SuccessMessage.Show($"Joined: {_room.RoomId}");
            Debug.Log($"[GameInstance] Joined: {_room.RoomId}");
        }

        public async void CreateRoom()
        {
            if (ShowAlreadyJoinedMessage())
                return;
            try
            {
                _room = await _client.Create<MyRoomState>("my_room");
            }
            catch (System.Exception ex)
            {
                ErrorMessage.Show(ex.Message);
                Debug.LogError($"[GameInstance->CreateRoom] Cannot Join: {ex.Message}");
                Debug.LogException(ex);
                return;
            }
            SetupRoomEvents();
            UpdateJoinedObjects();
            SuccessMessage.Show($"Joined: {_room.RoomId}");
            Debug.Log($"[GameInstance] Joined: {_room.RoomId}");
        }

        public async void LeaveRoom()
        {
            if (_room == null)
            {
                ErrorMessage.Show($"Not Joined A Room Yet");
                return;
            }
            try
            {
                await _room.Leave();
            }
            catch (System.Exception ex)
            {
                ErrorMessage.Show(ex.Message);
                Debug.LogError($"[GameInstance->LeaveRoom] Cannot Leave: {ex.Message}");
                Debug.LogException(ex);
                return;
            }
        }
    }
}