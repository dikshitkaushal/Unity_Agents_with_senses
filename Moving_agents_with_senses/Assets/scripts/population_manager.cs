using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using JetBrains.Annotations;

public class population_manager : MonoBehaviour
{
    public GameObject bot;
    public int populationsize=50;
    public static float elapsed = 0;
    public float trialtime = 5f;
    List<GameObject> population = new List<GameObject>();
    int generation = 1;

    GUIStyle guistyle = new GUIStyle();
    private void OnGUI()
    {
        guistyle.fontSize = 25;
        guistyle.normal.textColor = Color.white;
        GUI.BeginGroup(new Rect(10,10,250,150));
        GUI.Box(new Rect(0, 0, 140, 140), "Stats", guistyle);
        GUI.Label(new Rect(10, 25, 200, 30), "Gen" + generation, guistyle);
        GUI.Label(new Rect(10, 50, 200, 30), string.Format("Time : {0:0:00}", elapsed), guistyle);
        GUI.Label(new Rect(10, 75, 200, 30), "Population" + population.Count, guistyle);
        GUI.EndGroup();
    }
    // Start is called before the first frame update
    void Start()
    {
        for(int i=0;i<populationsize;i++)
        {
            Vector3 pos = new Vector3(this.transform.position.x + UnityEngine.Random.Range(-2, 2), this.transform.position.y, this.transform.position.z + UnityEngine.Random.Range(-2, 2));
            GameObject agent = Instantiate(bot, pos, this.transform.rotation);
            agent.GetComponent<brain>().init();
            population.Add(agent);
        }
    }
    public void breednewpopulation()
    {
        List<GameObject> sortedlist = population.OrderBy(o => (o.GetComponent<brain>().walkalive*5 + o.GetComponent<brain>().timealive)).ToList();
        population.Clear();
        for(int i=(int)(sortedlist.Count/2)-1;i<sortedlist.Count-1;i++)
        {
            population.Add(breed(sortedlist[i], sortedlist[i + 1]));
            population.Add(breed(sortedlist[i + 1], sortedlist[i]));
        }
        //destroy all parents and previous population
        for(int i=0;i<population.Count;i++)
        {
            Destroy(sortedlist[i]);
        }
        generation++;
    }
    public GameObject breed(GameObject parent1, GameObject parent2)
    {
        Vector3 pos = new Vector3(this.transform.position.x + UnityEngine.Random.Range(-2, 2), this.transform.position.y, this.transform.position.z + UnityEngine.Random.Range(-2, 2));
        GameObject offspring = Instantiate(bot, pos, this.transform.rotation);
        brain b = offspring.GetComponent<brain>();
        if(UnityEngine.Random.Range(0,100)==1)
        {
            b.init();
            b.dna.mutate();
        }
        else
        {
            b.init();
            b.dna.combine(parent1.GetComponent<brain>().dna, parent2.GetComponent<brain>().dna);
        }
        
        return offspring;
    }
    // Update is called once per frame
    void Update()
    {
        elapsed += Time.deltaTime;
        if(elapsed>trialtime)
        {
            breednewpopulation();
            elapsed = 0;
        }
    }
}
