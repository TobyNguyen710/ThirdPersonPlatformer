using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int score;
    [SerializeField] private TextMeshProUGUI scoreText;
    private CoinTrigger[] coins;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        coins = FindObjectsByType<CoinTrigger>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (CoinTrigger coin in coins)
        {
            coin.OnCoinCollected.AddListener(IncrementScore);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void IncrementScore()
    {
        score++;
        scoreText.text = $"Score:  {score}";
        //Debug.Log("Score: " + score);
    }
}
