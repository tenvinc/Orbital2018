using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FloatingCDText : MonoBehaviour {

    public float DestroyTime;
    public Vector3 Offset;
    public Vector3 RandomizeIntensity = new Vector3(0.5f, 0, 0);

    public Transform enemyRef;

    void Start()
    {
        InvokeRepeating("Count", 0.0f, 1.0f);

        Destroy(gameObject, DestroyTime);

        transform.localPosition += Offset;
        
        transform.localPosition += new Vector3(Random.Range(-RandomizeIntensity.x, RandomizeIntensity.x),
        Random.Range(-RandomizeIntensity.y, RandomizeIntensity.y),
        Random.Range(-RandomizeIntensity.z, RandomizeIntensity.z));
        
    }

    void Update()
    {
        GetComponent<TextMeshPro>().text = DestroyTime.ToString();
    }

    void Count ()
    {
        if ((DestroyTime == 0) || (enemyRef.GetComponent<Enemy>().isDead))
        {
            CancelInvoke("Count");
        } else
        {
            DestroyTime--;
        }
    }

}
