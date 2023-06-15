using Colyseus;

[System.Serializable]
public class MyRoomMetadata
{
    public string title;
    public string sceneName;
}

[System.Serializable]
public class MyRoomAvailable : ColyseusRoomAvailable
{
    public MyRoomMetadata metadata;
}