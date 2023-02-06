using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BaseGameManager : MonoBehaviour
{
    public static BaseGameManager instance;
    [SerializeField]
    private Spawner spawner;
    [SerializeField]
    private List<Base> allBases;
    public GameObject startMenu, gameOverMenu, roundMenu;
    public Projectile proj;
    //UI
    private float levelTime, baseLevelTime = 10f;
    private int totalPoints, basesAlive, level, shipUgrade=200, reviveUpgrade=500, totalScore;
    bool gameStarted, isRoundOver;
    public TextMeshProUGUI levelText, timerText, scoreText, basesText, reviveText,upgradeText, totalScoreText;
    public Button upgradeBtn, reviveBtn;
    public Transform baseParent;
    public int BaseCount
    {
        get
        {
            return allBases.Count;
        }
    }
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        level = 0;
    }
    public void BuyUpgrade(int cost)
    {
        
        if(totalPoints>=cost)
        {
            totalPoints -= cost;
        }
    }

    public Base GetRandomBase()
    {
        for (int i = BaseCount-1; i >=0; i--)
        {
            if (!allBases[i].possibleTarget())
            {
                allBases.RemoveAt(i);
            }
        }
        return allBases[Random.Range(0, BaseCount)];
    }
    private void Start()
    {
        startMenu.SetActive(true);
        gameOverMenu.SetActive(false);
    }
    public void StartGame()
    {
        isRoundOver = false;
        level++;
        levelTime = baseLevelTime * level;
        startMenu.SetActive(false);
        roundMenu.SetActive(false);
        Time.timeScale = 1f;
        gameStarted = true;
        StartCoroutine(spawner.StartSpawning());
    }
    public void AddGold(int value)
    {
        totalPoints += value;
    }
    public void NextRound()
    {
        roundMenu.SetActive(true);
        reviveText.text = "Revive Roots: " + reviveUpgrade;
        upgradeText.text = "Base Upgrade: " + shipUgrade;
    }
    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public bool hasStarted()
    { return gameStarted; }
    public void UpgradeShip()
    {
        BuyUpgrade(shipUgrade);
        proj.UpgradeStrength(level);

    }
    public void ReviveRoots()
    {
        BuyUpgrade(reviveUpgrade);
        allBases.Clear();
        foreach (Base item in baseParent.GetComponentsInChildren<Base>())
        {
            item.ResetHealth();
          
            allBases.Add(item);
        }
    }
    public void RoundComplete()
    {

        Time.timeScale = 0f;
        isRoundOver = true;
        spawner.Stop();
        spawner.RemoveAllBaddies();
        if (basesAlive <= 0)
            GameOver();
        else
            NextRound();
    }
    public bool roundCompleted()
    {
        return isRoundOver;
    }
    void CheckButtons()
    {
        if (totalPoints >= shipUgrade)
        {
            upgradeBtn.interactable = true;
        }
        else
            upgradeBtn.interactable = false;

        if (totalPoints >= reviveUpgrade)
        {
            reviveBtn.interactable = true;
        }
        else
            reviveBtn.interactable = false;
    }
    void CombineTotalScore()
    {
        totalScore = totalPoints * level;
        totalScoreText.text = "Score: " + totalScore;
    }
    public void GameOver()
    {

        spawner.RemoveAllBaddies();
        spawner.Stop();
        Time.timeScale = 0f;
        gameOverMenu.SetActive(true);
        gameStarted = false;
        CombineTotalScore();
    }    // Update is called once per frame
    void Update()
    {
        if(gameStarted)
        {


            CheckButtons();
            basesAlive = BaseCount;
            timerText.text = Mathf.Round(levelTime).ToString("00");
            scoreText.text = totalPoints.ToString();
            levelText.text = "Round: " + level;
            basesText.text = "Roots: " + basesAlive;
           levelTime -= Time.deltaTime;
            if(BaseCount<=0)
            {
                GameOver();
            }
            if(levelTime<=0  && !isRoundOver)
            {
                levelTime = 0;
                RoundComplete();
            }
        }
    }
}
