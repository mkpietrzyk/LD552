using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public PlayerController player;
    public bool gameStarted = false;
    public bool gamePaused = false;
    public Animator transition;
    public string uiState = "mainmenu";
    public int playerHealth;
    public int playerMana;
    public int playerSpellsCasted;
    public GameObject enemyPrefab;
    public int enemiesCount = 0;
    public int enemiesBodyCount = 0;
    
    public TextMeshProUGUI enemiesBodiesText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI manaText;
    
    public TextMeshProUGUI enemiesDefeatedText;
    public TextMeshProUGUI spellsCastedText;

    public GameObject endgameUI;
    public GameObject startUI;
    public GameObject pauseUI;
    public GameObject playerUI;

    public List<GameObject> uis = new List<GameObject>();
    
    public AudioSource audioSource;
    public AudioClip dedSound;

    
    void Start()
    {
        ResetState();
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        endgameUI = GameObject.FindWithTag("endgameui");
        startUI = GameObject.FindWithTag("startui");
        playerUI = GameObject.FindWithTag("gameplayui");
        pauseUI = GameObject.FindWithTag("pauseui");
        uis.Add(playerUI);
        uis.Add(pauseUI);
        uis.Add(endgameUI);
        uis.Add(startUI);
        SetUIState("startui");
    }
    
    void Update()
    {
        playerHealth = player.health;
        playerMana = player.mana;
        playerSpellsCasted = player.spellsCasted;
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!gamePaused && gameStarted)
            {
                gamePaused = true;
                PauseGame();    
            }
            else
            {
                gamePaused = false;
                ResumeGame();
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
        }

        if (player.health == 0)
        {
            EndGame();
        }
        
        if (gameStarted && enemiesCount < 50 && player.health > 0)
        {
            Instantiate(enemyPrefab);
            enemiesCount++;
        }
        
        enemiesBodiesText.text = $"Enemies killed: {enemiesBodyCount:D5}";
        healthText.text = $"HP: {playerHealth:D3}";
        manaText.text = $"Mana: {playerMana:D3}";
        
        
        enemiesDefeatedText.text = $"You have defeated {enemiesBodyCount:D5} enemies";
        spellsCastedText.text = $"By casting {playerSpellsCasted:D5} spells";
    }
    
    public void ResetState()
    {
        enemiesCount = 0;
        enemiesBodyCount = 0;
        gameStarted = false;
        gamePaused = false;
    }
    
    public void StartGame()
    {
        gameStarted = true;
        SetUIState("gameplayui");
    }
    
    public void PauseGame()
    {
        gamePaused = true;
        SetUIState("pauseui");
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        SetUIState("gameplayui");
        Time.timeScale = 1;
    }

    public void EndGame()
    {
        audioSource.Play();
        SetUIState("endgameui");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
    public void ReloadGame()
    {
        StartCoroutine(LoadLevel(0));
    }

    IEnumerator LoadLevel(int sceneIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(sceneIndex);
    }

    public void SetUIState(string state)
    {
        foreach (var ui in uis)
        {
            if (ui.CompareTag(state))
            {
                ui.SetActive(true);
            }
            else
            {
                ui.SetActive(false);
            }
        }
    }
}
