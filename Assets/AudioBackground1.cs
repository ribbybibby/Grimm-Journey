using UnityEngine;
using System.Collections;

public class AudioBackground1 : MonoBehaviour {
	private SoundManager play;
	private bool played;
	// Use this for initialization

	void Start () {
		played = false;
	}
	void Update () {

	
		if (gameObject.name == "CreepyWoods" & played == false)
		{
			play = GameObject.Find("SoundManager").gameObject.GetComponent<SoundManager>();
			Debug.Log (play.name);
			play.PlayCreepyWoods();
			played = true;
		}
	}
}
