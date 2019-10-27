using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using Firebase;
//using Firebase.Database;
//using Firebase.Unity.Editor;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class PhoneCamera : MonoBehaviour
{
    private bool camAvailable;
    private WebCamTexture backCam;
    private Texture defaultBackground;

    public RawImage background;
    public AspectRatioFitter fit;

    public static string savepath;
    int _CaptureCounter = 0;

    public RawImage takenImage;
    public Text itemName;
    public ChoiceController category;
    public ToggleButton casual;
    public ToggleButton office;
    public ToggleButton formal;
    public ToggleButton street;
    public ToggleButton cute;
    public ToggleButton event1;
    public SceneController controller;
    public ClosetController closet;
    public ScrollRect formView;

    //public DatabaseReference reference;

    // Start is called before the first frame update
    void Start()
    {


        //FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://fitme-257203.firebaseio.com");
        //reference = FirebaseDatabase.DefaultInstance.RootReference;


        savepath = Application.persistentDataPath + "/";
        defaultBackground = background.texture;
        WebCamDevice[] devices = WebCamTexture.devices;

        if (devices.Length == 0)
        {
            Debug.Log("No camera detected");
            camAvailable = false;
            return;
        }

        for (int i = 0; i < devices.Length; i++)
        {
            if (devices[i].isFrontFacing)
            { //change this to !devices[i]... afterwards
                backCam = new WebCamTexture(devices[i].name, Screen.width, Screen.width);
            }
        }

        if (backCam == null)
        {
            Debug.Log("Unable to find back");
            return;
        }

        backCam.Play();
        background.texture = backCam;

        camAvailable = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!camAvailable) return;

        float ratio = (float)backCam.width / (float)backCam.height;
        fit.aspectRatio = ratio;

        float scaleY = backCam.videoVerticallyMirrored ? -1f : 1f;
        background.rectTransform.localScale = new Vector3(1f, scaleY, 1f);

        int orient = -backCam.videoRotationAngle;
        background.rectTransform.localEulerAngles = new Vector3(0, 0, orient);

    }

    public void CamCapture()
    {
        Texture2D snap = new Texture2D(backCam.width, backCam.height);
        snap.SetPixels(backCam.GetPixels());
        snap.Apply();

        //System.IO.File.WriteAllBytes(savepath + _CaptureCounter.ToString() + ".png", snap.EncodeToPNG());
        ++_CaptureCounter;

        takenImage.texture = snap;
        // don't save until user has put in all features
    }

    public void SubmitForm()
    {
        Debug.Log("hello");
        List<string> styles = new List<string>();
        if (casual.isOn) { styles.Add("casual"); casual.Toggle(); }
        if (office.isOn) { styles.Add("office"); office.Toggle(); }
        if (street.isOn) { styles.Add("street"); street.Toggle(); }
        if (cute.isOn) { styles.Add("cute"); cute.Toggle(); }
        if (event1.isOn) { styles.Add("event"); event1.Toggle(); }
        if (formal.isOn) { styles.Add("formal"); formal.Toggle(); }
        ClosetItem newitem = new ClosetItem(takenImage.texture, itemName.text, category.category, styles);
        controller.items.Add(newitem);
        itemName.text = "";
        takenImage.texture = null;
        formView.verticalNormalizedPosition = 1f;
        closet.Populate();
        //Debug.Log(reference);
        //Debug.Log("hello");
        //reference.Child(itemName.text).Child("image").SetValueAsync(takenImage.texture);
        //reference.Child(itemName.text).Child("category").SetValueAsync(category.category);
        //foreach (string s in styles)
        //{
        //    reference.Child(itemName.text).Child("styles").SetValueAsync(s);
        //}

    }
}
