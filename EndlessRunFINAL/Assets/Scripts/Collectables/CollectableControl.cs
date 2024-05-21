using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CollectableControl : MonoBehaviour
{
    public static float Energy = 100.0f;
    public static float MaxEnergy = 100.0f;
    public float energiaRestante;
    public Image energyBar;
    public LevelDistance levelDistance;

    // Start is called before the first frame update
    void Start()
    {
        //Suscribimos el método energyCollected al evento OnEnergyCollected
        CollectEnergy.OnEnergyCollected += () => StartCoroutine(EnergyCollected());
        StartCoroutine(DecreaseEnergy());
    }

    public IEnumerator EnergyCollected()
    {
        if(Energy < MaxEnergy){
            //Recuperamos 10 de energía
            Energy += 10.0f;
            if(Energy > MaxEnergy) Energy = MaxEnergy; //Si la energía supera el máximo, la igualamos al máximo
            energiaRestante = Energy/MaxEnergy; //Calculamos el porcentaje de energía restante
            //Actualizamos la barra de energía
        }else{
            energiaRestante = 1;
        }
        energyBar.transform.localScale = new Vector3(energiaRestante, 1, 1);
        yield return new WaitForSeconds(1);
    }

    // Update is called once per frame
    public IEnumerator DecreaseEnergy()
    {
        while(Energy > 0)
        {
            //Quitamos 1 de energía cada segundos
            Energy -= 1.0f;
            energiaRestante = Energy/MaxEnergy; //Calculamos el porcentaje de energía restante
            energyBar.transform.localScale = new Vector3(energiaRestante, 1, 1); //Actualizamos la barra de energía
            yield return new WaitForSeconds(1);
        }
    }

    public void DamageCollision()
    {
        if(Energy > 0){
            Energy -= 5.0f; //Quitamos energia por chocar
        }else{
            NoMoreEnergy();
        }
    }

    public void NoMoreEnergy()
    {
        Debug.Log("Game Over");
        // Guardar la distancia obtenida en PlayerPrefs
        levelDistance.GameOver();
        SceneManager.LoadScene("PantallaFin");
    }
}
