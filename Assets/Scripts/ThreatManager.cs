using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreatManager : MonoBehaviour
{
    public static ThreatManager instance;

    public float StartSpawnTime = 5f;
    public float FastestSpawnTime = 0.4f;
    public float SpawnTimeScaler = 0.95f;
    public Threat[] Threats;

    public float ThreatSignalTime = 2f;

    Rect spawnRect;

    List<Threat> inactiveThreats;
    List<Threat> activeThreats;


    List<GameObject> spawnedObjects;

    float timeToNextThreat = 1f;
    int timeSinceStart = 0;

    void Start()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

    }


    public void StartSpawner()
    {
        inactiveThreats = new List<Threat>(Threats);
        activeThreats = new List<Threat>();

        Vector2 botLeft = (Vector2)Camera.main.ViewportToWorldPoint(new Vector2(0, 0)) - Vector2.one / 2f;
        Vector2 topRight = (Vector2)Camera.main.ViewportToWorldPoint(new Vector2(1, 1)) + Vector2.one / 2f;
        spawnRect = new Rect(botLeft, topRight - botLeft);

        timeToNextThreat = StartSpawnTime;
        timeSinceStart = 0;
        spawnedObjects = new List<GameObject>();

        StartCoroutine(NewThreats());
        StartCoroutine(Spawner());
    }


    public void Cleanup()
    {
        for(int i = spawnedObjects.Count - 1; i >= 0; i--)
        {
            DestroyThreatObject(spawnedObjects[i]);
        }
        spawnedObjects.Clear();
    }

    IEnumerator NewThreats()
    {
        while(GameManager.instance.IsPlaying())
        {
            yield return new WaitForSeconds(1f);
            if (GameManager.instance.IsPlaying())
            {
                timeSinceStart += 1;
                foreach (Threat t in inactiveThreats)
                {
                    if (t.TimeToSpawn <= timeSinceStart)
                    {
                        activeThreats.Add(t);
                        inactiveThreats.Remove(t);
                        break;
                    }
                }
            }
        }        
    }
    
    IEnumerator Spawner()
    {
        while(GameManager.instance.IsPlaying())
        {
            yield return new WaitForSeconds(timeToNextThreat);
            if(GameManager.instance.IsPlaying())
            {
                if (SpawnSignal())
                {
                    if (timeToNextThreat > FastestSpawnTime)
                    {
                        timeToNextThreat *= SpawnTimeScaler;
                    }
                }
            }
        }
    }

    bool SpawnSignal()
    {
        if (activeThreats.Count == 0) return false;
        int i = Random.Range(0, activeThreats.Count);
        Threat threat = activeThreats[i];

        int p = Random.Range(0, threat.Positions.Length);
        float angle = Random.Range(threat.Positions[p].Angle - threat.Positions[p].AngleDiff, threat.Positions[p].Angle + threat.Positions[p].AngleDiff);
        GameObject signal = Instantiate(threat.SignalObject, spawnRect.position + Vector2.Scale(threat.Positions[p].Position, spawnRect.size), Quaternion.Euler(new Vector3(0, 0, angle)));
        spawnedObjects.Add(signal);

        StartCoroutine(SpawnThreat(signal, threat.ThreatObject));
        return true;
    }

    IEnumerator SpawnThreat(GameObject signal, GameObject threatObj)
    {
        yield return new WaitForSeconds(ThreatSignalTime);
        if (GameManager.instance.IsPlaying())
        {
            GameObject to = Instantiate(threatObj, signal.transform.position, signal.transform.rotation);
            spawnedObjects.Add(to);
            DestroyThreatObject(signal);
        }
    }
    
    public void DestroyThreatObject(GameObject obj)
    {
        spawnedObjects.Remove(obj);
        Destroy(obj);
    }
}
