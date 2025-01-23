using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {
	
	public GameObject dialogueBox;

	public Text nameText;

	public Text dialogueText;	

	public Button continueButton;

	public Animator animator;

	private float startDelay;

	private float characterDelay;

	[Space(10)]

	public bool openDialogueBoxOnStart = true;

	private Queue<string> names;

	private Queue<Color> namesColour;

	private Queue<string> sentences;

	private Queue<Color> sentencesColour;

	private Color defaultColour = new Color( 0f, 0f, 0f, 1f );

	void Start () {

		names = new Queue<string>();

		namesColour = new Queue<Color>();

		sentences = new Queue<string>();

		sentencesColour = new Queue<Color>();

		if( openDialogueBoxOnStart )
		{

			animator.SetBool( "IsOpen", true );

		}

		StartCoroutine( TypeSentence( dialogueText.text ) );

	}

	public void StartDialogue ( Dialogue dialogue )
	{

		StopAllCoroutines();

		startDelay = dialogue.startDelay;

		characterDelay = dialogue.characterDelay;

		animator.SetBool( "IsOpen", true );

		nameText.text = "";

		dialogueText.text = "";

		names.Clear();

		namesColour.Clear();

		sentences.Clear();

		sentencesColour.Clear();

		foreach( Conversation person in dialogue.conversation )
		{

			if( person.NPCName == "" )
			{
				
				person.NPCName = dialogue.defaultNPCName;

			}


			names.Enqueue( person.NPCName );
	
			if( person.NPCNameColour.Equals( defaultColour ) )
			{

				namesColour.Enqueue( dialogue.defaultNPCNameColour );

			} else {

				namesColour.Enqueue( person.NPCNameColour );

			}


			sentences.Enqueue( person.sentence );

			if( person.sentenceColour.Equals( defaultColour ) )
			{

				sentencesColour.Enqueue( dialogue.defaultSentenceColour );

			} else {

				sentencesColour.Enqueue( person.sentenceColour );

			}

		}

		nameText.text = names.Peek();

		nameText.color = namesColour.Peek();

		StartCoroutine( WaitForFirstSentence( startDelay ) );

	}

	IEnumerator WaitForFirstSentence ( float waitTime )
	{

		yield return new WaitForSeconds( waitTime );

		DisplayNextSentence();

	}

	public void DisplayNextSentence ()
	{

		if( sentences.Count == 0 )
		{

			EndDialogue();
			
			return;

		}

		string name = names.Dequeue();

		nameText.text = name;

		nameText.color = namesColour.Dequeue();

		string sentence = sentences.Dequeue();

		dialogueText.color = sentencesColour.Dequeue();

		StopAllCoroutines();

		StartCoroutine( TypeSentence( sentence ) );

	}

	IEnumerator TypeSentence ( string sentence )
	{

		dialogueText.text = "";

		foreach( char letter in sentence.ToCharArray() )
		{

			dialogueText.text += letter;

			yield return new WaitForSeconds( characterDelay );

		}

	}

	public void EndDialogue()
	{

		StopAllCoroutines();

		animator.SetBool( "IsOpen", false );

	}

}
