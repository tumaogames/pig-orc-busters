using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour {

	public GameObject enemyPrefab;
	public GameObject enemyPrefabQueen;
	public List<GameObject> enemies;
	private float enemySpawningCooldown = 3f;
	private float enemySpawningTimer = 3f;
	private float finalEnemySpawningCooldown = 1f;
	private float finalEnemySpawningTimer = 1f;
	private float levelTimer = 20f;
	private float finalWaveDelay = 3f;
	private float finalWaveTimer = 10f;
	public TMP_Text queenIsComming;
	private bool instanceQueen = false;
	public int level = 1;
	public float nextLevel = 3f;
	public float nextLevelTimer = 3f;
	public TMP_Text levelTxt;
	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
        levelTxt.text = "Level " + level;
        switch (level) {
			case 1:
                startLevel ();
                print("level 1");
                break;
			case 2:
				print ("level 2");
                startLevel ();
				break;
			case 3:
				print ("level 3");
                startLevel ();
				break;
			case 4:
				print ("level 4");
                startLevel ();
				break;
			case 5:
				print ("level 5");
                startLevel ();
				break;
			case 6:
				print ("level 6");
                startLevel ();
				break;
            case 7:
                print("level 7");
                startLevel();
                break;
            case 8:
                print("level 8");
                startLevel();
                break;
            case 9:
                print("level 9");
                startLevel();
                break;
            case 10:
                print("level 10");
                startLevel();
                break;
            case 11:
                print("level 11");
                startLevel();
                break;
            case 12:
                print("level 12");
                startLevel();
                break;
            case 13:
                print("level 13");
                startLevel();
                break;
            case 14:
                print("level 14");
                startLevel();
                break;
            case 15:
                print("level 15");
                startLevel();
                break;
            default:
				queenIsComming.gameObject.SetActive (true);
				AudioManager.Instance.PlaySound(6);
				queenIsComming.text = "You Win";
				StartCoroutine (restartGame());
				break;
		}

		
	}

	public void startLevel(){
        enemySpawningTimer -= Time.deltaTime;
		levelTimer -= Time.deltaTime;
		if (enemySpawningTimer <= 0f && !GameManager.instance.GameOver && levelTimer >= 0f) {
			spawnEnemy ();
			enemySpawningTimer = enemySpawningCooldown;
		} else if(levelTimer <= 0f && GameManager.instance.EnemyList.Count == 0 && finalWaveDelay >= 0f){
			finalWaveDelay -= Time.deltaTime;
			queenIsComming.gameObject.SetActive (true);
		} else if(finalWaveDelay <= 0f && finalWaveTimer >= 0f && finalEnemySpawningTimer >= 0f){
			finalEnemySpawningTimer -= Time.deltaTime;
			finalWaveTimer -= Time.deltaTime;
			queenIsComming.gameObject.SetActive (false);
		}

		if(finalEnemySpawningTimer <= 0f && finalWaveTimer >= 0f){
			finalSpawnEnemy ();
		} else if(GameManager.instance.EnemyList.Count == 0 && finalWaveTimer <= 0f){
			queenIsComming.gameObject.SetActive (true);
			queenIsComming.text = "Level Cleared";
			nextLevel -= Time.deltaTime;
			if(nextLevel <= 0){
				level += 1;
				levelTimer = 20f;
				finalWaveDelay = 3f;
				finalEnemySpawningTimer = 1f;
				finalWaveTimer = 10f;
				nextLevel = nextLevelTimer;
				instanceQueen = false;
				queenIsComming.gameObject.SetActive (false);
				queenIsComming.text = "Queen is Coming";
			}
		}
	}


	IEnumerator restartGame(){
		yield return new WaitForSeconds (3f);
		SceneManager.LoadScene ("GameMenu");

	}


	void spawnEnemy(){
		enemySpawningTimer = enemySpawningCooldown;
		GameObject enemyObject;
		if (level > 1) {
			enemyObject = Instantiate (enemies[Random.Range (0, level)]);
		} else {
			enemyObject = Instantiate (enemies[0]);
		}
		enemyObject.transform.position = new Vector3 (Random.Range (33.4f, 58.7f), -0.429f, 45f);
		GameManager.instance.RegisterEnemy (enemyObject);
	}
	void finalSpawnEnemy(){
		finalEnemySpawningTimer = finalEnemySpawningCooldown;
		if (!instanceQueen) {
			GameObject enemyObjectQueen = Instantiate (enemyPrefabQueen);
			enemyObjectQueen.transform.position = new Vector3 (Random.Range (33.4f, 58.7f), -0.429f, 45f);
			GameManager.instance.RegisterEnemy (enemyObjectQueen);
			instanceQueen = true;
		}
		GameObject enemyObject;
		if (level > 1) {
			enemyObject = Instantiate (enemies[Random.Range (0, level)]);
		} else {
			enemyObject = Instantiate (enemies[0]);
		}
		enemyObject.transform.position = new Vector3 (Random.Range (33.4f, 58.7f), -0.429f, 45f);
		GameManager.instance.RegisterEnemy (enemyObject);
	}
}
