using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    ParticleSystem particle;
    // Start is called before the first frame update
    void Start()
    {
        GameObject obj = GameObject.Find("VenomSpell");
        particle = obj.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            particle.Play();
        }
        if (Input.GetMouseButtonUp(0))
        {
            particle.Stop();
        }
    }
}
