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
		AudioSource[] Asources;

		// Use this for initialization
		void Start ()
		{
		
			Asources = gameObject.GetComponents<AudioSource> ();
		
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

		}
	
		// Update is called once per frame
		void Update ()
		{

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
