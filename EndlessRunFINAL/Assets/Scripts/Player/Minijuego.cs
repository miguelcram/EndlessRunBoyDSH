 using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using uLipSync;

public class Minijuego : MonoBehaviour
{
    public Text preguntaText;
    public Button[] opcionesRespuestas;
    public Canvas minijuegoUI;

    public GameObject lipSyncComp;
    private uLipSyncBakedDataPlayer bakedGranny;

    private int respuestaCorrecta;

    void Awake()
    {
        minijuegoUI.enabled = false;
        // Comprueba si ya existe un EventSystem en la escena
        var eventSystems = FindObjectsOfType<EventSystem>();
        if (eventSystems.Length > 1)
        {
            // Si ya existe, desactiva este
            for (int i = 1; i < eventSystems.Length; i++)
            {
                eventSystems[i].gameObject.SetActive(false);
            }
        }
    }

    IEnumerator Start()
    {        
        bakedGranny = lipSyncComp.GetComponent<uLipSyncBakedDataPlayer>();
        bakedGranny.Play();
        yield return new WaitForSeconds(7.8f);
        bakedGranny.Stop();

        minijuegoUI.enabled = true;
        for(int i = 0; i < opcionesRespuestas.Length; i++)
        {
            int index = i;
            opcionesRespuestas[i].onClick.AddListener(() => CheckAnswer(index)); //Comprobamos la respuesta
            Debug.Log("asignado boton " + i);
        }
        GenerateQuestion();
    }

    public void GenerateQuestion()
    {
        System.Random rand = new System.Random();
        int operand1 = rand.Next(1, 40);
        int operand2 = rand.Next(1, 40);
        int operation = rand.Next(0, 4);    // 0: +, 1: -, 2: *, 3: /

        int respuesta = 0;
        string pregunta = "";

        switch(operation)
        {
            case 0:
                respuesta = operand1 + operand2;
                pregunta = $"¿Cuanto es {operand1} + {operand2}?";
                break;
            case 1:
                respuesta = operand1 - operand2;
                pregunta = $"¿Cuanto es {operand1} - {operand2}?";
                break;
            case 2:
                respuesta = operand1 * operand2;
                pregunta = $"¿Cuanto es {operand1} * {operand2}?";
                break;
            case 3:
                //Por si el segundo operando es mayor que el primero
                if(operand2 > operand1){
                    int temp = operand1;
                    operand1 = operand2;
                    operand2 = temp;
                }
                respuesta = operand1 / operand2;
                pregunta = $"¿Cuanto es {operand1} / {operand2}?";
                break;
        }

        preguntaText.text = pregunta;
        respuestaCorrecta = rand.Next(0, 4);
        opcionesRespuestas[respuestaCorrecta].GetComponentInChildren<Text>().text = respuesta.ToString();

        for(int i = 0; i < opcionesRespuestas.Length; i++)
        {
            //Se generan las otras respuestas que son incorrectas
            if(i != respuestaCorrecta)
            {
                int respuestaIncorrecta;
                do{
                    respuestaIncorrecta = rand.Next(respuesta-10, respuesta+10);
                }while(respuestaIncorrecta == respuesta);
                opcionesRespuestas[i].GetComponentInChildren<Text>().text = respuestaIncorrecta.ToString();
            }
        }
    }

    public void CheckAnswer(int respuesta)
    {
        Debug.Log("Comprueba la respuesta seleccionada");
        if(respuesta == respuestaCorrecta)
        {
            Debug.Log("Respuesta correcta");
            if(CollectableControl.Energy < CollectableControl.MaxEnergy)
            {
                CollectableControl.Energy += 20.0f;
            }
        }
        else
        {
            Debug.Log("Respuesta incorrecta. Lo siento");
        }
        
        try{
            SceneManager.UnloadSceneAsync("Minijuego");
        }catch (ArgumentOutOfRangeException e)
        {
            Debug.LogError("Se produjo un error al intentar descargar la escena del minijuego: " + e.Message);
        }
        Time.timeScale = 1f;
    }
}