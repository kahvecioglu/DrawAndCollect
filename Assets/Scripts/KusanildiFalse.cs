using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KusanildiFalse : MonoBehaviour
{ 
    public Animator animator;
    public void KusanildiKapat()
    {
       animator.SetBool("KusanildiIki", false);


    }
}
