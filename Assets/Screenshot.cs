using UnityEngine;
using System.Collections;
using Vuforia;

public class Screenshot : MonoBehaviour {
    public GameObject projectile;
    public GameObject clone;

    void Update() {
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
                }

            }
        }

        if (Input.GetMouseButtonDown(0)) {
            ScreenshotManager.SaveScreenshot("DoggyVision", "DoggyVision");
            Vuforia.Image image = CameraDevice.Instance.GetCameraImage(Vuforia.Image.PIXEL_FORMAT.GRAYSCALE);
              Debug.Log(image);
            if (image != null  && image.IsValid()){
              Debug.Log("heeeeyyyy");
              Texture2D texture2D = new Texture2D(image.Height, image.Width);
              image.CopyToTexture(texture2D);
              ScreenshotManager.SaveImage(texture2D, "DoggyVision");                    
            } 
            Debug.Log("Pressed primary button.");
        }

        if (Input.GetMouseButtonDown(1)) {
            ScreenshotManager.SaveScreenshot("DoggyVision", "DoggyVision");
            Debug.Log("Pressed secondary button.");
        }

        if (Input.GetMouseButtonDown(2)) {
            ScreenshotManager.SaveScreenshot("DoggyVision", "DoggyVision");
            Debug.Log("Pressed middle click.");
        }

    }

}