using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateLevel : MonoBehaviour
{
    public GameObject[] section;
    public int posZ = 44;   //primera posición de generación de la siguiente street
    public bool generaSection = false;
    public int secNum;

    // Update is called once per frame
    void Update()
    {
        if(generaSection == false)
        {
            generaSection = true;
            StartCoroutine(GeneraSection());
        }
    }

    IEnumerator GeneraSection()
    {
        secNum = Random.Range(0, 3);
        //Generamos una seccion num. 4 cada minuto con un 50% de probabilidad
        if(Time.timeSinceLevelLoad % 60 == 0)
        {
            GameObject newSection = Instantiate(section[3], new Vector3(0, 0, posZ), Quaternion.identity);
            newSection.AddComponent<DestroySection>();
        }
        else
        {
            //Generamos una seccion nueva aleatoria en la posicion posZ
            GameObject newSection = Instantiate(section[secNum], new Vector3(0, 0, posZ), Quaternion.identity);
            newSection.AddComponent<DestroySection>();
        }
        posZ += 44;
        yield return new WaitForSeconds(2);
        generaSection = false;
    }
}
