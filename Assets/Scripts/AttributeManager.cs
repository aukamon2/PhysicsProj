using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AttributeManager : MonoBehaviour
{
    public int health;
    public projectileCollision damage;
    public TextMeshProUGUI showHp;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        showHp.SetText("HP: " + health);
    }
    public void OnCollisionEnter(Collision other)
    {
        if(other.collider.tag == "Enemies")
        {
            health -= damage.damage;
            if(health <= 0)
            {
                Dead();
            }
        }
    }

    /*public void TakeDamage(int damage)
    {
            health -= damage;
            if(health <= 0)
            {
                Dead();
            }
           
    }*/
    private void Dead()
    {
        Destroy(gameObject);
        toCredit();
    }
    public void toCredit()
    {
        SceneManager.LoadSceneAsync(2);
    }
}
