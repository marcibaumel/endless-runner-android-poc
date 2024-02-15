using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject obstacle;
    public Transform[] spawnPoint = new Transform[2];
    int score = 0;
    public TextMeshProUGUI scoreText;
    public GameObject playButton;
    public GameObject creditButton;
    public GameObject player;
    [SerializeField] RectTransform fader;

    void Start()
    {
        playButton.SetActive(false);
        creditButton.SetActive(false);
        fader.gameObject.SetActive(true);
        LeanTween.scale(fader, new Vector3(1, 1, 1), 0);
        LeanTween.scale(fader, Vector3.zero, 0.5f).setOnComplete(() =>
        {
            fader.gameObject.SetActive(false);
            playButton.SetActive(true);
            creditButton.SetActive(true);
            LeanTween.scale(playButton, new Vector3(1, 1, 1), 0.5f);
            LeanTween.scale(creditButton, new Vector3(1, 1, 1), 0.5f);
        });
    }

    public void LoadCredit()
    {
        SceneManager.LoadScene(1);
    }

    public void GameStart()
    {
        player.SetActive(true);
        scoreText.gameObject.SetActive(true);
        playButton.SetActive(false);
        creditButton.SetActive(false);
        StartCoroutine("SpawnObstacles");
        InvokeRepeating("ScoreUp", 2.0f, 1.0f);
    }

    void Update()
    {
        if(score == 300){
            SceneManager.LoadScene(2);
        }
    }

    void ScoreUp()
    {
        score++;
        scoreText.text = score.ToString();
    }

    IEnumerator SpawnObstacles()
    {
        while (true)
        {
            int randomNumber = Random.Range(0, spawnPoint.Length);
            float waitTime = Random.Range(0.8f, 2.0f);
            yield return new WaitForSeconds(waitTime);
            Instantiate(obstacle, spawnPoint[randomNumber].position, Quaternion.identity);
        }
    }
}
