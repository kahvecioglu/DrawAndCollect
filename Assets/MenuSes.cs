using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSes : MonoBehaviour
{

    private static GameObject instance;
   
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        if(instance == null)
        {
            instance = gameObject;

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
