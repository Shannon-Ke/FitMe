using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideAllBut : MonoBehaviour
{
    public GameObject Casual;
    public GameObject Office;
    public GameObject Cute;
    public GameObject Formal;
    public GameObject Event;
    public GameObject Street;
    
    GameObject[] buttons;

    public GameObject welcomeItems;
    public SceneController control;
    public GameObject home;

    void Start() {
        buttons = new GameObject[6];
        buttons[0] = Casual;
        buttons[1] = Office;
        buttons[2] = Cute;
        buttons[3] = Formal;
        buttons[4] = Event;
        buttons[5] = Street;
    }

    public void Deactivate(string name) {
        foreach (GameObject g in buttons) {
            if (g.name != name) {
                g.SetActive(false);
            }
        }
        welcomeItems.SetActive(false);
        control.Home(name);
    }

    public void Activate()
    {
        foreach (GameObject g in buttons)
        {
            g.SetActive(true);
        }
        welcomeItems.SetActive(true);
        home.SetActive(false);
    }
}
