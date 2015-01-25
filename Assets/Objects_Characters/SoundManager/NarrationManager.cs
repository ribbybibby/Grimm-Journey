using UnityEngine;
using System.Collections;

/*
 * This class is used to store and manage narration
 * If any narration is played we should set talking to true
 * This should then be passed to the sound manager via GetTalking
 * This will then be checked in sound managers upate and if set to true will manage the SFX audio
 */
public class NarrationManager : MonoBehaviour {

	AudioSource BBWNoAttackNarration;
	AudioSource BBWNoJumpNarration;
	AudioSource BeanstalkNarration;
	AudioSource CastleNarration;
	AudioSource FirstTransformNarration;
	AudioSource ForestNarration;
	AudioSource IntroNarration;
	AudioSource LittleHealthNarration;
	AudioSource LRRHFirstHitNarration;
	AudioSource NoBothExitNarration;
	
	//Array to store sources
	public AudioSource[] Asources;
	
	bool talking;
	
	// Use this for initialization
	void Start ()
	{

		talking = false;
		
		Asources = gameObject.GetComponents<AudioSource> ();
		
		BBWNoAttackNarration = Asources [0];
		BBWNoJumpNarration = Asources [1];
		BeanstalkNarration = Asources [2];
		CastleNarration = Asources [3];
		FirstTransformNarration = Asources [4];
		ForestNarration = Asources [5];
		IntroNarration = Asources [6];
		LittleHealthNarration = Asources [7];
		LRRHFirstHitNarration = Asources [8];
		NoBothExitNarration = Asources [9];
	}
	
	// Update is called once per frame
	void Update ()
	{

	}

	void playNoAttackNar(){
		BBWNoAttackNarration.Play ();
	}

	void playNoJumpNar(){
		BBWNoJumpNarration.Play ();
	}

	void playBeanstalkNar(){
		BeanstalkNarration.Play ();
	}

	void playCastleNar(){
		CastleNarration.Play ();
	}

	void playSwitchNar(){
		FirstTransformNarration.Play ();
	}

	void playForestNar(){
		ForestNarration.Play ();
	}

	void playIntroNar(){
		IntroNarration.Play ();
	}

	void playLittleHealthNar(){
		LittleHealthNarration.Play ();
	}

	void playLRRHFirstHitNar(){
		LRRHFirstHitNarration.Play ();
	}

	void playNoBothExitNar(){
		NoBothExitNarration.Play ();
	}

	public bool getTalking(){
		return talking;
	}
}

