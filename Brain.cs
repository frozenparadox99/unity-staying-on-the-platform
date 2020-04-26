using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain : MonoBehaviour
{

    int DNALength = 2; // 2 bcos we need to make decisions if we can see the platform or we can't see the platform.
    public float timeAlive;
    public DNA dna;
    public GameObject eyes;
    bool alive = true;
    bool seeGround = true;

    private void OnCollisionEnter(Collision obj)
    {
        if(obj.gameObject.tag == "dead")
        {
            alive = false;
        }
    }

    public void Init()
    {
        // initialise DNA
        // 0 forward
        // 1 left
        // 2 right
        dna = new DNA(DNALength, 3);
        timeAlive = 0;
        alive = true;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!alive) return;

        Debug.DrawRay(eyes.transform.position, eyes.transform.forward * 10, Color.red, 10);
        seeGround = false;
        RaycastHit hit;
        if(Physics.Raycast(eyes.transform.position,eyes.transform.forward*10,out hit))
        {
            if(hit.collider.gameObject.tag == "platform")
            {
                seeGround = true;
            }
        }
        timeAlive = PopulationManager.elapsed;

        //read DNA
        float h = 0;
        float v = 0;
        if (seeGround)
        {
            // make v relative to character and always move forward
            if (dna.GetGene(0) == 0) v = 1;
            else if (dna.GetGene(0) == 1) h = -90;
            else if (dna.GetGene(0) == 2) h = 90;
        }
        else
        {
            if (dna.GetGene(1) == 0) v = 1;
            else if (dna.GetGene(1) == 1) h = -90;
            else if (dna.GetGene(1) == 2) h = 90;
        }
        this.transform.Translate(0, 0, v * 0.1f);
        this.transform.Rotate(0, h, 0);
    }
}
