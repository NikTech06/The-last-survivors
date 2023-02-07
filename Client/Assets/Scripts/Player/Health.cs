using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int currentHealth;

    [SerializeField]
    private int maxHealth;

    public bool isAlive = true;

    [SerializeField]
    private PlayerLook playerLook;
    [SerializeField]
    private PlayerMovement playerMovement;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void Damage(int damage, string type)
    {
        currentHealth -= damage;

        if(currentHealth <= 0 ) 
        {
            isAlive = false;
            Die();
        }
    }

	public void Damage(int damage)
	{
		currentHealth -= damage;

		if (currentHealth <= 0)
		{
			isAlive = false;
			Die();
		}
	}

	void Die() 
    {
        playerLook.enabled = false;
        playerMovement.enabled = false;

        //Add more death logic in the future
    }
}
