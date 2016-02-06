using UnityEngine;
using System.Collections;

public class Fader : MonoBehaviour {

	private Color textureColor;

	// Use this for initialization
	void Start () {
		textureColor = GetComponent<Renderer>().material.color;
		textureColor.a = 0;
		GetComponent<Renderer>().material.color = textureColor;
	}

}