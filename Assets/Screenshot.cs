using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using Vuforia;
using ColorBlindUtility.UGUI;

public class Screenshot : MonoBehaviour {
    //private string m_URL = "http://192.168.240.17:9000";
    private string m_URL = "http://127.0.0.1:9000";
	private GameObject arcamera;

    private Image.PIXEL_FORMAT mPixelFormat = Image.PIXEL_FORMAT.RGB888; // GRAYSCALE in editor, RGB888 for Mobile
    private bool mAccessCameraImage = true;
    private bool mFormatRegistered = false;

    void Start() {
		arcamera = GameObject.Find("ARCamera");
        // Register Vuforia life-cycle callbacks:
        VuforiaARController.Instance.RegisterVuforiaStartedCallback(OnVuforiaStarted);
        //VuforiaARController.Instance.RegisterTrackablesUpdatedCallback(OnTrackablesUpdated);
        VuforiaARController.Instance.RegisterOnPauseCallback(OnPause);
    }

    void Update() {
        if (gameObject.GetComponent<CameraToggle>().getMode() == StereoTargetEyeMask.None) { // not in AR mode
            for (int i = 0; i < Input.touchCount; ++i) {
                if (Input.GetTouch(i).phase == TouchPhase.Began) {
                    if (Application.platform == RuntimePlatform.Android) {
                        Debug.Log("Touch event detected on Android");
                    }
                    else if (Application.platform == RuntimePlatform.IPhonePlayer) {
                        Debug.Log("Touch event detected on iOS");
                    }

                    takeScreenshots();                   
                }
            }

            if (Input.GetKeyDown(KeyCode.S)){
               takeScreenshots();
            }
        }


    }

    void takeScreenshots() {
        /* Grab texture and send to server */
        //arcamera.GetComponent<ColorBlindFilter>().colorBlindMode = ColorBlindMode.None;
        Vuforia.Image cameraImage = CameraDevice.Instance.GetCameraImage(mPixelFormat);

        if (cameraImage == null) {
            Debug.Log("pixel format is not available yet");
        } else {
            Texture2D texture2DHuman = new Texture2D(cameraImage.Height, cameraImage.Width);
            cameraImage.CopyToTexture(texture2DHuman);
            ScreenshotManager.SaveImage(flipTexture(texture2DHuman), "HumanVision");  
            //StartCoroutine(savePhoto(texture2DHuman, "HumanVision"));          

            Texture2D texture2DDoggy = new Texture2D(cameraImage.Height, cameraImage.Width);
            cameraImage.CopyToTexture(texture2DDoggy);
            ScreenshotManager.SaveImage(addColorBlindnessToTexture(flipTexture(texture2DDoggy)), "DoggyVision");            
            //StartCoroutine(savePhoto(texture2DDoggy, "DoggyVision"));

            // Upload bytes of image to server
            //StartCoroutine(Upload(bytes));
        }
    }

    Texture2D flipTexture(Texture2D original){
        int xN = original.width;
        int yN = original.height;        
        Texture2D flipped = new Texture2D(xN,yN);
        
        for(int i=0;i<xN;i++) {
            for(int j=0;j<yN;j++) {
                flipped.SetPixel(xN-i-1, j, original.GetPixel(i,j));
            }
        }
        flipped.Apply();
        
        return flipped;
    }

    Texture2D rotateTexture(Texture2D originalTexture, bool clockwise) {
        Color32[] original = originalTexture.GetPixels32();
        Color32[] rotated = new Color32[original.Length];
        int w = originalTexture.width;
        int h = originalTexture.height;

        int iRotated, iOriginal;

        for (int j = 0; j < h; ++j) {
            for (int i = 0; i < w; ++i) {
                iRotated = (i + 1) * h - j - 1;
                iOriginal = clockwise ? original.Length - 1 - (j * w + i) : j * w + i;
                rotated[iRotated] = original[iOriginal];
            }
        }
 
        Texture2D rotatedTexture = new Texture2D(h, w);
        rotatedTexture.SetPixels32(rotated);
        rotatedTexture.Apply();
        return rotatedTexture;
    }

    Texture2D addColorBlindnessToTexture(Texture2D originalTexture) {
        Color32[] original = originalTexture.GetPixels32();
        Color32[] blinded = new Color32[original.Length]; 

        for (int i = 0; i < blinded.Length; i++) {
            Color32 pixel = original[i];
            byte average = System.Convert.ToByte((System.Convert.ToInt32(pixel.r) + System.Convert.ToInt32(pixel.g)) / 2);
            blinded[i] = new Color32(average, average, pixel.b, pixel.a);
        }
 
        Texture2D blindedTexture = new Texture2D(originalTexture.width, originalTexture.height);
        blindedTexture.SetPixels32(blinded);
        blindedTexture.Apply();
        return blindedTexture;         
    }

/*  IEnumerator savePhoto(Texture2D texture, string filename) {
        Debug.Log("staring save photo coroutine...");
        yield return ScreenshotManager.SaveImage(texture, filename);
    }
    */

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

    /* */
    void OnVuforiaStarted() {
        // Try register camera image format
        if (CameraDevice.Instance.SetFrameFormat(mPixelFormat, true)) {
            Debug.Log("Successfully registered pixel format " + mPixelFormat.ToString());
            mFormatRegistered = true;
        }
        else {
            Debug.LogError(
                "\nFailed to register pixel format: " + mPixelFormat.ToString() +
                "\nThe format may be unsupported by your device." +
                "\nConsider using a different pixel format.\n");
            mFormatRegistered = false;
        }
    }

    /// <summary>
    /// Called each time the Vuforia state is updated
    /// </summary>
    void OnTrackablesUpdated() {
        if (mFormatRegistered) {
            if (mAccessCameraImage) {
                Vuforia.Image image = CameraDevice.Instance.GetCameraImage(mPixelFormat);

                if (image != null) {
                    byte[] pixels = image.Pixels;
                }
            }
        }
    }

    /// <summary>
    /// Called when app is paused / resumed
    /// </summary>
    void OnPause(bool paused) {
        if (paused) {
            Debug.Log("App was paused");
            UnregisterFormat();
        }
        else {
            Debug.Log("App was resumed");
            RegisterFormat();
        }
    }

    /// <summary>
    /// Register the camera pixel format
    /// </summary>
    void RegisterFormat() {
        if (CameraDevice.Instance.SetFrameFormat(mPixelFormat, true)) {
            Debug.Log("Successfully registered camera pixel format " + mPixelFormat.ToString());
            mFormatRegistered = true;
        }
        else {
            Debug.LogError("Failed to register camera pixel format " + mPixelFormat.ToString());
            mFormatRegistered = false;
        }
    }

    /// <summary>
    /// Unregister the camera pixel format (e.g. call this when app is paused)
    /// </summary>
    void UnregisterFormat() {
        Debug.Log("Unregistering camera pixel format " + mPixelFormat.ToString());
        CameraDevice.Instance.SetFrameFormat(mPixelFormat, false);
        mFormatRegistered = false;
    }


}