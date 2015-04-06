using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {

	AudioSource ColdCastleMusic;
	AudioSource CreepyWoodsMusic;
	AudioSource ToweringBeanstalkMusic;

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
		
		ColdCastleMusic = Asources [0];
		CreepyWoodsMusic = Asources [1];
		ToweringBeanstalkMusic = Asources [2];

		//We check to see which level was last loaded (meaning which is the current level
		//We then use that index to start playing the correct music track
		switch (Application.loadedLevel)
		{
		case 1:
			CreepyWoodsMusic.Play();
			break;
		case 2:
			CreepyWoodsMusic.Play ();
			break;
		case 3:
			ColdCastleMusic.Play ();
			break;
		case 4:
			ToweringBeanstalkMusic.Play ();
			break;
		case 5:
			ToweringBeanstalkMusic.Play ();
			break;
		default:
			Debug.LogError("Please set audio track in Music Manager for this level index");
			break;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		/*Manage the volume of the levels audio
			 *  This does not include the narration volume
			 *  As we want that to drown out the other audio until its finished
			 * */
		ColdCastleMusic.volume = setVolume;
		CreepyWoodsMusic.volume = setVolume;
		ToweringBeanstalkMusic.volume = setVolume;
		
		//Change this to set a bool whenever any narration is playing inside the narrationmanager
		if (localNarrationManager.getTalking() == true) {
			setVolume = 0.2F;
			Debug.Log ("FROMMUSIC_gettalking is " + setVolume);
		}
		
		if (localNarrationManager.getTalking() == false) {
			setVolume = 1.0F;
			Debug.Log ("FROMMUSIC_gettalking is " + setVolume);
		}
	}
	

	
}
