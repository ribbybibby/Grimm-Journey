using UnityEngine;
using UnityEditor;
using System.Collections;

public class FindAudioSources : EditorWindow {

	[MenuItem("Window/FindAudioSources")]
	public static void ShowWindow()
	{
		EditorWindow.GetWindow(typeof(FindAudioSources));
	}

	public void OnGUI()
	{
		if (GUILayout.Button("Find name of Audio Sources from Array"))
		{
			FindInSelected();
		}
	}

	public void FindInSelected()
	{
		GameObject go = Selection.activeGameObject;
		AudioSource[] hello = go.gameObject.GetComponent<SoundManager>().Asources;
		Debug.Log (hello.Length);
		for (int i =0; i < hello.Length; i++)
		{
			Debug.Log ("Index " + i + ": " + hello[i].clip);
		}
	}
}
