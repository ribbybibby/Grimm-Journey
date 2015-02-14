using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
		AudioSource BBWHit;
		AudioSource BBWKilled;
		AudioSource Bite;
		AudioSource BroomstickWhoosh;
		AudioSource Claw;
		AudioSource ColdCastle;
		AudioSource CollapsingPlatform;
		AudioSource CreepyWoods;
		AudioSource CWHurt;
		AudioSource CWKilled;
		AudioSource Howl;
		AudioSource HuffAndPuff;
		AudioSource JumpBBW;
		AudioSource JumpLRRH;
		AudioSource LRRHHit;
		AudioSource LRRHKilled;
		AudioSource PlayerSpawn;
		AudioSource ProjectileCat;
		AudioSource ReachedExit;
		AudioSource THit;
		AudioSource TKilled;
		AudioSource ToweringBeanstalk;
		AudioSource TransformingLight;
		AudioSource TrollHeadCrush;
		AudioSource WCHit;
		AudioSource WCKilled;
		AudioSource WoodCutterAxe;
		AudioSource Bwap;
		//Testing narration
		//AudioSource IntroNarration;
		//music
		//AudioSource musicLoop;
		//Array to store sources
		public AudioSource[] Asources;

		public NarrationManager localNarrationManager;

		//Used to store a volume to be used to quiet down the audio during narration
		public float setVolume;

		// Use this for initialization
		void Start ()
		{

			setVolume = 1.0F;
			//talking = false;

			Asources = gameObject.GetComponents<AudioSource> ();
			
			localNarrationManager = GameObject.Find ("NarrationManager").GetComponent<NarrationManager> ();
		
			BBWHit = Asources [0];
			BBWKilled = Asources [1];
			Bite = Asources [2];
			BroomstickWhoosh = Asources [3];
			Claw = Asources [4];
			ColdCastle = Asources [5];
			CollapsingPlatform = Asources [6];
			CreepyWoods = Asources [7];
			CWHurt = Asources [8];
			CWKilled = Asources [9];
			Howl = Asources [10];
			HuffAndPuff = Asources [11];
			JumpBBW = Asources [12];
			JumpLRRH = Asources [13];
			LRRHHit = Asources [14];
			LRRHKilled = Asources [15];
			PlayerSpawn = Asources [16];
			ProjectileCat = Asources [17];
			ReachedExit = Asources [18];
			THit = Asources [19];
			TKilled = Asources [20];
			ToweringBeanstalk = Asources [21];
			TransformingLight = Asources [22];
			TrollHeadCrush = Asources [23];
			WCHit = Asources [24];
			WCKilled = Asources [25];
			WoodCutterAxe = Asources [26];
			Bwap = Asources [27];
		}
	
		// Update is called once per frame
		void Update ()
		{
			/*Manage the volume of the levels audio
			 *  This does not include the narration volume
			 *  As we want that to drown out the other audio until its finished
			 * */
			BBWHit.volume = setVolume;
			BBWKilled.volume = setVolume;
			Bite.volume = setVolume;
			BroomstickWhoosh.volume = setVolume;
			Claw.volume = setVolume;
			ColdCastle.volume = setVolume;
			CollapsingPlatform.volume = setVolume;
			CreepyWoods.volume = setVolume;
			CWHurt.volume = setVolume;
			CWKilled.volume = setVolume;
			Howl.volume = setVolume;
			HuffAndPuff.volume = setVolume;
			JumpBBW.volume = setVolume;
			JumpLRRH.volume = setVolume;
			LRRHHit.volume = setVolume;
			LRRHKilled.volume = setVolume;
			PlayerSpawn.volume = setVolume;
			ProjectileCat.volume = setVolume;
			ReachedExit.volume = setVolume;
			THit.volume = setVolume;
			TKilled.volume = setVolume;
			ToweringBeanstalk.volume = setVolume;
			TransformingLight.volume = setVolume;
			TrollHeadCrush.volume = setVolume;
			WCHit.volume = setVolume;
			WCKilled.volume = setVolume;
			WoodCutterAxe.volume = setVolume;
			Bwap.volume = setVolume;

			Debug.Log (localNarrationManager.getTalking ());

			//Change this to set a bool whenever any narration is playing inside the narrationmanager
			if (localNarrationManager.getTalking() == true) {
				setVolume = 0.2F;
				Debug.Log ("gettalking is " + setVolume);
			}

			if (localNarrationManager.getTalking() == false) {
				setVolume = 1.0F;
				Debug.Log ("gettalking is " + setVolume);
			}
		}
		
		//Once narration is over, reset the volume to max
		//.volume is normalized so  between 0.0F and 1.0F
		//private void checkForNaration(){
			//Intro to level narration
			//if (!IntroNarration.isPlaying) {
				//talking = false;	
				//setVolume = 1.0F;
				//Debug.Log (setVolume);
			//}
		//}

		//public void PlayIntroNarration ()
		//{
		//
		//	setVolume = 0.2F;
		//	IntroNarration.Play ();
		//
		//}

		public void PlayBwap ()
		{
			Bwap.Play ();
		}

		public void PlayBBWHit ()
		{
				BBWHit.Play ();
		}

		public void PlayBBWKilled ()
		{
				BBWKilled.Play ();
		}

		public void PlayBite ()
		{
				Bite.Play ();
		}

		public void PlayBroomstickWhoosh ()
		{
				BroomstickWhoosh.Play ();
		}

		public void PlayClaw ()
		{
				Claw.Play ();
		}

		public void PlayColdCastle ()
		{
				ColdCastle.Play ();
		}

		public void PlayCollapsingPlatform ()
		{
				CollapsingPlatform.Play ();
		}

		public void PlayCreepyWoods ()
		{
				CreepyWoods.Play ();
		}

		public void PlayCWHurt ()
		{
				CWHurt.Play ();
		}

		public void PlayCWKilled ()
		{
				CWKilled.Play ();
		}

		public void PlayHowl ()
		{
				Howl.Play ();
		}

		public void PlayHuffAndPuff ()
		{
				HuffAndPuff.Play ();
		}

		public void PlayJumpBBW ()
		{
				JumpBBW.Play ();
		}

		public void PlayJumpLRRH ()
		{
				JumpLRRH.Play ();
		}

		public void PlayLRRHHit ()
		{
				LRRHHit.Play ();
		}

		public void PlayLRRHKilled ()
		{
				LRRHKilled.Play ();
		}

		public void PlayPlayerSpawn ()
		{
				PlayerSpawn.Play ();
		}

		public void PlayProjectileCat ()
		{
				ProjectileCat.Play ();
		}

		public void PlayReachedExit ()
		{
				ReachedExit.Play ();
		}

		public void PlayTHit ()
		{
				THit.Play ();
		}

		public void PlayTKilled ()
		{
				TKilled.Play ();
		}

		public void PlayToweringBeanstalk ()
		{
				ToweringBeanstalk.Play ();
		}

		public void PlayTransformingLight ()
		{
				TransformingLight.Play ();
		}

		public void PlayTrollHeadCrush ()
		{
				TrollHeadCrush.Play ();
		}

		public void PlayWCHit ()
		{
				WCHit.Play ();
		}

		public void PlayWCKilled ()
		{
				WCKilled.Play ();
		}

		public void PlayWoodCutterAxe ()
		{
				WoodCutterAxe.Play ();
		}
		
}
