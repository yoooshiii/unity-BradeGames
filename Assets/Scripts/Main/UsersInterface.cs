using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsersInterface : MonoBehaviour
{

    int countbt = 0;
    private AudioSource audioSource;
    // Update is called once per frame
    void Update()
    {   
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void Onclickbt()
    {
        countbt++;
    }
    public void OncilckSetting()
    {
        this.gameObject.SetActive(true);
        Time.timeScale = 0;
       // audioSource.Volume = 0.5;

    }
    public void OncilckSettingF()
    {
        this.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
    public void OncilckPause()
    {

    }
}
