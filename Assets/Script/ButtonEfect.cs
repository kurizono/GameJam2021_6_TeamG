using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonEfect : MonoBehaviour
{
    public Text text;
    public static Color oncolor = new Color(1.0f, 0.0f, 0.0f, 1.0f);
    public static Color offcolor = new Color(0.0f, 0.0f, 0.0f, 1.0f);

    public void OnPointerEvent()
    {
        text.color = oncolor;
    }
    public void OffPointerEvent()
    {
        text.color = offcolor;
    }
}
