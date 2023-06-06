using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static bool isOnMobile = false;
    public  TMP_Text scoreText;
    public GameObject gameOverPanel;
    public GameObject spawner;
    public GameObject mainMenu;
    public  int score;
    public AudioClip gameMusic;
    public AudioClip gameOverClip;
    public GameObject menuPlayerRight;
    public GameObject menuPlayerLeft;
    public TMP_Text maxScoreText;
    public TMP_Text allTimeScoreText;
    public TMP_Text versionText;
    int maxScore = 0;
    public Button audioButton;
    public Sprite mutedImage;
    public Sprite unmutedImage;

    void Awake()
    {
        if(Application.platform == RuntimePlatform.Android)
        {
            isOnMobile = true;
           
        }
        Debug.Log(isOnMobile);
        StartCoroutine("MenuAnimation");
        maxScore = PlayerPrefs.GetInt("BestScore");
        gameObject.GetComponent<AudioSource>().loop = true;
        versionText.text = "v" + Application.version;
        Debug.Log(maxScore);
        if (maxScore != 0)
        {
            maxScoreText.gameObject.SetActive(true);
            maxScoreText.text = "Best score: " + maxScore;
        }
        else
            maxScoreText.gameObject.SetActive(false);
        if(PlayerPrefs.GetInt("AllTimeScore")>100)
        {
            allTimeScoreText.gameObject.SetActive(true);
            allTimeScoreText.text = "All Time Score: "+PlayerPrefs.GetInt("AllTimeScore");
        }
        else
        {
            allTimeScoreText.gameObject.SetActive(false);
        }

        if(PlayerPrefs.GetInt("mutedAudio") == 1)
        {
            gameObject.GetComponent<AudioSource>().mute = true;
            audioButton.image.sprite = mutedImage;
        }
        StartCoroutine("PlayMenuMusic");
    }

    public  void EndGame()
    {
        if (score > maxScore)
        {
            PlayerPrefs.SetInt("BestScore", score);
        }
        int getAllTimeScore = PlayerPrefs.GetInt("AllTimeScore");
        PlayerPrefs.SetInt("AlltimeScore", getAllTimeScore + score);
        PlayerPrefs.Save();
        gameOverPanel.transform.parent.GetComponent<Animator>().SetBool("GameEnded", true);
        gameObject.GetComponent<AudioSource>().clip = gameOverClip;
        gameObject.GetComponent<AudioSource>().Play();
        gameObject.GetComponent<AudioSource>().loop = false;
        Destroy(spawner.gameObject);

    }
    public  void GetMoney()
    {
        score++;
        scoreText.text = ""+score;
    }
    public void Restart()
    {
        gameOverPanel.transform.parent.GetComponent<Animator>().SetBool("GameEnded",false);
        SceneManager.LoadScene(0);
    }
    public void StartGame()
    {
        mainMenu.GetComponent<Animator>().SetBool("GameStart", true);
        gameObject.GetComponent<AudioSource>().clip = gameMusic;
        gameObject.GetComponent<AudioSource>().Play();
        maxScoreText.gameObject.SetActive(false);
        allTimeScoreText.gameObject.SetActive(false);
        PlayerPrefs.Save();
    }

    IEnumerator MenuAnimation()
    {
        yield return new WaitForSeconds(Random.Range(15f,60f));
        if(Random.Range(1, 2) == 1 && gameObject.GetComponent<AudioSource>().clip != gameMusic)
        {
            menuPlayerLeft.GetComponent<Animator>().SetBool("AnimateNow", true);
        }
        else if(gameObject.GetComponent<AudioSource>().clip != gameMusic)
        {
            menuPlayerRight.GetComponent<Animator>().SetBool("AnimateNow", true);
            
        }
        StartCoroutine("AnimationCanceller");
    }

    IEnumerator AnimationCanceller()
    {
        yield return new WaitForSeconds(7f);
        menuPlayerLeft.GetComponent<Animator>().SetBool("AnimateNow", false);
        menuPlayerRight.GetComponent<Animator>().SetBool("AnimateNow", false);

        StartCoroutine("MenuAnimation");
    }
    IEnumerator PlayMenuMusic()
    {
        yield return new WaitForSeconds(1f);
        if(gameObject.GetComponent<AudioSource>().clip != gameMusic)
        {
            gameObject.GetComponent<AudioSource>().Play();
        }
    }
    public void AudioControl()
    {
        gameObject.GetComponent<AudioSource>().mute = !gameObject.GetComponent<AudioSource>().mute;
        if(gameObject.GetComponent<AudioSource>().mute == true)
        {
            audioButton.image.sprite = mutedImage;
            PlayerPrefs.SetInt("mutedAudio", 1);
        }
        else
        {
            audioButton.image.sprite = unmutedImage;
            PlayerPrefs.SetInt("mutedAudio", 0);
        }
    }

    public void Support()
    {
        Application.OpenURL("https://www.paypal.com/donate/?hosted_button_id=43MBNHFP49N7Y");
    }


}
