using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class EnemyControllerScript : MonoBehaviour
{
    Animator anim;
    int countA, countB, countC;

    public Image BHPImage;
    /*勇者の体力*//*攻撃で勇者の体力が減る事*/
    float B_MaxHP = 100.0f; float B_CurrentHP;

    string EnemyName;
    [SerializeField]
    private Text EnemyNameText;

    public float TimeX;//何秒に一回（小）
    public float TimeY;//何秒に一回（大）
    float TimeZ = 5.0f;
    private float TimeXElapsed;//時間を測る（小）
    private float TimeYElapsed;//時間を測る（大）
    private float TimeZElapsed;
    private float TimeZZElapsed;

    [SerializeField]
    private Text BraveHPText;
    bool s = true;
    bool ss = true;
    bool canplay = false;

    // Start is called before the first frame update
    void Start()
    {
        EnemyName = PlayerPrefs.GetString("enemyname");
        EnemyNameText.text = EnemyName;

        /*アニメーション類*/
        anim = this.gameObject.GetComponent<Animator>();
        anim.SetBool("AttacK", false);
        anim.SetBool("Basic Attack", false);
    }
    // Update is called once per frame
    void Update()
    {
        //anim.SetBool("Basic Attack",false);
        if (canplay==true)
        {

            TimeXElapsed += Time.deltaTime;
            TimeYElapsed += Time.deltaTime;
            TimeZElapsed += Time.deltaTime;
            TimeZZElapsed += Time.deltaTime;

        }
        /*一定時間ごとに処理*/
        /*敵が一定時間あたりに攻撃*/

        if (TimeXElapsed >= TimeX)
        {
            B_CurrentHP = PlayerPrefs.GetFloat("B_CurrentHP");
            B_CurrentHP -= 11.0f;
            BHPImage.fillAmount = B_CurrentHP / B_MaxHP;

            BraveHPText.text = B_CurrentHP.ToString();
            PlayerPrefs.SetFloat("B_CurrentHP", B_CurrentHP);
            PlayerPrefs.Save();
            Debug.Log(B_CurrentHP);
            //anim.SetTrigger("Attack");

            if (B_CurrentHP < 1)
            {
                SceneManager.LoadScene("GameOver");
            }
            TimeXElapsed = 0.0f;
        }
        if (TimeYElapsed >= TimeY)
        {
            B_CurrentHP = PlayerPrefs.GetFloat("B_CurrentHP");
            B_CurrentHP -= 30.0f;
            BHPImage.fillAmount = B_CurrentHP / B_MaxHP;
            BraveHPText.text = B_CurrentHP.ToString();
            PlayerPrefs.SetFloat("B_CurrentHP", B_CurrentHP);
            PlayerPrefs.Save();
            Debug.Log(B_CurrentHP);
            if (B_CurrentHP < 1)
            {
                SceneManager.LoadScene("GameOver");
            }
            TimeYElapsed = 0.0f;
        }

        if (TimeZElapsed >= 4 && s == true)
        {
            anim.SetBool("AttacK", true);
        }
        if (TimeZElapsed >= 6 && s == true)
        {
            anim.SetBool("AttacK", false);
            TimeZElapsed = 0.0f;
            s = false;
        }
        if (TimeZElapsed >= 2.9 && s == false)
        {
            anim.SetBool("AttacK", true);
        }
        if (TimeZElapsed >= 4.9 && s == false)
        {
            anim.SetBool("AttacK", false);
            TimeZElapsed = 0.0f;
        }
        if(TimeZZElapsed >= 15.2)
        {
            anim.SetBool("Basic Attack", true);
        }
        if (TimeZZElapsed >= 16.2)
        {
            anim.SetBool("Basic Attack", false);
            TimeZZElapsed = 0.0f;
        }
    }
    public void btn()
    {
        canplay = true;
    }
}