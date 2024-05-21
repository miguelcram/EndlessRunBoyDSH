using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySection : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            //Debug.Log("Player salio de la seccion");
            StartCoroutine(DestroyAfterTime());
        }
    }

    private IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
