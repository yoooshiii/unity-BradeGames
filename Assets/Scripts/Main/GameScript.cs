using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Events;

public class GameScript : MonoBehaviour
{
    float score;
    float setHP;
    /*ゲーム  時間類*/
    float G_MaxTime = 60.0f;float GameCurrentTime = 60.0f; public Image TimeImage;float NowTime = 0.0f;
    float AnimTime = 0.0f;
    /*勇者    体力類*/
    float B_MaxHP = 100.0f; float B_CurrentHP = 100.0f; public Image EHPImage;
    /*敵      体力類*/
    float SetHP = 0.0f; float E_MaxHP = 100.0f; float E_CurrentHP = 100.0f; public Image BHPImage;
    /*勇者   　技時間＋イメージ*/     /*敵　　　技時間＋イメージ*/
    float B_MaxTime = 2.0f;       float B_CurrentTime = 3.0f;
    //public Image EnemywazaImage;
    　public Image BravewazaImage;
    /*勇者　　　速さ*/
    float ChangeSpeed = 1.0f;
                　    /*ボタン　カウント*/
    int A_count = 0;int B_count = 0;int C_count = 0;
    public int countAAAB = 0;
    public int countBBAB = 0;
    public int countCCAB = 0;
    public int countBBCC = 0;
    /*繰り出せるか否か*/
    bool A_A = false; bool A_B = false; bool C_C = false; bool B_B = false;

    bool canPlay = true;
    [SerializeField]
    private float Attackpoint = 1;
    [SerializeField]
    private float Recoverpoint = 10.0f;

    [SerializeField]
    private Text BraveHPText;
    [SerializeField]
    private Text EnemyHPText;
    [SerializeField]
    private Text PowerText;
    [SerializeField]
    private Text TimenumText;

    public bool canplay = false;
    public bool anim = false;
    public GameObject btncount;
    Animator Animation;

    ParticleSystem particleR;
    ParticleSystem particleBi;
    ParticleSystem particleEX;
    ParticleSystem particle;
    ParticleSystem particleTM;

    void Start()
    {
        if (PlayerPrefs.HasKey("sethp") == true)
        {
            SetHP = PlayerPrefs.GetFloat("sethp");
            E_MaxHP = SetHP;
            E_CurrentHP = SetHP;
        }
        else
        {
            SetHP = PlayerPrefs.GetFloat("sethp");
            Debug.Log(SetHP);
        }

        PlayerPrefs.SetFloat("B_CurrentHP", B_CurrentHP);
        PlayerPrefs.Save();
        setHP = PlayerPrefs.GetFloat("sethp");
        EnemyHPText.text = E_CurrentHP.ToString();
        BraveHPText.text = B_CurrentHP.ToString();
        PowerText.text = Attackpoint.ToString();

        GameObject objR = GameObject.Find("VenomSpell");
        particleR = objR.GetComponent<ParticleSystem>();

        GameObject objBi = GameObject.Find("Bai_VenomSpellell");
        particleBi = objBi.GetComponent<ParticleSystem>();

        GameObject objEX = GameObject.Find("AerialExplode");
        particleEX = objEX.GetComponent<ParticleSystem>();

        GameObject obj = GameObject.Find("IceSpear");
        particle = obj.GetComponent<ParticleSystem>();

        GameObject objt = GameObject.Find("BladeStorm");
        particleTM = objt.GetComponent<ParticleSystem>();

    }

