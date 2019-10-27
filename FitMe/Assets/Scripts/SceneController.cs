using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using UnityEngine.UI;

[Serializable]
public class SceneController : MonoBehaviour
{
    public static SceneController control;
    public GameObject homescreen;
    public GameObject popup;

    public GameObject suggestionContent;
    public GameObject popupContent;

    public GameObject suggestionPrefab;
    public GameObject miniClothingPrefab;

    public List<ClosetItem> items;

    // void Awake() {
    //     if (File.Exists(Application.persistentDataPath + "/player.dat"))
    //     {
    //         BinaryFormatter bf = new BinaryFormatter();
    //         FileStream file = File.Open(Application.persistentDataPath + "/player.dat", FileMode.Open);
    //         PlayerData data = (PlayerData)bf.Deserialize(file) as PlayerData;
    //         file.Close();
    //         items = data.items;
    //     } else {
    //         items = new List<ClosetItem>();
    //     }
    // }
    void Start() {
        homescreen.SetActive(false);
        items = new List<ClosetItem>();
    }

    public void Home(string search) {
        //populate home page with items from search results
        homescreen.SetActive(true);
        Vector3 pos = suggestionContent.transform.position;
        Quaternion rot = suggestionContent.transform.rotation;
        for (int i = 0 ; i < 5; i++) {
            //populate with dummy data
            GameObject curr = Instantiate(suggestionPrefab, pos, rot);
            curr.transform.SetParent(suggestionContent.transform);
            curr.transform.localScale = new Vector3(1, 1, 1);
            Text number = curr.transform.Find("Number").GetComponent<Text>();
            number.text = (i + 1).ToString();
            GameObject container = curr.transform.Find("Items").transform.Find("Viewport").transform.Find("Content").gameObject;
            Vector3 pos1 = container.transform.position;
            Quaternion rot1 = container.transform.rotation;
            for (int j = 0; j < 5; j++) {
                GameObject curr1 = Instantiate(miniClothingPrefab, pos, rot);
                curr1.transform.SetParent(container.transform);
                curr1.transform.localScale = new Vector3(1, 1, 1);
                Image image = curr1.transform.Find("ClothingItem").transform.Find("Image").GetComponent<Image>();
                image.sprite = (Sprite)Resources.Load<Sprite>("hatEx") as Sprite;
            }
            Button button = curr.transform.GetComponent<Button>();
            button.onClick.AddListener(delegate { OpenPopup();});
        }
    }

    void OpenPopup() {
        popup.SetActive(true);
    }


    // public void OnApplicationQuit() {
    //     FileStream file;
    //     if (!File.Exists(Application.persistentDataPath + "/player.dat"))
    //     {
    //         file = File.Create(Application.persistentDataPath + "/player.dat");
    //     }
    //     else 
    //     {
    //         file = File.Open(Application.persistentDataPath + "/player.dat", FileMode.Open);
    //     }

    //     BinaryFormatter bf = new BinaryFormatter();
    //     PlayerData data = new PlayerData();
    //     data.items = items;
    //     bf.Serialize(file, data);
    //     file.Close();
    // }


}

[Serializable]
public class ClosetItem {
    public Texture image;
    public string name;
    public string category;
    public List<string> styles;

    public ClosetItem (Texture i, string n, string c, List<string> s) {
        image = i;
        name = n;
        category = c;
        styles = s;
    }
}

[Serializable]
class PlayerData
{
    public List<ClosetItem> items;
}