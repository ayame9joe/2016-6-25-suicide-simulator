using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	// TODO: 气泡效果；情绪效果 

	// ------ Texts and their Positions ------

	public Vector3[] postions = new Vector3[5];

	public Text[] texts = new Text[5]; 

	// ------ Variables for Word Choosing

	int languageLevel = 0;

	bool isAdjBranch;

	/// <summary>
	/// The index of the sentiment.
	/// </summary>
	float sentimentIndex;




	//------- Strings ---------

	string[] nounToAdjWords = new string[]{"mom is ", "dad is ", "I am", "I feel ", "my family are ", "friends are ", "surroundings are ", "my partner are"};

	string[] adjWords = new string[]{"strict", "protective", "awesome", "lonely", "stressed", "awkward", "poor", "rich", "anxious", "intimate", "warm", "violent", "sorry"};

	string[] conjWords = new string[]{"and", "so", "but", "yet", "however"};

	string[] nounToVerbWords = { "mom", "dad", "I", "friends", "my partner" };

	string[] verbWords = new string[]{"like", "should", "hope", "have"};


	/// <summary>
	/// 鼠标拾取的有效距离
	/// </summary>
	float distance = 20f;

	// Use this for initialization
	void Start () {
		ChooseWords ();
	}
	
	// Update is called once per frame
	void Update () {
		MousePicks ();
	}

	void WordsLayout (string[] words) {
		for (int i = 0; i < texts.Length; i++) {
			int tempWordId = Random.Range (0, words.Length - 1);
			texts [i].text = words [tempWordId];
			texts [i].transform.localPosition = postions [i];
		}
	}

	void ChooseWords () {


		if (languageLevel < 4) {
			languageLevel++;
		} else {
			languageLevel = 1;
		}


		switch (languageLevel) {
		case 1:
			if (Random.Range (1, 10) < 6) {
				isAdjBranch = true;
				WordsLayout (nounToAdjWords);
			} else {
				isAdjBranch = false;
				WordsLayout (nounToVerbWords);
			}
			break;
		case 2:
			if (isAdjBranch) {
				WordsLayout (adjWords);
			} else {
				WordsLayout (verbWords);
			}
			break;
		case 3:
			WordsLayout (conjWords);
			break;
			
		}
	}

	void MousePicks () {
		//检测鼠标左键的拾取  
		if (Input.GetMouseButtonDown(0)) { 
			RaycastHit2D hit = Physics2D.Raycast(Input.mousePosition, Vector2.zero);
			if (hit) {
				print (hit.collider.name);
				if (hit.collider.tag == "Words") {
					ChooseWords ();
					string chosenWords = hit.collider.gameObject.GetComponent<Text> ().text;
					if (chosenWords == "stressed") {
						sentimentIndex--;
					}
				}
			}
		}  
	}
}
