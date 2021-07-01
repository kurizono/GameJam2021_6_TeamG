using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonEfect : MonoBehaviour
{
    public Text text;

    public void OnPointerEvent()
    {
        text.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
    }
    public void OffPointerEvent()
    {
        text.color = new Color(0.0f, 0.0f, 0.0f, 1.0f);
    }

}
