using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalFlock : MonoBehaviour
{
    public GameObject boidPrefab;
    public GameObject goalPrefab;

    public static int fieldSize = 20; 

    static int numBoids = 100;
    public static GameObject[] allBoids = new GameObject[numBoids];

    public static Vector3 goalPosition = Vector3.zero;
    GameObject boid;

    [Range(1, 50)]
    public float neighboor = 4.0f;

    // Start is called before the first frame update
    void Start()
    {
        for (int i=0; i<numBoids; i++)
        {
            //va créer une barrière autour des boids, dans ce cas ci de (10,10,10) à partir du point d'origine
            Vector3 position = new Vector3(Random.Range(-fieldSize, fieldSize),
                                           Random.Range(-fieldSize, fieldSize),
                                           Random.Range(-fieldSize, fieldSize));
            //crée les prefabs et les place dans la scène de façon random
            allBoids[i] = (GameObject)Instantiate(boidPrefab, position, Quaternion.identity);   
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Déplace la sphère "Goal" dans le périmètre du field en random
        if (Random.Range (0, 1000) < 5)
        {
            goalPosition = new Vector3(Random.Range(-fieldSize, fieldSize),
                                       Random.Range(-fieldSize, fieldSize),
                                       Random.Range(-fieldSize, fieldSize));
            goalPrefab.transform.position = goalPosition;
        }

        Boid boid = GetComponent<Boid>();
        boid.neighboorDistance = neighboor;
    }
}
