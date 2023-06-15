using Colyseus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInstance : MonoBehaviour
{
    public FlashMessage SuccessMessage;
    public FlashMessage ErrorMessage;
    public FlashMessage[] StackableMessages = new FlashMessage[0];
    public GameObject[] NotJoinedObjects = new GameObject[0];
    public GameObject[] JoinedObjects = new GameObject[0];
    public string Title = "TITLE";
    public string SceneName = "SCENE_NAME";

    public ColyseusClient Client { get; private set; }
    public ColyseusRoom<MyRoomState> Room { get; private set; }

    public static GameInstance Instance { get; private set; }

    private int _showingStackableMessageIndex = 0;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        CreateClient();
        UpdateJoinedObjects();
    }

    public void UpdateJoinedObjects()
    {
        foreach (var obj in NotJoinedObjects)
        {
            obj.SetActive(Room == null);
        }
        foreach (var obj in JoinedObjects)
        {
            obj.SetActive(Room != null);
        }
    }

    public void CreateClient()
    {
        Client = new ColyseusClient("ws://localhost:2567");
    }

    private void SetupRoomEvents()
    {
        Room.OnLeave += _room_OnLeave;
        Room.OnMessage<SimpleChat>("simple-chat", OnReceiveSimpleChat);
    }

    private void ClearRoomEvents()
    {
        Room.OnLeave -= _room_OnLeave;
    }

    private void _room_OnLeave(int code)
    {
        ClearRoomEvents();
        Room = null;
        ErrorMessage.Show($"Leave From The Room: {code}");
        UpdateJoinedObjects();
    }

    public bool ShowAlreadyJoinedMessage()
    {
        if (Room != null)
        {
            ErrorMessage.Show($"Already Joined: {Room.RoomId}");
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
            Room = await Client.JoinOrCreate<MyRoomState>("my_room", new Dictionary<string, object>()
            {
                { "title", Title },
                { "sceneName", SceneName }
            });
        }
        catch (System.Exception ex)
        {
            ErrorMessage.Show(ex.Message);
            Debug.LogError($"[GameInstance->JoinOrCreateRoom] Cannot Join: {ex.Message}");
            return;
        }
        SetupRoomEvents();
        UpdateJoinedObjects();
        SuccessMessage.Show($"Joined: {Room.RoomId}");
        Debug.Log($"[GameInstance] Joined: {Room.RoomId}");
    }

    public async void JoinRoom()
    {
        if (ShowAlreadyJoinedMessage())
            return;
        try
        {
            Room = await Client.Join<MyRoomState>("my_room", new Dictionary<string, object>()
            {
                { "title", Title },
                { "sceneName", SceneName }
            });
        }
        catch (System.Exception ex)
        {
            ErrorMessage.Show(ex.Message);
            Debug.LogError($"[GameInstance->JoinRoom] Cannot Join: {ex.Message}");
            return;
        }
        SetupRoomEvents();
        UpdateJoinedObjects();
        SuccessMessage.Show($"Joined: {Room.RoomId}");
        Debug.Log($"[GameInstance] Joined: {Room.RoomId}");
    }

    public async void CreateRoom()
    {
        if (ShowAlreadyJoinedMessage())
            return;
        try
        {
            Room = await Client.Create<MyRoomState>("my_room", new Dictionary<string, object>()
            {
                { "title", Title },
                { "sceneName", SceneName }
            });
        }
        catch (System.Exception ex)
        {
            ErrorMessage.Show(ex.Message);
            Debug.LogError($"[GameInstance->CreateRoom] Cannot Join: {ex.Message}");
            return;
        }
        SetupRoomEvents();
        UpdateJoinedObjects();
        SuccessMessage.Show($"Joined: {Room.RoomId}");
        Debug.Log($"[GameInstance] Joined: {Room.RoomId}");
    }

    public async void LeaveRoom()
    {
        if (Room == null)
        {
            ErrorMessage.Show($"Not Joined A Room Yet");
            return;
        }
        try
        {
            await Room.Leave();
        }
        catch (System.Exception ex)
        {
            ErrorMessage.Show(ex.Message);
            Debug.LogError($"[GameInstance->LeaveRoom] Cannot Leave: {ex.Message}");
            return;
        }
    }

    public async void SendSimpleChat(string message)
    {
        if (Room == null)
        {
            ErrorMessage.Show($"Not Joined A Room Yet");
            return;
        }
        try
        {
            await Room.Send("simple-chat", message);
        }
        catch (System.Exception ex)
        {
            ErrorMessage.Show(ex.Message);
            Debug.LogError($"[GameInstance->SendMessageToServer] Cannot Send: {ex.Message}");
            return;
        }
    }

    private void OnReceiveSimpleChat(SimpleChat msg)
    {
        StackableMessages[_showingStackableMessageIndex++].Show($"{msg.sessionId}: {msg.message}");
        if (_showingStackableMessageIndex >= StackableMessages.Length)
            _showingStackableMessageIndex = 0;
    }
}