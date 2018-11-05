using UnityEngine;
using UnityEngine.UI;

public class CameraToggle : MonoBehaviour {

	private GameObject arcamera;
	private Camera camera;
	public Button button;
	public StereoTargetEyeMask mode;

	void Start() {
		arcamera = GameObject.Find("ARCamera");
		button = GameObject.Find("Button").GetComponent<Button>();
		button.onClick.AddListener(toggleARCamera);
		mode = StereoTargetEyeMask.Both;
		if (arcamera != null) {
			camera = arcamera.GetComponent("Camera") as Camera;
			Debug.Log(camera);
		}
	}

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Debug.Log("Space key was pressed.");
            if (mode == StereoTargetEyeMask.Both) {
            	mode = StereoTargetEyeMask.None;
            	camera.stereoTargetEye = mode;
                camera.fieldOfView = 30;
            } else {
            	mode = StereoTargetEyeMask.Both;
            	camera.stereoTargetEye = mode;
            }
        }
    }

    void toggleARCamera() {
    	if (mode == StereoTargetEyeMask.Both) {
            	mode = StereoTargetEyeMask.None;
            	camera.stereoTargetEye = mode;
                camera.fieldOfView = 30;
        } else {
            	mode = StereoTargetEyeMask.Both;
            	camera.stereoTargetEye = mode;
        }
    }

    public StereoTargetEyeMask getMode(){
        return this.mode;
    }

}