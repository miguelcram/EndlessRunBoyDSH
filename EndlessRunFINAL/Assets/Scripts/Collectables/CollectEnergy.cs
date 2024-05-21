using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollectEnergy : MonoBehaviour
{
    public AudioSource energyFX;
    public delegate void EnergyCollectedHandler();
    public static event EnergyCollectedHandler OnEnergyCollected;
    
    void OnTriggerEnter(Collider other)
    {
        energyFX.Play();
        //CollectableControl.energyCount += 1;
        //Invocamos para recuperara energia en EnergyPlayerController
        OnEnergyCollected?.Invoke();
        this.gameObject.SetActive(false);
    }
}
