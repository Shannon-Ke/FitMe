using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleButton : MonoBehaviour
{
    public Sprite off;
    public Sprite on;

    public bool isOn;
    Button but;
    Image image;
    // Start is called before the first frame update
    void Start()
    {
        isOn = false;
        but = GetComponent<Button>();
        but.onClick.AddListener(Toggle);
        image = GetComponent<Image>();
    }

    public void Toggle() {
        if (!isOn) {
            isOn = true;
            image.sprite = on;
        } else {
            isOn = false;
            image.sprite = off;
        }
    }

}
