using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewButton : MonoBehaviour
{
    public GameObject nameSettingcanvas;
    public GameObject RoolText1;
    public GameObject RoolText2;
    public InputField braveinputField;
    public InputField enemyinputField;
    string braveName;
    string enemyName;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnclickNS()
    {
        braveName = braveinputField.text;
        enemyName = enemyinputField.text;
        Debug.Log(braveName);
        PlayerPrefs.SetString("bravename", braveName);
        Debug.Log(enemyName);
        PlayerPrefs.SetString("enemyname", enemyName);
        if (braveName.Length != 0 && enemyName.Length != 0)
        {
            /*キャンバスを消す*/
            nameSettingcanvas.SetActive(false);
        }
    }
    public void OnclickRT()
    {
        if (PlayerPrefs.HasKey("bravename")&& PlayerPrefs.HasKey("enemyname"))
        {
            nameSettingcanvas.SetActive(false);
        }
        if (braveName.Length != 0 && enemyName.Length != 0)
        {
            nameSettingcanvas.SetActive(false);
        }
    }
    public void OnclickRT1()
    {
        RoolText1.SetActive(false);
    }
    public void OnclickRT2()
    {
        RoolText2.SetActive(false);
    }

}
