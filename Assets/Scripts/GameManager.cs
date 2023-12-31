using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;
	[SerializeField] GameObject player;
	private bool gameOver = false;
	public List<GameObject> EnemyList = new List<GameObject> ();
	public GameController GameCon;

	public bool GameOver{
		get { return gameOver;}
		set { gameOver = value;}
	}

	public GameObject Player{
		get { return player;}
	}

	void Awake(){
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PlayerHit(int currentHP){
		if (currentHP > 0) {
			gameOver = false;
		} else {
			gameOver = true;
			StartCoroutine (endGame());
		}
	}

	IEnumerator endGame(){
		GameCon.queenIsComming.gameObject.SetActive (true);
		GameCon.queenIsComming.text = "Game Over";
		yield return new WaitForSeconds (3f);
		SceneManager.LoadScene ("GameMenu");

	}

	public void RegisterEnemy(GameObject enemy){
		EnemyList.Add (enemy);
	}

	public void UnregisterEnemy(GameObject enemy){
		EnemyList.Remove(enemy);
	}
}
