using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelDistance : MonoBehaviour
{
    public GameObject displayDistance;
    public int distance;
    public static int RecordDistance;
    private int aumentoDistancia = 1;
    public bool addingDistance = false;

    // Update is called once per frame
    void Update()
    {
        if(!addingDistance)
        {
            addingDistance = true;
            StartCoroutine(AddDis());
        }
    }

    public IEnumerator AddDis()
    {
        distance += aumentoDistancia;
        displayDistance.GetComponent<Text>().text = distance + " m.";
        yield return new WaitForSeconds(2);
        addingDistance = false;
    }

    public void GameOver()
    {
        int newRecord = PlayerPrefs.GetInt("RecordDistance", 0);
        if(distance > newRecord)
        {
            PlayerPrefs.SetInt("RecordDistance", distance);
        }
    }
}
