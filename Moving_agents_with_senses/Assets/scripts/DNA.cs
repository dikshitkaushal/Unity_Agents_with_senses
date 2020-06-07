using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNA 
{
    public List<int> gene = new List<int>();
    int dnalength = 0;
    int maxvalues = 0;

    public DNA(int l,int v)
    {
        dnalength = l;
        maxvalues = v;
        setrandom();
    }
    
    public void setrandom()
    {
        for(int i=0;i<dnalength;i++)
        {
            gene.Add(UnityEngine.Random.Range(0, maxvalues));
        }
    }
    
    public int getgene(int pos)
    {
        return gene[pos];
    }

    public void setgene(int pos ,int value)
    {
        gene[pos] = value;
    }
    public void combine(DNA dna1 ,DNA dna2)
    {
        for(int i=0;i<dnalength;i++)
        {
            if(i<dnalength/2.0f)
            {
                gene[i] = dna1.getgene(i);
            }
            else
            {
                gene[i] = dna2.getgene(i);
            }
        }
    }
    public void mutate()
    {
        gene[UnityEngine.Random.Range(0, dnalength)] = UnityEngine.Random.Range(0, maxvalues);
    }
}
