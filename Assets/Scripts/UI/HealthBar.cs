using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

	public Image fillArea;
	public Slider slider;

	private Color fullHealthColour = Color.green;
	private Color mediumHealthColor = Color.yellow;
	private Color noHealthColour = Color.red;

	float mediumHealthAmount = 0.6f;
	float dangerHealthAmount = 0.2f;

	// Use this for initialization
	void Start () {
		slider.value = GetHealth();
	}
	
	// Update is called once per frame
	void Update () {
		slider.value = GetHealth ();

		if (slider.value < dangerHealthAmount) {
			fillArea.color = noHealthColour;
			Debug.Log ("I have a health amount lower than danger health amount");
		} else if (slider.value < mediumHealthAmount) {
			fillArea.color = mediumHealthColor;
		} else {
			fillArea.color = fullHealthColour;
		}
		fillArea.GraphicUpdateComplete ();

	}

	float GetHealth () {
		return 1f;
	}
}
