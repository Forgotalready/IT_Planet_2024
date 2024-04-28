using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutScene : MonoBehaviour
{
    [SerializeField] GameObject panel;
    Image img;
    float a = 0f;

    // Start is called before the first frame update
    void Awake()
    {
        img = panel.GetComponent<Image>();
        img.color = Color.black;
    }

    // Update is called once per frame
    
    void Update()
    {
        img.color = new Color(0, 0, 0, 255 * Mathf.Abs(Mathf.Sin(0.15f * a)));
        a += Time.deltaTime;
    }
}
