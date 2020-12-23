using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class HealthManager : MonoBehaviour
{

    GameObject playerSprite;
    SpriteRenderer sprite;
    Animator PlayerAnimator;
    bool playerDies;
    GameObject GameOverImage;
    GameObject LevelCompleteImage;
    Transform playerStartPosition;

    int startHealth = 100;
    int currentHealth;
    Slider healthSlider;

    bool isDead = false;
    bool damaged;

    TextMeshProUGUI CoinsTextMeshPro;

    int collectedCount = 0;
    int PreviousCollectedCount;

    Image damageImage;
    public float flashSpeed = 2f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.3f);

    private AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = AudioManager.instance;

        CoinsTextMeshPro = GameObject.Find("CoinCount").GetComponent<TextMeshProUGUI>();
        

        playerStartPosition = transform;

        currentHealth = startHealth;
        playerSprite = GameObject.Find("PlayerSprite");
        GameOverImage = GameObject.Find("GameOverImage");
        PlayerAnimator = playerSprite.GetComponent<Animator>();
        sprite = playerSprite.GetComponent<SpriteRenderer>();
        playerDies = false;
        if (GameOverImage != null)
        {
            GameOverImage.SetActive(false);
        }
        healthSlider = GameObject.Find("HealthSlider").GetComponent<Slider>();
        damageImage = GameObject.Find("DamageImage").GetComponent<Image>();

        LevelCompleteImage = GameObject.Find("LevelCompleteImage");
        if (LevelCompleteImage != null)
        {
            LevelCompleteImage.SetActive(false);
        }

        PreviousCollectedCount = collectedCount - 1;


    }

    // Update is called once per frame
    void Update()
    {
        if (CoinsTextMeshPro != null && PreviousCollectedCount < collectedCount)
        {
            CoinsTextMeshPro.text = collectedCount.ToString();
            PreviousCollectedCount = collectedCount;
            if (collectedCount > 0)
            {
            audioManager.PlaySound("Collected");
            }

        }

        if (damaged)
        {
            damageImage.color = flashColour;
            if (audioManager != null)
            {
                audioManager.PlaySound("Player Damage Audio");
            }

        }
        else
        {
            if (damageImage != null)
            {
                damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);

            }
        }

        damaged = false;

        if (transform.position.y < -10 && !isDead)
        {
            TakeDamage(100);

        }

    }

    public void collected()
    {
        collectedCount += 1;
    }

    public void addHealth(int amout)
    {
        currentHealth += amout;

        if (currentHealth > 100)
        {
            currentHealth = 100;
        }
        if (currentHealth < 100 && currentHealth > 0)
        {
            if (audioManager != null)
            {
                audioManager.PlaySound("Collected");
            }
        }

        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }

    }

    public void TakeDamage(int amount)
    {
        damaged = true;
        Debug.Log("player damaged");


        currentHealth -= amount;

        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }


        if (currentHealth <= 0 && !playerDies)
        {

            Death();
            damaged = false;
            Debug.Log("player is dead");

        }

    }


    void Death()
    {
        isDead = true;
        playerDies = true;
        StartCoroutine(GameOver());

    }
    public void PlayerWins()
    {
        if (audioManager != null)
        {
            audioManager.PlaySound("Victory");

        }
        StartCoroutine(playerWinsCoroutine());

    }




    IEnumerator GameOver()
    {
        if (PlayerAnimator != null)
        {
            PlayerAnimator.SetBool("playerDies", playerDies);
        }
        if (audioManager != null)
        {
            audioManager.PlaySound("Game Over Audio");

        }



        yield return new WaitForSeconds(1.5f);

        if (GameOverImage != null)
        {
            GameOverImage.SetActive(true);
        }

        sprite.enabled = false;

        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(0);

    }

    IEnumerator playerWinsCoroutine()
    {

        yield return new WaitForSeconds(1f);

        if (LevelCompleteImage != null)
        {
            LevelCompleteImage.SetActive(true);
        }

        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(1);

    }



}
