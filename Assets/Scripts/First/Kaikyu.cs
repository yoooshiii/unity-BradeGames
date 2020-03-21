using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Kaikyu : MonoBehaviour
{
    public GameObject KaikyuCanvase;
    private float sethp;
    bool canPlayEasy = false;
    bool canPlayStand = false;


    public void OnclickHighLevel()
    {
        sethp = PlayerPrefs.GetFloat("sethp");
        if(sethp == 1000.0f)
        {
            sethp = 8000.0f;
            PlayerPrefs.SetFloat("sethp", sethp);
            PlayerPrefs.Save();
            Debug.Log(sethp);
            KaikyuCanvase.SetActive(false);
        }
    }
    public void OnclickStandardLevel()
    {
        sethp = PlayerPrefs.GetFloat("sethp");
        if (sethp == 100.0f)
        {
            sethp = 1000.0f;
            PlayerPrefs.SetFloat("sethp", sethp);
            PlayerPrefs.Save();
            canPlayStand = true;
            Debug.Log(sethp);
            KaikyuCanvase.SetActive(false);
        }
    }
    public void OnclickEasyLevel()
    {
        sethp = 100.0f;
        PlayerPrefs.SetFloat("sethp", sethp);
        PlayerPrefs.Save();
        canPlayEasy = true;
        Debug.Log(sethp);
        KaikyuCanvase.SetActive(false);
    }
}
