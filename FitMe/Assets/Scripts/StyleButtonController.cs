using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StyleButtonController : MonoBehaviour
{
    Animator anim;
    public HideAllBut hideScript;
    public Transform orig;
    Vector3 origc;
    bool selected;
    // Start is called before the first frame update
    void Awake()
    {
        origc = orig.transform.position;
        selected = false;
        anim = GetComponent<Animator>();
        anim.enabled = false;
    }

    public void Clicked() {
        if (!selected)
        {
            anim.enabled = true;
            hideScript.Deactivate(gameObject.name);
            selected = true;
        } else
        {
            anim.enabled = false;
            hideScript.Activate();
            selected = false;
            //reset position
            transform.position = origc;
        }

    }
}
