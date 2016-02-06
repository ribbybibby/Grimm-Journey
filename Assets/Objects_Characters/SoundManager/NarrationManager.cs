using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

/*
 * This class is used to store and manage narration
 * If any narration is played we should set talking to true
 * This should then be passed to the sound manager via GetTalking
 * This will then be checked in sound managers upate and if set to true will manage the SFX audio (turn down soundFX)
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

	//Used to set a narration-is-playing state
	bool talking;
	bool cooldownActive;
	// Use this for initialization
	void Start ()
	{
		//At the start, no narration
		talking = false;
		cooldownActive = false;

		Asources = gameObject.GetComponents<AudioSource> ();

		/*The below section is now out of date.
		 * We can use the method names to track the array vals now
		 * We made tis part obsolute so that we can use teh array directly
		 * in GetTalkingState()
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
		*/

		//Depending on the level - play the levels intro narration
		if (SceneManager.GetActiveScene().buildIndex == 0) {
			playIntroNar();
		}
		if (SceneManager.GetActiveScene().buildIndex == 1) {
			playForestNar();
		}
		if (SceneManager.GetActiveScene().buildIndex == 3) {
			playCastleNar();
		}
		if (SceneManager.GetActiveScene().buildIndex == 4) {
			playBeanstalkNar();
		}

		InvokeRepeating("getTalkingState", 0.2F, 0.2F);

		//Adding cooldowns
		InvokeRepeating ("runNoJumpCoolDown", 0.2F, 20.0F);
	}

	// Update is called once per frame
	void Update ()
	{

		//BUG ---
		//The below code works for one narration clip, but we can't do this check for multiple clips
		// Because the if clause will always be true for at least one narration clip at some point
		//Need to think of a good way to set talking to false basically
		// I do it un update to continiously check to see if a narration clip is playing
		//Can't think of a better way to do this for now - unless we time each narration clip and set talking
		// to false after x seconds. But that will make adding and removing narration a huge hassle.

		//if (!ForestNarration.isPlaying) {
		//	talking = false;
		//	Debug.Log ("!ForestNarration.isPlaying reached: " + talking);
		//}
	}

	//Used to reset cooldown
	void runNoJumpCoolDown(){
			cooldownActive = false;
	}

	void getTalkingState(){
		//for (int i = 1; i < AudioSources[i]; i++)
		foreach (AudioSource element in Asources)
		{
			if(element.isPlaying){
				talking = true;
				break;
			}else{
				talking = false;
			}
		}
	}

	void playNoAttackNar(){
		//BBWNoAttackNarration.Play ();
		if (talking == false) {
			Asources [0].Play ();
			talking = true;
		}
	}

	public void playNoJumpNar(){
		//BBWNoJumpNarration.Play ();
		if (talking == false && cooldownActive == false)
		{
			Asources [1].Play ();
			talking = true;
			cooldownActive = true;
		}
	}

	void playBeanstalkNar(){
		//BeanstalkNarration.Play ();
		if (talking == false) 
		{
			Asources [2].Play ();
			talking = true;
		}
	}

	void playCastleNar(){
		//CastleNarration.Play ();
		if (talking == false) {
			Asources [3].Play ();
			talking = true;
		}
	}

	void playSwitchNar(){
		//FirstTransformNarration.Play ();
		if (talking == false) {
			Asources [4].Play ();
			talking = true;
		}
	}

	/*
	 * We check to see if talking is false, so that we don't overlap other narration and make things sound crazy
	 * If it's quiet we play the narration clip and set talking to true to show that we are busy playing a narration clip
	 */
	void playForestNar(){
		if (talking == false) {
			//ForestNarration.Play ();
			Asources [5].Play ();
			talking = true;
		}
	}

	/*
	 * We check to see if talking is false, so that we don't overlap other narration and make things sound crazy
	 * If it's quiet we play the narration clip and set talking to true to show that we are busy playing a narration clip
	 */
	void playIntroNar(){
		if (talking == false) {
			//IntroNarration.Play ();
			Asources [6].Play ();
			talking = true;
		}
	}

	void playLittleHealthNar(){
		//LittleHealthNarration.Play ();
		if (talking == false) {
			Asources [7].Play ();
			talking = true;
		}
	}

	void playLRRHFirstHitNar(){
		//LRRHFirstHitNarration.Play ();
		if (talking == false) {
			Asources [8].Play ();
			talking = true;
		}
	}

	public void playNoBothExitNar(){
		//NoBothExitNarration.Play ();
		if (talking == false) {
			Asources [9].Play ();
			talking = true;
		}
	}

	/*
	 * Get method used by Music and Sound manager to get the talking state
	 */
	public bool getTalking(){
		return talking;
	}
}

