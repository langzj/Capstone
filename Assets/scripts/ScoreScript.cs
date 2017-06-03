using UnityEngine;
using System.Collections;

public class ScoreScript : MonoBehaviour {

    public GUIText scoreText;
    public GUIText healthText;
    public int score;
    
        
    public static ScoreScript Instance;
    

    private HealthScript playerHealth;

    void Awake()
    {
        // Make sure only one script is initiation, not multiple
        if (Instance != null)
        {
            Debug.LogError("Multiple instances of scoreText!");
        }
        Instance = this;

        Instance.score = 0;
        Instance.updateScore();
                
    }


    
    public void AddScore (int newVal)
    {
        score += newVal;
        updateScore();
    }

    void updateScore() {
        scoreText.text = "Score: " + score;
        
    }

    public void updateHealth(int hp)
    {
        healthText.text = "Health: " + hp;
    }
}
