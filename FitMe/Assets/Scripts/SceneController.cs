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
    public GameObject ClothingPrefab;

    public Texture[] textures;

    public List<ClosetItem> items;
    public ClosetController closet;
    System.Random random;
     //void Awake() {
     //    if (File.Exists(Application.persistentDataPath + "/player.dat"))
     //    {
     //        BinaryFormatter bf = new BinaryFormatter();
     //        FileStream file = File.Open(Application.persistentDataPath + "/player.dat", FileMode.Open);
     //        PlayerData data = (PlayerData)bf.Deserialize(file) as PlayerData;
     //        file.Close();
     //        items = data.items;
     //    } else {
     //        items = new List<ClosetItem>();
     //    }
     //}
    void Start() {
        random = new System.Random();
        homescreen.SetActive(false);
        items = new List<ClosetItem>();
        List<string> temp = new List<string>();
        temp.Add("cute");
        temp.Add("casual");
        items.Add(new ClosetItem(textures[0], "All Might Hat", "hat", temp.ConvertAll(str => new String(str.ToCharArray()))));

        temp.Add("street");
        temp.Add("event");
        items.Add(new ClosetItem(textures[1], "Coach Flower Bag", "accessories", temp.ConvertAll(str => new String(str.ToCharArray()))));

        items.Add(new ClosetItem(textures[2], "Black Flowery Dress", "dress", temp.ConvertAll(str => new String(str.ToCharArray()))));
        temp.Remove("casual");
        items.Add(new ClosetItem(textures[3], "Black Lace up Dress", "dress", temp.ConvertAll(str => new String(str.ToCharArray()))));
        temp.Add("casual");
        temp.Remove("event");
        temp.Remove("cute");
        temp.Add("office");
        items.Add(new ClosetItem(textures[4], "Black Leggings", "pants", temp.ConvertAll(str => new String(str.ToCharArray()))));
        temp.Add("cute");
        temp.Remove("office");
        items.Add(new ClosetItem(textures[5], "Brown Shorts", "pants", temp.ConvertAll(str => new String(str.ToCharArray()))));
        temp.Add("office");
        items.Add(new ClosetItem(textures[6], "Blue China Shirt", "shirt", temp.ConvertAll(str => new String(str.ToCharArray()))));
        temp.Remove("office");
        temp.Add("street");
        items.Add(new ClosetItem(textures[7], "Disney Shirt", "shirt", temp.ConvertAll(str => new String(str.ToCharArray()))));
        temp.Add("office");
        temp.Add("formal");
        items.Add(new ClosetItem(textures[8], "White Flowery Dress", "dress", temp.ConvertAll(str => new String(str.ToCharArray()))));
        temp = new List<string>();
        temp.Add("cute");
        temp.Add("event");
        temp.Add("office");
        temp.Add("formal");
        items.Add(new ClosetItem(textures[9], "Heels", "shoes", temp.ConvertAll(str => new String(str.ToCharArray()))));
        temp.Remove("office");
        temp.Remove("formal");
        temp.Add("street");
        temp.Add("casual");
        items.Add(new ClosetItem(textures[10], "JOJO HAT", "hat", temp.ConvertAll(str => new String(str.ToCharArray()))));
        items.Add(new ClosetItem(textures[11], "Beartrap sandals", "shoes", temp.ConvertAll(str => new String(str.ToCharArray()))));
        closet.Populate();
    }

    public void Home(string search) {
        foreach (Transform child in suggestionContent.transform)
        {
            Destroy(child.gameObject);
        }
        //populate home page with items from search results
        List<ClosetItem> newItems = Filter(search);
        homescreen.SetActive(true);
        Vector3 pos = suggestionContent.transform.position;
        Quaternion rot = suggestionContent.transform.rotation;
        for (int i = 0 ; i < newItems.Count / 4; i++) {
            //populate with dummy data
            GameObject curr = Instantiate(suggestionPrefab, pos, rot);
            curr.transform.SetParent(suggestionContent.transform);
            curr.transform.localScale = new Vector3(1, 1, 1);
            Text number = curr.transform.Find("Number").GetComponent<Text>();
            number.text = (i + 1).ToString();
            GameObject container = curr.transform.Find("Items").transform.Find("Viewport").transform.Find("Content").gameObject;
            Vector3 pos1 = container.transform.position;
            Quaternion rot1 = container.transform.rotation;
            List<int> used = new List<int>();
            List<string> usedtags = new List<string>();
            for (int j = 0; j < 6; j++) {

                GameObject curr1 = Instantiate(miniClothingPrefab, pos, rot);
                curr1.transform.SetParent(container.transform);
                curr1.transform.localScale = new Vector3(1, 1, 1);
                RawImage image = curr1.transform.Find("ClothingItem").transform.Find("Image").GetComponent<RawImage>();

                int index = random.Next(newItems.Count);
                while (used.Contains(index))
                {
                    index = random.Next(newItems.Count);
                    if (used.Count == newItems.Count)
                    {
                        break;
                    }

                }
                if (used.Count == newItems.Count)
                {
                    break;
                }
                used.Add(index);



                image.texture = newItems[index].image;



            }
            Button button = curr.transform.GetComponent<Button>();
            button.onClick.AddListener(delegate { OpenPopup(used, newItems);});
        }
    }


    void OpenPopup(List<int> used, List<ClosetItem> newItems) {
        foreach (Transform child in popupContent.transform)
        {
            Destroy(child.gameObject);
        }
        Vector3 pos = popupContent.transform.position;
        Quaternion rot = popupContent.transform.rotation;
        List<string> usedtags = new List<string>();
        for (int j = 0; j < 6; j++){
            if (!usedtags.Contains(newItems[used[j]].category))
            {
                GameObject curr1 = Instantiate(ClothingPrefab, pos, rot);
                curr1.transform.SetParent(popupContent.transform);
                curr1.transform.localScale = new Vector3(1, 1, 1);
                RawImage image = curr1.transform.Find("CircleMask").transform.Find("Image").GetComponent<RawImage>();


                image.texture = newItems[used[j]].image;
                Text text = curr1.transform.Find("Text").GetComponent<Text>();
                text.text = newItems[used[j]].name;

                usedtags.Add(newItems[used[j]].category);
            }
                


        }
            popup.SetActive(true);

    }

    public List<ClosetItem> Filter(string filter)
    {
        List<ClosetItem> filtered = new List<ClosetItem>();
        foreach (ClosetItem c in items)
        {
            foreach (string s in c.styles)
            {
                if (filter == s)
                {
                    filtered.Add(c);
                    break;
                }
            }
        }
        return filtered;
    }
     //public void OnApplicationQuit() {
     //    FileStream file;
     //    if (!File.Exists(Application.persistentDataPath + "/player.dat"))
     //    {
     //        file = File.Create(Application.persistentDataPath + "/player.dat");
     //    }
     //    else 
     //    {
     //        file = File.Open(Application.persistentDataPath + "/player.dat", FileMode.Open);
     //    }

     //    BinaryFormatter bf = new BinaryFormatter();
     //    PlayerData data = new PlayerData();
     //    data.items = items;
     //    bf.Serialize(file, data);
     //    file.Close();
     //}


}

[Serializable]
public class ClosetItem {
    [SerializeField]
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

//[Serializable]
//class PlayerData
//{
//    public List<ClosetItem> items;
//}