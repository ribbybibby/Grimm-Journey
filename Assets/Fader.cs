using UnityEngine;
using System.Collections;

public class Fader : MonoBehaviour {

	private Color textureColor;

	// Use this for initialization
	void Start () {
		textureColor = renderer.material.color;
		textureColor.a = 0;
		renderer.material.color = textureColor;
	}

}