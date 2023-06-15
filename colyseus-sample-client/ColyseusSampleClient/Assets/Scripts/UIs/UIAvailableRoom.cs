using UnityEngine;

public class UIAvailableRoom : MonoBehaviour
{
    public TMPro.TextMeshProUGUI TextClients;
    public TMPro.TextMeshProUGUI TextMaxClients;
    public TMPro.TextMeshProUGUI TextRoomId;
    public TMPro.TextMeshProUGUI TextMetadataTitle;
    public TMPro.TextMeshProUGUI TextMetadataSceneName;

    public void SetData(MyRoomAvailable available)
    {
        if (TextClients != null)
            TextClients.text = available.clients.ToString("N0");
        if (TextMaxClients != null)
            TextMaxClients.text = available.maxClients.ToString("N0");
        if (TextRoomId != null)
            TextRoomId.text = available.roomId;
        if (TextMetadataTitle != null)
            TextMetadataTitle.text = available.metadata.title;
        if (TextMetadataSceneName != null)
            TextMetadataSceneName.text = available.metadata.sceneName;
    }
}
