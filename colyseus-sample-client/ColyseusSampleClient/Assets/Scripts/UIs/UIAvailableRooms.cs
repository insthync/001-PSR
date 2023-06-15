using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAvailableRooms : MonoBehaviour
{
    public UIAvailableRoom Prefab;
    public Transform Container;

    private float _lastUpdate = 0f;

    void Update()
    {
        if (Time.unscaledTime - _lastUpdate > 3f)
        {
            _lastUpdate = Time.unscaledTime;
            UpdateRooms();
        }
    }

    async void UpdateRooms()
    {
        List<MyRoomAvailable> rooms = new List<MyRoomAvailable>();
        if (GameInstance.Instance.Client != null)
            rooms.AddRange(await GameInstance.Instance.Client.GetAvailableRooms<MyRoomAvailable>("my_room"));
        for (int i = Container.childCount - 1; i >= 0; --i)
        {
            Destroy(Container.GetChild(i).gameObject);
        }
        for (int i = 0; i < rooms.Count; ++i)
        {
            var ui = Instantiate(Prefab, Container);
            ui.SetData(rooms[i]);
        }
    }
}
