using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGameManager : MonoBehaviour
{
    float Score;
    public Text ScoreText;
    float highScore;
    public Text highScoreText;
    public GameObject Conguratulations;
    void Start()
    {
        Score = PlayerPrefs.GetFloat("SCORE");
        if (ScoreText != null)
        {
            ScoreText.text = Score.ToString("f1");
        }

        if (PlayerPrefs.HasKey("highScore") == true)
        {//セーブデータがある→HasKey
            highScore = PlayerPrefs.GetFloat("highScore");//ゼロを代入。データをロード
            if (highScore < Score)//lastScoreは固定値
            {//もしもラストスコアがハイスコアより大きかったら
                highScore = Score;//更新ーーー→ハイスコアに代入
                PlayerPrefs.SetFloat("highScore", Score);//Setはデータをセーブ
                Conguratulations.SetActive(true);
            }
        }
        else
        {
            highScore = Score;//更新ーーー→ハイスコアに代入
            PlayerPrefs.SetFloat("highScore", Score);//Setはデータをセーブ
        }
        if (highScoreText != null)
        {
            highScoreText.text = PlayerPrefs.GetFloat("highScore").ToString();
        }
    }
    void Update(){

    }
    public void OnClickmain()
    {
        SceneManager.LoadScene("Main");
    }
    public void OnClickSatrt()
    {
        SceneManager.LoadScene("FirstStart");
    }
}