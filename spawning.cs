using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;
using static UnityEngine.GraphicsBuffer;

public class spawning : MonoBehaviour
{
    private float spawnRate = 3f;
    public GameObject barrel;
    public TextMeshProUGUI scoreText;
    private Vector2 spawnpos=new Vector2(-9,5.3f);
    private int score;
    public bool gameover;
    // Start is called before the first frame update
    void Start()
    {
      //  gameover= controller.gameOver;
        score = 0;
        StartCoroutine(SpawnTarget());
        scoreText.text = "Score: " + score;
        
    }

    // Update is called once per frame
    void Update()
    {
        gameover = controller.gameOver;
    }
    IEnumerator SpawnTarget()
    {
        while (gameover==false)
        {
            yield return new WaitForSeconds(spawnRate);

            Instantiate(barrel);
        }

    }

    public void UpdateScore(int ScoreToAdd)
    {
        score += ScoreToAdd;
        scoreText.text = "Score" + score;
    }
}
