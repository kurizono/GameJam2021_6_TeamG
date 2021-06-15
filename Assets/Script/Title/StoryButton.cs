using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryButton : MonoBehaviour
{
    TitleController Controllercs;

    [SerializeField]
    Button Button_back;

    void Awake()
    {
        Controllercs = GetComponent<TitleController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Button_back.onClick.AddListener(BackClick);
    }

    void BackClick()
    {
        Controllercs.HomeSetting();
    }
}
