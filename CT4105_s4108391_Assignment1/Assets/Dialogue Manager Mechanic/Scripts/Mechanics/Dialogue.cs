using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Conversation {

	[Space(10)]
	
	public string NPCName;

	public Color NPCNameColour = new Color( 0f, 0f, 0f, 1f );

	[Space(10)]

	[TextArea( 3, 10 )]
	public string sentence;

	public Color sentenceColour = new Color( 0f, 0f, 0f, 1f );
	
}

[System.Serializable]
public class Dialogue {

	[Help("Start by supplying Defaults below. Then, for each Conversation Element you can override the Defaults with a new value. Otherwise, leave blank to use Defaults.")]
	
	public string defaultNPCName;

	public Color defaultNPCNameColour = new Color( 255.0f/255.0f, 255.0f/255.0f, 255.0f/255.0f, 255.0f/255.0f );

	public Color defaultSentenceColour = new Color( 204.0f/255.0f, 204.0f/255.0f, 204.0f/255.0f, 255.0f/255.0f );

	[Header("Delay typing start-time (in seconds)")]
	public float startDelay = 1f;

	[Header("Delay each character appearing (in seconds)")]
	public float characterDelay = 0.03f;

	[Space(10)]

	public Conversation[] conversation = new Conversation[100];

}