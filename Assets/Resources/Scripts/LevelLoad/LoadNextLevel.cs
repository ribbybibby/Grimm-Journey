using UnityEngine;
using System.Collections;

public class LoadNextLevel : MonoBehaviour {

	//Running out of time so I'm not using GetComponent camera I'm just going to plug it in here
	public Camera mainCam;


	private bool bbw;
	private bool lrrh;

	private bool hasPlayedVic;

	public NarrationManager localNarrationManager;

	// Use this for initialization
	void Start () {
		bbw = false;
		lrrh = false;
		hasPlayedVic = false;

		localNarrationManager = GameObject.Find ("NarrationManager").GetComponent<NarrationManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (lrrh == true && bbw == true)
		{
			mainCam.transform.SendMessage("startwaitThenPanUp", SendMessageOptions.DontRequireReceiver);
		}
	}

	IEnumerator OnTriggerStay2D(Collider2D other) {
		if (other.name == "BBW" || other.name == "LRRH")
		{
			other.GetComponent<EnemyReceiver>().invuln = true;
		}

		if (other.name == "BBW")
		{
			bbw = true;
		}

		if (other.name == "LRRH")
		{
			lrrh = true;
		}

		/*
		 * We use the below to play the victory help narration
		 * If one of the characters hits the victory point but not the other leave it for a little while (wait for seconds)
		 * After this play the narration if the case is still the same (not both player are in).
		 * Since this means that we are still waiting for the next player
		 * If this is no longer the case after wait for seconds do nothing 
		 * Since the next level will already have loaded (and we do not need to play the narration)
		 * BUG: The narration will play twice?
		 */
		if((other.name == "LRRH" && other.name != "BBW") ||(other.name == "BBW" && other.name != "LRRH"))
		{
			if(hasPlayedVic == false)
			{
				yield return new WaitForSeconds(7.0F);
				if(other.name == "LRRH" && other.name == "BBW")
				{
					//Do nothing and load next level (see Update())
				}else{
					localNarrationManager.playNoBothExitNar();
					Debug.Log("Does this appear twice?");
					hasPlayedVic = true;
				}
			}
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.name == "BBW" || other.name == "LRRH")
		{
			other.GetComponent<EnemyReceiver>().invuln = false;
		}

		if (other.name == "BBW")
		{
			bbw = false;
		}
		
		if (other.name == "LRRH")
		{
			lrrh = false;
		}
	}
}
