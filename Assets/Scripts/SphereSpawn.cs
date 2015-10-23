using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SphereSpawn : MonoBehaviour
{
    [SerializeField]
    GameObject Sphere, Cursor;
    [SerializeField]
    List<Transform> spawnPos = new List<Transform>();
    public static List<int> blockedPos = new List<int>();
    int randSpawnPos;

    int maxCountOfSphere;
    public static int curCountOfSphere;

    public static int Score;
    public Text ScoreText;

    GameObject ob, ob2;

    void Start()
    {
        maxCountOfSphere = 1;
    }

    void Update()
    {
        sphereSpawn();
        ScoreText.text = "Score: " + Score;
    }

    void sphereSpawn()
    {
        bool hz = true;
        if (curCountOfSphere < maxCountOfSphere)
        {
            if (blockedPos.Count == 0)
            {
                randSpawnPos = Random.Range(0, spawnPos.Count - 1);
            }
            else
            {
                randSpawnPos = Random.Range(0, spawnPos.Count - 1);
                for (int i = 0; i < blockedPos.Count; i++)
                {
                    if (randSpawnPos != blockedPos[i])
                    {
                        hz = true;
                    }
                    else
                    {
                        hz = false;
                        break;
                    }
                }
            }
            if (hz == true)
            {
                ob = (GameObject)Instantiate(Sphere, spawnPos[randSpawnPos].position + Vector3.up, Quaternion.identity);
                curCountOfSphere++;
                blockedPos.Add(randSpawnPos);
                ob.GetComponent<SphereEvent>().sphereNum = randSpawnPos;
            }
        }
    }
}
