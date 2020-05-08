using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointController : MonoBehaviour
{
    public GameController gameController;

    private Transform[] checkpoints;
    private int maxCheckPoints = 0;
    private int checkpointAtual = 0;
    private int voltaAtual = 0;
    private int maximoVoltas;

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
                        gameController.UpdateVoltas(voltaAtual);
                    }
                }else{
                    checkpointAtual = 0;
                    Debug.Log(voltaAtual);
                }
            }
        }
    }
}
