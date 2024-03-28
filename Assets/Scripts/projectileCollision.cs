using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileCollision : MonoBehaviour
{
    public int damage;
    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag != "Enemies" )
        {
            Destroy(gameObject);
            
        }
    }
    
}
