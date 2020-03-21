using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class BraveControllerScript : MonoBehaviour
{
    public AudioClip clipaudioA;
    public AudioClip clipaudioB;
    public AudioClip clipaudioC;
    AudioSource clipSound;

    int A_buttoncount = 0;
    int B_buttoncount = 0;
    int C_buttoncount = 0;

    string BraveName;
    [SerializeField]
    private Text BraveNameText;

    public GameScript gamescript;
    int num;

    Animator anim;

    void Start()
    {
        BraveName = PlayerPrefs.GetString("bravename");
        BraveNameText.text = BraveName.ToString();

        clipSound = this.gameObject.GetComponent<AudioSource>();

        anim = this.gameObject.GetComponent<Animator>();
        anim.SetBool("Attack", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (A_buttoncount == 1)
        {
            clipSound.clip = clipaudioA;
            clipSound.Play();

            ResetbtCount();
        }
        if (B_buttoncount == 1)
        {
            clipSound.clip = clipaudioB;
            clipSound.Play();

            ResetbtCount();
        }
        if (C_buttoncount == 1)
        {
            clipSound.clip = clipaudioC;
            clipSound.Play();

            ResetbtCount();
        }

        num = gamescript.countBBAB;
        if( num == 1)
        {
            anim.SetBool("Attack",true);
        }
    }
    /*アタックアクション関数*/


    /*カウントAボタン*/
    public void OnClickA()
    {
        A_buttoncount++;
        Debug.Log(A_buttoncount);
    }
    /*カウントBボタン*/
    public void OnClickB()
    {
        B_buttoncount++;
        Debug.Log(B_buttoncount);
    }
    /*カウントCボタン*/
    public void OnClickC()
    {
        C_buttoncount++;
        Debug.Log(C_buttoncount);
    }
    /*ボタンカウントリセット*/
    void ResetbtCount() {
        A_buttoncount = 0;
        B_buttoncount = 0;
        C_buttoncount = 0;
    }
}
