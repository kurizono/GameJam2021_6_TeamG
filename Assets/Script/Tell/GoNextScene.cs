using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GoNextScene : MonoBehaviour
{
    [SerializeField]
    Image brack;

    float time = 0;
    // Start is called before the first frame update
    void Start()
    {
        time = 0;
    }

    private void Update()
    {
        time += Time.deltaTime;
        if (time < 3)
        {
            brack.color = new Color(0, 0, 0, 1 - (time / 3));
        }
        else
        {
            GoScene();
        }
    }

    void GoScene()
    {
        SceneManager.LoadScene("Game01");
    }
}
