using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIUsers : MonoBehaviour
{
    public TMPro.TextMeshProUGUI TextUsers;

    private void Update()
    {
        TextUsers.text = string.Empty;
        if (GameInstance.Instance.Room == null)
            return;
        GameInstance.Instance.Room.State.users.ForEach((key, user) =>
        {
            TextUsers.text += $"{key}: {(string.IsNullOrEmpty(user.latestMessage) ? "(no message)" : user.latestMessage)}\n";
        });
    }
}
