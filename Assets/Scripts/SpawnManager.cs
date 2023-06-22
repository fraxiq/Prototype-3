using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obsticales;
    private Vector3 spawnPos = new Vector3(25, 0, 0);
    private float startDelay = 2;
    private float repeatRate = 2;
    private PlayerController PlayerControllerScript;
    private int prefabIndex;
    // Start is called before the first frame update
    void Start()
    {
        PlayerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        InvokeRepeating("SpawnObsticale", startDelay, repeatRate);
    }

    // Update is called once per frame
    void Update()
    {
        prefabIndex = Random.Range(0, 4);
    }
    void SpawnObsticale()
    {
        if (PlayerControllerScript.gameOver==false)
        {
            Instantiate(obsticales[prefabIndex], spawnPos, obsticales[prefabIndex].transform.rotation);
        }
    }
}
