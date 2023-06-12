using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISessionId : MonoBehaviour
{
    public TMPro.TextMeshProUGUI TextSessionId;

    private void Update()
    {
        TextSessionId.text = $"Your Session ID:\n{(GameInstance.Instance.Room != null ? GameInstance.Instance.Room.SessionId : "?")}";
    }
}
