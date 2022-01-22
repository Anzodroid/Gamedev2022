using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // ####################################the code below is from 80% section and no longer used ############################
    private Animator anim;
    private bool isAlive; 

    void Start()
    {
        anim = GetComponent<Animator>();
/*      anim.SetBool("up", false);
        anim.SetBool("down", false);
        anim.SetBool("left", true);
        anim.SetBool("right", false);*/
        isAlive = true;
    }

    void Update()
    {

        //if (GameManagement.Scared == true)
           if (GameManagement.Scared == true && isAlive == true)
            {
            anim.SetBool("up", false);
            anim.SetBool("down", false);
            anim.SetBool("left", false);
            anim.SetBool("right", false);
            anim.SetBool("isScared", true);
            Invoke("Recovery",7f);
        }
       // Debug.Log("GM Scared" + GameManagement.Scared + " || Animations : anim UP :" + anim.GetBool("up") + " anim RIGHT :" + anim.GetBool("right") + " anim DOWN :" + anim.GetBool("down") + " anim LEFT :" + anim.GetBool("left") + " || scared " + anim.GetBool("isScared") + " is recovery "+anim.GetBool("isRecovery") + " is DEad" + anim.GetBool("isDead"));
    }

    /*    private void OnTriggerEnter(Collider trigger) Removed from 80% Section
        {
            if (GameManagement.Scared == true)
            {
                isAlive = false;
                anim.SetBool("isScared", false);
                anim.SetBool("isRecovery", false);
                anim.SetBool("isDead", true);
                AudioController.GhostDead = true;
                GameManagement.Score += 300;
              //  StartCoroutine(EnemyDead()); // Removed from 80% Section
            }
        }

        IEnumerator EnemyDead() // only used for 80% Section 
        {
            CancelInvoke("Recovery");
            CancelInvoke("NormalState");

            anim.SetBool("isScared", false);
            anim.SetBool("isRecovery", false);
            yield return new WaitForSeconds(5);
            AudioController.GhostDead = false;
            isAlive = true;
            anim.SetBool("isDead", false);
            anim.SetBool("up", true);
        }*/

    void Recovery()
    {
        anim.SetBool("isRecovery", true);
        anim.SetBool("isScared", false);
        Invoke("NormalState", 3f);
    }

    void NormalState()
    {
        anim.SetBool("isRecovery", false);
        anim.SetBool("isDead", false);
        anim.SetBool("up", true);
    }
}
