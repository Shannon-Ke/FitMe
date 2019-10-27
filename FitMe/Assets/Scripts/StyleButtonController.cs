using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StyleButtonController : MonoBehaviour
{
    Animator anim;
    public HideAllBut hideScript;
    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
        anim.enabled = false;
    }

    public void Clicked() {
        anim.enabled = true;
        hideScript.Deactivate(gameObject.name);
    }
}
