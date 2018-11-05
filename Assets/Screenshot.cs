using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using Vuforia;

public class Screenshot : MonoBehaviour {
    private string m_URL = "http://127.0.0.1:9000";

    void Update() {
        if (gameObject.GetComponent<CameraToggle>().getMode() == StereoTargetEyeMask.None) { // not in VR mode
            for (int i = 0; i < Input.touchCount; ++i) {
                if (Input.GetTouch(i).phase == TouchPhase.Began){
                    if (Application.platform == RuntimePlatform.Android){
                        Debug.Log("Do something special here");
                    }
                    else if (Application.platform == RuntimePlatform.IPhonePlayer){
                        Debug.Log("Do something special here");
                    }
                    ScreenshotManager.SaveScreenshot("DoggyVision", "DoggyVision"); // works, but saves as stereo screenshot
                    Vuforia.Image image = Vuforia.CameraDevice.Instance.GetCameraImage(Image.PIXEL_FORMAT.RGB888);
                    if (image != null && image.IsValid()){
                        Texture2D texture2D = new Texture2D(image.Height, image.Width);
                        image.CopyToTexture(texture2D);
                        ScreenshotManager.SaveImage(texture2D, "DoggyVision");  

                        // Encode texture into PNG
                        byte[] bytes = texture2D.EncodeToPNG();
                        Object.Destroy(texture2D);
           
                        StartCoroutine(Upload(bytes));
                    }

                }
            }

            if (Input.GetKeyDown(KeyCode.UpArrow)) {
                Vuforia.Image image = Vuforia.CameraDevice.Instance.GetCameraImage(Image.PIXEL_FORMAT.GRAYSCALE);
                if (image != null && image.IsValid()){
                    Texture2D texture2D = new Texture2D(image.Height, image.Width);
                    image.CopyToTexture(texture2D);

                    Debug.Log("starting post request stuff...");
                    // Encode texture into PNG
                    byte[] bytes = texture2D.EncodeToPNG();
                    Object.Destroy(texture2D);

                    StartCoroutine(Upload(bytes));
                } else {
                    Debug.Log("invalid or null image: " + image);
                    // test sending bytes with an invalid image anyway...
                    // byte[] bytes = new byte[10];
                    // StartCoroutine(Upload(bytes));
                }
            }

            /*if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2)) {
                Vuforia.Image image = Vuforia.CameraDevice.Instance.GetCameraImage(Image.PIXEL_FORMAT.RGB888);
                if (image != null && image.IsValid()){
                    Texture2D texture2D = new Texture2D(image.Height, image.Width);
                    image.CopyToTexture(texture2D);
                    ScreenshotManager.SaveImage(texture2D, "DoggyVision");  

                    // Encode texture into PNG
                    byte[] bytes = texture2D.EncodeToPNG();
                    Object.Destroy(texture2D);
                    StartCoroutine(Upload(bytes));
                }
            }*/

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