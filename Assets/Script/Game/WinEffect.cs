using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinEffect : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer Win, Defeat;

    private void Start()
    {
        Win.color = new Color(1, 1, 1, 0);
        Defeat.color = new Color(1, 1, 1, 0);
    }


    public IEnumerator PlayerWin()
    {
        yield return StartCoroutine(Effect(Win));
    }
    public IEnumerator PlayerDefeat()
    {
        yield return StartCoroutine(Effect(Defeat));
    }

    private IEnumerator Effect(SpriteRenderer winchara)
    {
        TimeContoller timecs;
        timecs = GetComponent<TimeContoller>();
        for(float i = 0; i < 100; i++)
        {
            winchara.color = new Color(1, 1, 1, (i / 100f));
            yield return new WaitForSeconds(0.002f);
        }
        yield return new WaitForSeconds(0.3f);
    }
}