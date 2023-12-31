using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverArea : MonoBehaviour
{
    public Cover[] covers;

    void Awake()
    {
        covers = GetComponentsInChildren<Cover>();
        if( covers == null )
        {
            Debug.Log("supiri");
        }
        
    }

    public Cover GetRandomCover(Vector3 agentLocation)
    {
        
        int randomIndex = Random.Range(0 , covers.Length);
        return covers[randomIndex];
    }
}
