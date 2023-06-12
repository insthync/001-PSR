using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMessageSending : MonoBehaviour
{
    public TMPro.TMP_InputField InputFieldMessage;

    public void OnClickSend()
    {
        GameInstance.Instance.SendSimpleChat(InputFieldMessage.text);
        InputFieldMessage.text = string.Empty;
    }
}
