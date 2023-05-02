using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    private const float PLAYER_DISTANCE_SPAWN_LEVEL_PART = 20f;
    [SerializeField] private Transform levelPart_Start;
    [SerializeField] private List<Transform> levelPartList;
    [SerializeField] private PlayerMovement player;

    public Vector3 lastEndPosition;
    private Vector3 deletedPlatformPosition;

    private void Awake(){

        lastEndPosition = levelPart_Start.Find("EndPosition").position;

        int startingSpawnLevelParts = 1;
        for(int i = 0; i < startingSpawnLevelParts; i++){
            SpawnLevelPart();
        }  
    }

    private void Update(){
        if(Vector3.Distance(player.GetComponent<Transform>().position, lastEndPosition) < PLAYER_DISTANCE_SPAWN_LEVEL_PART){
            SpawnLevelPart();
        }
        /*
        Debug.Log(Vector3.Distance(player.GetComponent<Transform>().position, deletedPlatformPosition));
        if(Vector3.Distance(player.GetComponent<Transform>().position, deletedPlatformPosition) > 19){
            Destroy(GameObject.FindGameObjectWithTag("levelPart"));
        }
        */
    }


    public void SpawnLevelPart(){
        Transform chosenLevelPart = levelPartList[Random.Range(0, levelPartList.Count)];
        Transform lastLevelPartTransform = SpawnLevelPart(chosenLevelPart, lastEndPosition);
        //deletedPlatformPosition = lastEndPosition;
        lastEndPosition = lastLevelPartTransform.Find("EndPosition").position;
    }


    private Transform SpawnLevelPart(Transform levelPart, Vector3 spawnPosition){
        Transform levelPartTransform = Instantiate(levelPart, spawnPosition, Quaternion.identity);
        return levelPartTransform;
    }


}