    /*アップロード*/
        void Update()
    {
        /*時間制限*/
        if (canplay==true)
        {
            GameCurrentTime -= Time.deltaTime;
            TimenumText.text = GameCurrentTime.ToString("f1");
        }
        AnimTime += Time.deltaTime;
        if (AnimTime >= 4.0f){
            //Animation.SetBool("Attack",false);
            AnimTime = 0.0f;
            anim = false;
        }
        /*タイマーゲージのイメージ*/
        if (TimeImage != null)
        {
            TimeImage.fillAmount = GameCurrentTime / G_MaxTime;
        }

        /*MYターン可能時間制限*///ワザゲージのイメージ
        if (canplay == true)
        {
            B_CurrentTime -= Time.deltaTime * ChangeSpeed;
        }
        if (BravewazaImage != null){
            BravewazaImage.fillAmount = B_CurrentTime / B_MaxTime;//ワザゲージのイメージ
        }
        /*MYゲージをリセット*/
        /* if (B_CurrentTime < 0){
            B_CurrentTime = B_MaxTime;
            ResetButtonBool();
            ResetButtonCount();
            ButtonSetting();
        }*/
        if (GameCurrentTime < 0 || B_CurrentHP <1)
        {
            GameOver();
        }
        /*順番通りに押されたら発動！！！！*/
        ButtonSetting();//→外のクラスへ
        /*スピードアップ発動とその効果*/
        if (A_A == true && A_B == true)
        {
            Debug.Log("時間増える");
            GameCurrentTime += 3.0f;
            /*タイマーゲージのイメージ*/
            if (TimeImage != null)
            {
                TimeImage.fillAmount = GameCurrentTime / G_MaxTime;
            }
            if (E_CurrentHP < 1)
            {
                GameEnd(B_CurrentHP, GameCurrentTime);
            }
            particleTM.Play();

            ResetButtonCount();
            ResetButtonBool();
            countAAAB++;
        }
        /*攻撃を発動とその効果*/
        else if (B_B == true && A_B == true)
        {
            E_CurrentHP -= Attackpoint;
            EHPImage.fillAmount = E_CurrentHP / E_MaxHP;
            EnemyHPText.text = E_CurrentHP.ToString();
            Debug.Log("攻撃力"+ Attackpoint);//デバッグ
            //Animation.SetBool("Attack", false);

            ResetButtonBool();
            ResetButtonCount();
            countBBAB++;

            particle.Play();
        }
        /*バイキルトを発動とその効果*/
        else if ( C_C == true && A_B == true)
        {
            Debug.Log("倍攻撃");
            Attackpoint = Attackpoint * 2;
            PowerText.text = Attackpoint.ToString();
            Recoverpoint = Recoverpoint * 1.2f;
            countCCAB++;
            Debug.Log(B_CurrentHP);
            ResetButtonBool();
            ResetButtonCount();

            particleBi.Play();
        }
        /*回復を発動とその効果*/
        else if (B_B == true && C_C == true)
        {
            B_CurrentHP = PlayerPrefs.GetFloat("B_CurrentHP");
            B_CurrentHP += 20.0f;
            if (B_CurrentHP>100.0f)
            {
                B_CurrentHP = 100.0f;
            }
            BHPImage.fillAmount = B_CurrentHP/B_MaxHP;
            PlayerPrefs.SetFloat("B_CurrentHP", B_CurrentHP);
            PlayerPrefs.Save();
            BraveHPText.text = B_CurrentHP.ToString();
            Debug.Log(B_CurrentHP);
            Debug.Log("回復");

            particleR.Play();

            ResetButtonBool();
            countBBCC++;
        }

        /*体力２０以下の時、バトルで一度だけ発動させることができる*/
        else if (A_A == true  && B_B == true) {
            B_CurrentHP = PlayerPrefs.GetFloat("B_CurrentHP");
            if(B_CurrentHP <= 20  && canPlay == true)
            {
                B_CurrentHP = B_MaxHP;
                BHPImage.fillAmount = B_CurrentHP / B_MaxHP;
                PlayerPrefs.SetFloat("B_CurrentHP", B_CurrentHP);
                PlayerPrefs.Save();
                canPlay = false;
                Debug.Log("緊急");
            }
        }
        if (E_CurrentHP < 1)
        {
            particleEX.Play();
            GameEnd(B_CurrentHP, GameCurrentTime);
        }
    }

    /*三つのクリックカウント類*/
    public void OnClickA(){
        A_count++;
        canplay = true;
    }
    public void OnClickB(){
        B_count++;
        canplay = true;
    }
    public void OnClickC(){
        C_count++;
        canplay = true;
    }
    /*ネクストゲーム。シーン移動*/
    void GameEnd(float a, float b){

        if (setHP >= 8000.0f)
        {
            score = a * b * 30;
            PlayerPrefs.SetFloat("SCORE", score);
        }
        else if (setHP < 8000.0f && setHP >= 1000.0f)
        {
            score = a * b * 10;
            PlayerPrefs.SetFloat("SCORE", score);
        }
        else if (setHP < 1000.0f && setHP >= 100.0f)
        {
            score = a * b;
            PlayerPrefs.SetFloat("SCORE", score);
        }
        else
        {
            Debug.Log(score);
        }
        Invoke("Clear", 1);
        Debug.Log("A");

    }
    void Clear()
    {
        SceneManager.LoadScene("ClearScene");
    }
    /*ゲームオーバー。シーン移動*/
    void GameOver() {
        SceneManager.LoadScene("GameOver");
    }

    /*コマンドセットプログラム。*/
    void ButtonSetting()
    {
        if (A_count == 2) {  ResetButtonCount(); A_A = true; }
        if (B_count == 2) {  ResetButtonCount(); B_B = true; }
        if (C_count == 2) {  ResetButtonCount(); C_C = true; }
        if ( A_count == 1 && B_count == 1 ) { A_B = true;  }
    }
    /*コマンドリセット関数*/
    void ResetButtonBool()
    {
         A_A = false;  A_B = false;  C_C = false; B_B = false;
    }
    /*カウントリセット関数*/
    void ResetButtonCount()
    {
        A_count = 0;
        B_count = 0;
        C_count = 0;
    }
    void ResetSettingAnim()
    {
        countBBAB = 0;
        countCCAB = 0;
        countAAAB = 0;
        countBBCC = 0;
    }
}
