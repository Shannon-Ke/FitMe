using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClosetController : MonoBehaviour
{
    public GameObject closetContainer;
    public GameObject closetPrefab;

    public SceneController control;

    public Sprite shirt;
    public Sprite pants;
    public Sprite shoes;
    public Sprite hat;
    public Sprite dress;
    public Sprite coat;
    public Sprite acc;

    public List<string> filterList;

    // Start is called before the first frame update
    //void Start()
    //{

    //    Vector3 pos = closetContainer.transform.position;
    //    Quaternion rot = closetContainer.transform.rotation;
    //    for (int i = 0 ; i < control.items.Count; i++) {
    //        //populate with dummy data
    //        GameObject curr = Instantiate(closetPrefab, pos, rot);
    //        curr.transform.SetParent(closetContainer.transform);
    //        curr.transform.localScale = new Vector3(1, 1, 1);
    //        Text name = curr.transform.Find("Text").GetComponent<Text>();
    //        name.text = control.items[i].name;
    //        RawImage itemImage = curr.transform.Find("ItemImage").GetComponent<RawImage>();
    //        itemImage.texture = control.items[i].image;
    //        Image type = curr.transform.Find("Image").GetComponent<Image>();
    //        GameObject tags = curr.transform.Find("Tags").gameObject;
    //    }
    //}

    private void Start()
    {
        filterList = new List<string>();
    }
    public void Populate() {
        foreach(Transform child in closetContainer.transform)
        {
            Destroy(child.gameObject);
        }
        Vector3 pos = closetContainer.transform.position;
        Quaternion rot = closetContainer.transform.rotation;
        for (int i = 0 ; i < control.items.Count; i++) {
            //populate with dummy data
            GameObject curr = Instantiate(closetPrefab, pos, rot);
            curr.transform.SetParent(closetContainer.transform);
            curr.transform.localScale = new Vector3(1, 1, 1);
            Text name = curr.transform.Find("Text").GetComponent<Text>();
            name.text = control.items[i].name;
            RawImage itemImage = curr.transform.Find("ItemImage").GetComponent<RawImage>();
            //Texture2D tex = new Texture2D(16,16);
            //tex.LoadRawTextureData(control.items[i].image);
            //tex.Apply();
            itemImage.texture = control.items[i].image;
            Image type = curr.transform.Find("Image").GetComponent<Image>();
            if (control.items[i].category == "shirt") type.sprite = shirt;
            else if (control.items[i].category == "pants") type.sprite = pants;
            else if (control.items[i].category == "dress") type.sprite = dress;
            else if (control.items[i].category == "accessories") type.sprite = acc;
            else if (control.items[i].category == "hat") type.sprite = hat;
            else if (control.items[i].category == "coat") type.sprite = coat;
            else if (control.items[i].category == "shoes") type.sprite = shoes;
            Transform tags = curr.transform.Find("Tags").transform;
            foreach (string s in control.items[i].styles) {
                foreach (Transform child in tags) {
                    if (child.gameObject.name == s + "Tag") {
                        child.gameObject.SetActive(true);
                        break;
                    }
                }
            }
            
        }
    }

}
