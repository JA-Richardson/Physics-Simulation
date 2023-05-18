using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereCollider : MonoBehaviour
{
    public GameObject Sphere1;
    public GameObject Sphere2;
    public Vector3 Velocity = new Vector3(0.0f, 0.0f, 0.0f);
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Sphere1.transform.position += Velocity * Time.deltaTime;
        collision(Sphere1 , Sphere2);   
    }
    //calculates the angle between two vectors
    float Angle(Vector3 vec1, Vector3 vec2)
    {
        var dot = Vector3.Dot(vec1 , vec2);
        dot = dot / (vec1.magnitude * vec2.magnitude);
        var acos = Mathf.Acos(dot); 
        float Angle = acos * 180 / Mathf.PI;
        return Angle;
    }

    void collision(GameObject Sphere1, GameObject Sphere2)
    {
        float r1 = 0.5f, r2 = 0.5f;
        float radius = r1 + r2;
        Vector3 a = Sphere2.transform.position - Sphere1.transform.position; //vector from centre of sphere1 to sphere 2
        float a_length = a.magnitude;
        float Q = Angle(Velocity, a);
        float d = a_length * Mathf.Sin(Q / 180 * Mathf.PI);//distance between sphere centers
        float e = Mathf.Sqrt(radius * radius - d * d); //distance between sphere surface
        float vc = Mathf.Cos(Q / 180 * Mathf.PI) * a_length - e; //collision point

        if(vc <= Velocity.magnitude * Time.deltaTime) // if collision point is on the velcoity vector, calculate the response
        {
            Vector3 vcVector = Velocity / Velocity.magnitude * vc;
            Sphere2.transform.position += vcVector;

            Vector3 ResposeOne = Sphere1.transform.position - Sphere2.transform.position; //calculate response vector
            ResposeOne = ResposeOne.normalized; //normalise the vector
            ResposeOne = ResposeOne * Velocity.magnitude; //scale the vector to the velocity magnitude
            float Force1 = Mathf.Cos(Vector3.Angle(Velocity, ResposeOne) / 180 * Mathf.PI); //calculate the force using the angle between the velocity and response vector
            Velocity = Velocity + ResposeOne * (1 - Force1); //update the velocity by the response vector and the force

            //Unsure as to why this doesn't work on the stationary sphere
            Vector3 ResposeTwo = Sphere2.transform.position - Sphere1.transform.position;
            ResposeTwo = ResposeTwo.normalized;
            ResposeTwo = ResposeTwo * Velocity.magnitude;
            float Force2 = Mathf.Cos(Vector3.Angle(Velocity, ResposeTwo) / 180 * Mathf.PI);
            Velocity = Velocity + ResposeTwo * (1 - Force2);
            
        }

    }
}
