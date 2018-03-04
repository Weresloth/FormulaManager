using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectKiller : MonoBehaviour {
	
	void Start () {

        StartCoroutine(killObject());

        }

    IEnumerator killObject()
    {

        if (gameObject.tag == "Smoke")
        {
            yield return new WaitForSeconds(5f);
            DestroyObject(gameObject);

        } else
        {
            yield return new WaitForSeconds(2f);
            DestroyObject(gameObject);

        }
        

    }

}
