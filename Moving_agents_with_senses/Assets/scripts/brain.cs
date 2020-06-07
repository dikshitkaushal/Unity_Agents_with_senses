using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brain : MonoBehaviour
{
    public float timealive;
    public float walkalive;
    int dnalength = 2;
    public DNA dna;
    public GameObject eyes;
    public GameObject ethan_prefab;
    GameObject ethan;
    public bool isalive;
    bool seeground;

    private void OnDestroy()
    {
        Destroy(ethan);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="dead")
        {
            isalive = false;
            timealive = 0;
            walkalive = 0;
            Destroy(ethan);
        }
    }

    public void init()
    {
        //0 forward
        //1 left 
        //2 right
        isalive = true;
        dna = new DNA(dnalength, 3);
        ethan = Instantiate(ethan_prefab, this.transform.position, this.transform.rotation);
        ethan.GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControl>().target = this.transform;
        seeground = true;

    }

    // Update is called once per frame
    void Update()
    {
        if(!isalive)
        {
            return;
        }
        Debug.Log("dead");
        Debug.DrawRay(eyes.transform.position, eyes.transform.forward * 10, new Color(1, 0, 0), 10);
        seeground = false;
        RaycastHit hit;
        if(Physics.Raycast(eyes.transform.position,eyes.transform.forward*10,out hit))
        {
            if(hit.transform.tag=="platform")
            {
                seeground = true;
            }
        }
        timealive = population_manager.elapsed;
        //read dna 
        int v = 0;
        int h = 0;
        if(seeground)
        {
            if(dna.getgene(0)==0)
            {
                v = 1;
                walkalive++;
            }
            else if(dna.getgene(0)==1)
            {
                h = 90;
            }
            else if(dna.getgene(0)==2)
            {
                h = -90;
            }
        }
        else
        {
            if (dna.getgene(1) == 0)
            {
                v = 1;
                walkalive++;
            }
            else if (dna.getgene(1) == 1)
            {
                h = 90;
            }
            else if (dna.getgene(1) == 2)
            {
                h = -90;
            }
        }
        this.transform.Translate(0, 0, v * 0.1f);
        this.transform.Rotate(0, h, 0);
    }
}
