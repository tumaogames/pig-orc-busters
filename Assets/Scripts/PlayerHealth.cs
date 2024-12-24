using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

public class PlayerHealth : MonoBehaviour {

	[SerializeField] int startingHealth = 100;
    [SerializeField] int PlayerHealthDefense = 30;
    [SerializeField] float timeSinceLastHit = 2f;
	[SerializeField] Slider healthSlider;
    [SerializeField] int reducedDefense = 10; // Defense value during power-up
    [SerializeField] float increaseFire = 0.1f; // Defense value during power-up
    [SerializeField] float cooldownFire = 0.4f; // Defense value during power-up
    [SerializeField] int powerUpDuration = 10; // Duration of the power-up effect
    [SerializeField] Image imageShield;
    [SerializeField] Image imageSword;


    private float timer = 0f;
	private CharacterController characterController;
	private CharacterMovement characterMovement;
	private Animator anim;
	private int currentHealth;
    public GameObject[] CollectedEffect;
    public AudioClip[] ItemSounds;
    int EffectCount;

    void Awake(){
		Assert.IsNotNull (healthSlider);
	}

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		characterController = GetComponent<CharacterController> ();
		characterMovement = GetComponent<CharacterMovement> ();
		currentHealth = startingHealth;
	}
	
	// Update is called once per frame
	void Update () {

		timer += Time.deltaTime;
	}

	void OnTriggerEnter (Collider other){
		if (timer >= timeSinceLastHit && !GameManager.instance.GameOver) {
			if (other.tag == "EnemyBullet") {
				takeHit ();
				timer = 0;
				Destroy (other.gameObject);
			}
			if (other.tag == "EnemyWeapon") {
				takeMeleeHit ();
				timer = 0;
			}
		}

        if (other.tag == "PowerUpHealth")
        {
			PowerUpHealth();
            GameObject newDestroyedEffect = Instantiate(CollectedEffect[EffectCount], other.transform.position, Quaternion.identity);
            Destroy (other.gameObject);
            StartCoroutine(Countdown(newDestroyedEffect));

            EffectCount++;
            if (EffectCount >= CollectedEffect.Length)
            {
                EffectCount = 0;
            }

            //Play random sound
            AudioSource SoundPlayer = this.GetComponent<AudioSource>();
            int randSound = Random.Range(0, ItemSounds.Length);
            SoundPlayer.clip = ItemSounds[randSound];
            SoundPlayer.Play();
        }


        if (other.tag == "PowerUpSword")
        {
            PowerUpSword();
            GameObject newDestroyedEffect = Instantiate(CollectedEffect[EffectCount], other.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            StartCoroutine(Countdown(newDestroyedEffect));

            EffectCount++;
            if (EffectCount >= CollectedEffect.Length)
            {
                EffectCount = 0;
            }

            //Play random sound
            AudioSource SoundPlayer = this.GetComponent<AudioSource>();
            int randSound = Random.Range(0, ItemSounds.Length);
            SoundPlayer.clip = ItemSounds[randSound];
            SoundPlayer.Play();
            StartCoroutine(IncreaseFireTemporarily());
        }

        if (other.tag == "PowerUpShield")
        {
            GameObject newDestroyedEffect = Instantiate(CollectedEffect[EffectCount], other.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            StartCoroutine(Countdown(newDestroyedEffect));

            EffectCount++;
            if (EffectCount >= CollectedEffect.Length)
            {
                EffectCount = 0;
            }

            //Play random sound
            AudioSource SoundPlayer = this.GetComponent<AudioSource>();
            int randSound = Random.Range(0, ItemSounds.Length);
            SoundPlayer.clip = ItemSounds[randSound];
            SoundPlayer.Play();
            StartCoroutine(IncreaseDefenseTemporarily());
        }
    }

    private IEnumerator IncreaseDefenseTemporarily()
    {
        PlayerHealthDefense = reducedDefense; // Set defense to reduced value
        Debug.Log($"Defense reduced to {PlayerHealthDefense}");
        imageShield.gameObject.SetActive(true);

        yield return new WaitForSeconds(powerUpDuration); // Wait for the duration

        PlayerHealthDefense = 30; // Restore the original defense value
        imageShield.gameObject.SetActive(false);
        Debug.Log($"Defense restored to {PlayerHealthDefense}");
    }

    private IEnumerator IncreaseFireTemporarily()
    {
        characterMovement.resetCoolDown = increaseFire; // Set defense to reduced value
        Debug.Log($"Fire increase to {characterMovement.resetCoolDown}");
        imageSword.gameObject.SetActive(true);

        yield return new WaitForSeconds(powerUpDuration); // Wait for the duration

        characterMovement.resetCoolDown = cooldownFire; // Restore the original defense value
        imageSword.gameObject.SetActive(false);
        Debug.Log($"Fire decrease to {characterMovement.resetCoolDown}");
    }

    private IEnumerator Countdown(GameObject destroyedEffect)
    {

        yield return new WaitForSeconds(0.9f);
        Destroy(destroyedEffect);
    }

    void PowerUpHealth()
    {
        if (currentHealth > 0)
        {
            currentHealth += 50;
            healthSlider.value = currentHealth;
        }
    }

    void PowerUpSword()
    {
        if (currentHealth > 0)
        {
            currentHealth += 50;
            healthSlider.value = currentHealth;
        }
    }

    void takeHit(){
		if (currentHealth > 0) {
			GameManager.instance.PlayerHit (currentHealth);
			//anim.Play("die");
			currentHealth -= PlayerHealthDefense;
			healthSlider.value = currentHealth;
		}

		if (currentHealth <= 0) {
			killPlayer ();
		}
	}

	void takeMeleeHit (){
		if (currentHealth > 0) {
			GameManager.instance.PlayerHit (currentHealth);
			//anim.Play("die");
			currentHealth -= PlayerHealthDefense;
			healthSlider.value = currentHealth;
		}

		if (currentHealth <= 0) {
			killPlayer ();
		}
	}

	void killPlayer(){
		GameManager.instance.PlayerHit (currentHealth);
		anim.SetTrigger ("Dead");
		anim.SetInteger ("State", 6);
		characterController.enabled = false;
		characterMovement.enabled = false;
	}
}
