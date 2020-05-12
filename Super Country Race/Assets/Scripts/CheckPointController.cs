using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointController : MonoBehaviour
{
    public GameController gameController;

    private Transform[] checkpoints;
    public GameObject lock1, lock2;
    private int maxCheckPoints = 0;
    private int checkpointAtual = 0;
    private int voltaAtual = 0;
    private int maximoVoltas;
    [SerializeField] private int id = 0;

    private void Awake(){
        GameObject checkpointContainer = GameObject.FindGameObjectWithTag("CheckpointsContainer");

        checkpoints = new Transform[checkpointContainer.transform.childCount];
        for(int i = 0; i < checkpointContainer.transform.childCount; i++){
            checkpoints[i] = checkpointContainer.transform.GetChild(i);
        }
    }

    void Start(){
        maximoVoltas = gameController.maximoVoltas;
        maxCheckPoints = checkpoints.Length - 1;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Checkpoint")){
            Debug.Log(voltaAtual);
            if (other.transform == checkpoints[checkpointAtual]){
                if(checkpointAtual == 0){
                    if(voltaAtual >= maximoVoltas){
                        gameController.FinishRace(gameObject);
                    }else{
                        voltaAtual++;
                        Debug.Log(voltaAtual);
                        if (id == 0)
                        {
                            gameController.UpdateVoltasP1(voltaAtual);
                        }
                        else
                        {
                            gameController.UpdateVoltasP2(voltaAtual);
                        }
                        
                    }
                }else{
                    checkpointAtual = 0;
                    Debug.Log(voltaAtual);
                }
            }
        }

        if (other.gameObject.CompareTag("Unlock"))
        {
            Debug.Log("Unlock");
            if (id == 0)
            {
                lock1.SetActive(false);
            }
            else
            {
                lock2.SetActive(false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
            if (id == 0)
            {
                lock1.SetActive(true);
            }
            else
            {
                lock2.SetActive(true);
            }
    }
}
