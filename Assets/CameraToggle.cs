using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class CameraToggle : MonoBehaviour {

	private GameObject arcamera;
	private Camera camera;
	public Button button;
	public StereoTargetEyeMask mode;
	private MonoBehaviour vuforiaBehaviour;

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
			toggleARCamera();
        }
    }

    void toggleARCamera() {
    	if (mode == StereoTargetEyeMask.Both) { // switching to normal mode
            	mode = StereoTargetEyeMask.None;
				MixedRealityController.Instance.SetMode(MixedRealityController.Mode.HANDHELD_AR);
        } else { // switching to AR mode
            	mode = StereoTargetEyeMask.Both;
				MixedRealityController.Instance.SetMode(MixedRealityController.Mode.VIEWER_AR);
        }
    }

    public StereoTargetEyeMask getMode(){
        return this.mode;
    }

}