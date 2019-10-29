using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClosetFilter : MonoBehaviour
{
    ToggleButton toggle;
    public GameObject content;
    public string filterQ;
    public ClosetController control;
    // Start is called before the first frame update
    void Start()
    {
        toggle = GetComponent<ToggleButton>();
    }

    public void ClickController()
    {
        if (toggle.isOn)
        {
            Unfilter();
        }
        else
        {
            FilterCloset();
        }
    }

    public void FilterCloset()
    {
        control.filterList.Add(filterQ);
        foreach (Transform child in content.transform)
        {
            Image type = child.Find("Image").GetComponent<Image>();
            if (control.filterList.Contains(type.sprite.name))
            {
                child.gameObject.SetActive(true);
            } else
            {
                child.gameObject.SetActive(false);
            }
        }
    }
    public void Unfilter()
    {
        control.filterList.Remove(filterQ);
        if (control.filterList.Count == 0)
        {
            foreach (Transform child in content.transform)
            {


                    child.gameObject.SetActive(true);

                
            }
        } else
        {
            foreach (Transform child in content.transform)
            {
                Image type = child.Find("Image").GetComponent<Image>();
                if (!control.filterList.Contains(type.sprite.name)) {
                    child.gameObject.SetActive(false);
                }
                    

                
            }
        }
    }
}
