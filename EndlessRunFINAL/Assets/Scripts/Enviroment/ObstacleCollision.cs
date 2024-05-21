using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ObstacleCollision : MonoBehaviour
{
    public GameObject Player;
    public GameObject characterModel;
    public CollectableControl collectableControl;
    
    private PlayerAnimation playerAnimation;
    private bool isVulnerable = false;

    private void Start()
    {
        playerAnimation = Player.GetComponent<PlayerAnimation>();
    }

    void OnTriggerEnter(Collider other)
    {
        //Si ha sido golpeado, es inmune a volverse a golpear
        if(isVulnerable) return;

        if(this.gameObject.CompareTag("minijuego"))
        {
            Debug.Log("Minijuego activado");
            LoadMinigame();
        }
        else if(this.gameObject.CompareTag("obstaculo"))
        {
            Debug.Log("Obstaculo golpeado");
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
            playerAnimation.Collision();

            collectableControl.DamageCollision();
            this.gameObject.GetComponent<BoxCollider>().enabled = true;
            StartCoroutine(BlinkCharacterModel());
        }else{
            Debug.Log("Error: No se ha encontrado el tag");
        }
    }

    //Parpadeo de invulnerabilidad
    private IEnumerator BlinkCharacterModel()
    {
        isVulnerable = true;
        float endTime = Time.time + 2;
        //Player.GetComponent<Collider>().enabled = false;
        Renderer render = characterModel.GetComponent<Renderer>();
        while(Time.time < endTime)
        {
            render.enabled = false;
            yield return new WaitForSeconds(0.2f);
            render.enabled = true;
            yield return new WaitForSeconds(0.2f);
        }
        //Player.GetComponent<Collider>().enabled = true;
        isVulnerable = false;
    }

    //Aqui se carga la escena del minijuego guardando la escena actual
    public void LoadMinigame()
    {
        Time.timeScale = 0f;
        SceneManager.LoadScene("Minijuego", LoadSceneMode.Additive);
    }
}
