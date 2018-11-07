using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using Vuforia;
using ColorBlindUtility.UGUI;

public class Screenshot : MonoBehaviour {
    private string m_URL = "http://192.168.240.17:9000";
    //private string m_URL = "http://127.0.0.1:9000";
	private GameObject arcamera;

    void Start() {
		arcamera = GameObject.Find("ARCamera");
    

    }

    void Update() {
        if (gameObject.GetComponent<CameraToggle>().getMode() == StereoTargetEyeMask.None) { // not in AR mode
            for (int i = 0; i < Input.touchCount; ++i) {
                if (Input.GetTouch(i).phase == TouchPhase.Began){
                    if (Application.platform == RuntimePlatform.Android){
                        Debug.Log("Do something special here");
                    }
                    else if (Application.platform == RuntimePlatform.IPhonePlayer){
                        Debug.Log("Do something special here");
                    }

                    ScreenshotManager.SaveScreenshot("DoggyVision", "DoggyVision"); 

                    arcamera.GetComponent<ColorBlindFilter>().colorBlindMode = ColorBlindMode.None;
                    Vuforia.Image imageHuman = Vuforia.CameraDevice.Instance.GetCameraImage(Image.PIXEL_FORMAT.RGB888);

                    arcamera.GetComponent<ColorBlindFilter>().colorBlindMode = ColorBlindMode.Protanopia;;
                    Vuforia.Image imageDoggy = Vuforia.CameraDevice.Instance.GetCameraImage(Image.PIXEL_FORMAT.RGB888);

                    if (imageHuman != null && imageHuman.IsValid()){
                        Texture2D texture2DHuman = new Texture2D(imageHuman.Height, imageHuman.Width);
                        imageHuman.CopyToTexture(texture2DHuman);
                        ScreenshotManager.SaveImage(texture2DHuman, "HumanVision");  

                        // Encode texture into PNG
                        byte[] bytes = texture2DHuman.EncodeToPNG();
                        Object.Destroy(texture2DHuman);
           
                        StartCoroutine(Upload(bytes));
                    }

                    if (imageDoggy != null && imageDoggy.IsValid()){
                        Texture2D texture2DDoggy = new Texture2D(imageDoggy.Height, imageDoggy.Width);
                        imageDoggy.CopyToTexture(texture2DDoggy);
                        ScreenshotManager.SaveImage(texture2DDoggy, "HumanVision");  

                        // Encode texture into PNG
                        byte[] bytes = texture2DDoggy.EncodeToPNG();
                        Object.Destroy(texture2DDoggy);
           
                        StartCoroutine(Upload(bytes));
                    }

                }
            }

        }
    }

    IEnumerator Upload(byte[] bytes) {
        Debug.Log("staring upload coroutine...");
        WWWForm form = new WWWForm();
        form.AddBinaryData("DoggyVision", bytes);
 
        UnityWebRequest www = UnityWebRequest.Post(m_URL, form);
        yield return www.SendWebRequest();
 
        if(www.isNetworkError || www.isHttpError) {
            Debug.Log(www.error);
        }
        else {
            Debug.Log("Form upload complete!");
        }
    }

}