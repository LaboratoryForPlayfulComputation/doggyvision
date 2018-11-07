
using UnityEngine;
using UnityEngine.UI;

public class PositionButton : MonoBehaviour {

	void Start() {
        float newX = this.transform.position.x + (Screen.width/2 - GetComponent<RectTransform>().rect.width);
        float newY = this.transform.position.y - (Screen.height/2 - GetComponent<RectTransform>().rect.height);
        this.transform.position = new Vector3(newX, newY, 0.0f);
    }
    
}