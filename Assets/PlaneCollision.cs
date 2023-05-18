using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneCollision : MonoBehaviour
{

    public GameObject Sphere;
    public GameObject Plane;
    public Vector3 Velocity = new Vector3(0.0f, 0.0f, 0.0f);

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Sphere.transform.position += Velocity * Time.deltaTime;
        Collision(Plane, Sphere);

    }

    void Collision(GameObject plane, GameObject sphere)
    {
        float radius = 0.5f; //sets the radius of the sphere
        Vector3 pN = plane.transform.up; //normal of the plane
        Vector3 V = sphere.transform.position - plane.transform.position; //vector from sphere centre to the plane
        var dot = Vector3.Dot(pN, V); //dot product of the two vectors
        if (dot < radius) //collision has occured if the dot is less than the radius
        {
            Vector3 response = 2 * (Vector3.Dot(Velocity, pN) / pN.magnitude * pN); //calculates the response vector
            Velocity = Velocity - response; //updates sphere velocity based on the response
        }
    }

}
