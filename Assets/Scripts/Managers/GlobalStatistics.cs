using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalStatistics : StatisticsManager
{
    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
        {
            // Unparent the GameObject to make it a root object
            transform.parent = null;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
