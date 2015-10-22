using UnityEngine;
using System.Collections;

public class SphereEvent : MonoBehaviour
{
    float time;
    public int sphereNum; 

    void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "Player")
        {
            SphereSpawn.Score += 50;
            SphereSpawn.curCountOfSphere--;
            SphereSpawn.blockedPos.Remove(sphereNum);
            Destroy(gameObject);
        }
    }

    void Update()
    {
        time += Time.deltaTime;
        if (time >= 5)
        {
            Destroy(gameObject);
            SphereSpawn.curCountOfSphere--;
            SphereSpawn.blockedPos.Remove(sphereNum);
        }
    }
}
