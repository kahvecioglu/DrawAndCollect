using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatinAlindiFalse : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animator;
    public void SatinalindiKapat()
    {
        animator.SetBool("SatinAlindiIki", false);


    }
}
