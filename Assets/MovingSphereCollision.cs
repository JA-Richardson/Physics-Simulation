using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSphereCollision : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Sphere1;
    public GameObject Sphere2;
    public Vector3 Velocity1 = new Vector3(0.0f, 0.0f, 0.0f);
    public Vector3 Velocity2 = new Vector3(0.0f, 0.0f, 0.0f);


    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        Sphere1.transform.position += Velocity1 * Time.deltaTime;
        Sphere2.transform.position += Velocity2 * Time.deltaTime;
        collision(Sphere1, Sphere2);
    }

    void collision(GameObject Sphere1, GameObject Sphere2)
    {
        Vector3 SphereOnePos = Sphere1.transform.position;
        Vector3 SphereTwoPos = Sphere2.transform.position;
        Vector3 SphereOneVelocity = Velocity1 / Time.deltaTime;
        Vector3 SphereTwoVelocity = Velocity2 / Time.deltaTime;

        float radius1 = 0.5f;
        float radius2 = 0.5f;
        float T = 0;

        float a1i = SphereOnePos.x;
        float b1j = SphereOnePos.y;
        float c1k = SphereOnePos.z;

        float a2i = SphereTwoPos.x;
        float b2j = SphereTwoPos.y;
        float c2k = SphereTwoPos.z;

        float d1i = SphereOneVelocity.x;
        float e1j = SphereOneVelocity.y;
        float f1k = SphereOneVelocity.z;

        float d2i = SphereTwoVelocity.x;
        float e2j = SphereTwoVelocity.y;
        float f2k = SphereTwoVelocity.z;

        float deltaXP = a1i - a2i;
        float deltaYP = b1j - b2j;
        float deltaZP = c1k - c2k;

        float deltaXV = d1i - d2i;
        float deltaYV = e1j - e2j;
        float deltaZV = f1k - f2k;
        //quadratic equation for finding values needed to see if the spheres collide
        float A = (deltaXV * deltaXV) + (deltaYV * deltaYV) + (deltaZV * deltaZV);
        float B = (2 * deltaXP * deltaXV) + (2 * deltaYP * deltaYV) + (2 * deltaZP * deltaZV);
        float C = (deltaXP * deltaXP) + (deltaYV * deltaYV) + (deltaZP * deltaZP) - ((radius1 + radius2) * (radius1 + radius2));
        float NB = B * -1;
        float sqrt = Mathf.Sqrt(B * B - 4 * A * C);


        //if square root is greater than 0 then a collision has occured
        if (sqrt > 0)
        {
            //calculate collision time and store in value T
            float t1 = ((NB + sqrt) / (2 * A)) * Time.deltaTime;
            float t2 = ((NB - sqrt) / (2 * A)) * Time.deltaTime;

            if (t1 > t2)
            {
                T = t2;
            }
            else if (t2 > t1)
            {
                T = t1;
            }
            
            //calculates distance between spheres at time of collision
            float R = Mathf.Sqrt(((deltaXP + (T * deltaXV)) * (deltaXP + (T * deltaXV))) + ((deltaYP + (T * deltaYV)) * (deltaYP + (T * deltaYV))) + ((deltaZP + (T * deltaZV)) * (deltaZP + (T * deltaZV))));

            //if the distance is less or equal to the sum of the radii a collision has occured
            if (R <= radius1 + radius2)
            {
                Debug.Log("HIT");
                //response calculation
                Vector3 ResposeOne = SphereOnePos - SphereTwoPos; //calculate response vector
                ResposeOne = ResposeOne.normalized;  //normalise the vector              
                ResposeOne = ResposeOne * SphereOneVelocity.magnitude; //scale the vector to the velocity magnitude
                float Force1 = Mathf.Cos(Vector3.Angle(SphereOneVelocity, ResposeOne)/180*Mathf.PI); //calculate the force using the angle between the velocity and response vector
                SphereOneVelocity = SphereOneVelocity + ResposeOne * (1 - Force1); //update the velocity by the response vector and the force

                Vector3 ResposeTwo = SphereTwoPos - SphereOnePos;
                ResposeTwo = ResposeTwo.normalized;
                ResposeTwo = ResposeTwo * SphereTwoVelocity.magnitude;
                float Force2 = Mathf.Cos(Vector3.Angle(SphereTwoVelocity, ResposeTwo) / 180 * Mathf.PI);
                SphereTwoVelocity = SphereTwoVelocity + ResposeTwo * (1 - Force2);

                Velocity1 = SphereOneVelocity * Time.deltaTime;
                Velocity2 = SphereTwoVelocity * Time.deltaTime;
            }

        }

    }


}