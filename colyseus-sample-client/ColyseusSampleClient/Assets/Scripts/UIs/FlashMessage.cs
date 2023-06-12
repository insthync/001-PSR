using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashMessage : MonoBehaviour
{
    public float ShowDuration = 3f;
    public TMPro.TextMeshProUGUI TextComp;

    public void Show(string message)
    {
        TextComp.text = message;
        gameObject.SetActive(true);
        CancelInvoke(nameof(Hide));
        Invoke(nameof(Hide), ShowDuration);
        transform.SetAsLastSibling();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
