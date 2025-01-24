using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] Hearts = new GameObject[4];
    public int health = 5;
    public Sprite goodHeart;
    public Sprite badHeart;

    // Update is called once per frame
    void Update()
    {
        for (int i = 5; i > health; i--)
        {
            Hearts[i-1].GetComponent<UnityEngine.UI.Image>().sprite = badHeart;
        }

        for (int i = 0; i < health; i++)
        {
            Hearts[i].GetComponent<UnityEngine.UI.Image>().sprite = goodHeart;
        }

    }
}
