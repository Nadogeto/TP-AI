using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    public float speed = 0.5f;
    float rotationSpeed = 3.0f;

    //Vector3 averageHeading;
    //Vector3 averagePosition;

    float neighboorDistance = 4.0f;

    bool turning = false;


    // Start is called before the first frame update
    void Start()
    {
        //Randomize la vitesse
        speed = Random.Range(0.5f, 1);
    }

    // Update is called once per frame
    void Update()
    {
        //test la distance entre la position du boid et du field
        if (Vector3.Distance(transform.position, Vector3.zero) >= GlobalFlock.fieldSize)
        {
            turning = true;
        }
        else
            turning = false;

        if (turning)
        {
            //calcule la direction pour revenir au centre du field et applique une rotation dans cette direction
            Vector3 direction = Vector3.zero - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
            //speed = Random.Range(0.5f, 1);
            speed = 0.5f;
        }
        else
        {
            //pour ne pas appliquer les rules à chaque frame
            //if (Random.Range(0, 5) < 1)
                Rules();
        }

        transform.Translate(0, 0, Time.deltaTime * speed);
    }

    void Rules()
    {
        //identifie les objets boid afin d'appliquer les règles
        GameObject[] GoB;
        GoB = GlobalFlock.allBoids;

        //Initalement les vecteurs sont à zéro mais ils vont être calculés par la suite
        //Vecteur qui permet de suivre les autres boids (leur centre)
        Vector3 averageCenter = Vector3.zero;
        //Vecteur qui permet de les éviter s'ils sont trop proches
        Vector3 avoid = Vector3.zero;

        //Vitesse du groupe
        float gSpeed = 0.1f;

        Vector3 goalPosition = GlobalFlock.goalPosition;

        float distance;

        //contient les boids voisins (voisins sont reconnus dans une var neighboorDistance de 2.0f)
        //si un boid est dans cette zone de 2.0f il est compté dans le groupe voisins
        int groupSize = 0;

        foreach (GameObject go in GoB)
        {
            if (go != this.gameObject)
            {
                distance = Vector3.Distance(go.transform.position, this.transform.position);

                //appliqués uniquement au groupe de boid voisins et pas les boids distants
                if (distance <= neighboorDistance)
                {
                    //additionne les centres pour avoir une moyenne
                    averageCenter += go.transform.position;
                    //ajuste la taille du groupe
                    groupSize++;

                    //si la distance entre deux boids est trop petite, on ajoute un vecteur à la direction opposée de la direction où le boid se dirige
                    if(distance < 2.0f)
                    {
                        avoid = avoid + (this.transform.position - go.transform.position);
                    }

                    Boid anotherBoid = go.GetComponent<Boid>();
                    gSpeed = gSpeed + anotherBoid.speed;
                }
            }
        }

        if (groupSize > 0)
        {
            //calcul la moyenne du centre et de la vitesse du groupe
            averageCenter = averageCenter / groupSize + (goalPosition - this.transform.position);
            speed = gSpeed / groupSize;

            //prend le vecteur du centre et l'additionne au vecteur d'évitement moins la postion actuelle du boid, le tout donne la direction à prendre
            Vector3 direction = (averageCenter + avoid) - transform.position;

            //si le boid doit changer de direction il doit avoir une nouvelle rotation
            //Slerp permet d'avoir une trasition smooth
            if (direction != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
                transform.position += avoid * speed * Time.deltaTime;
            }
            }
    }
}
