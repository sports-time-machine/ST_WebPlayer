using UnityEngine;
using System.Collections;

public class FPSCounter : MonoBehaviour
{
	public Rect position = new Rect(0, 0, 100, 100);
	public GUIStyle style;
	private int count;
	private float time;
	private string text;
	
	// Use this for initialization
	void Start()
	{
		
	}
	
	// Update is called once per frame
	void Update()
	{
		time += Time.deltaTime;
		count++;
		
		if (time >= 1.0f)
		{
			text = count + " FPS";
			time = 0.0f;
			count = 0;
		}
	}
	
	void OnGUI()
	{
		GUI.Label(position, text, style);
	}
}