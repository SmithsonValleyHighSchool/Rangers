using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class NewPlayerMove : MonoBehaviour
{
    public float speed = 0;
	public TextMeshProUGUI countText;
	public TextMeshProUGUI LifeText;
	public GameObject winTextObject;
	public GameObject GameOverTextObject;
	public GameObject Effect;
	public GameObject DamageEffect;
	public GameObject Player;

    private Rigidbody rb;
	private int count;
	private int LifeCount;

    private float movementX;
    private float movementY;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
		count = 0;
		LifeCount = 3;
		SetCountText (); 
		SetLifeText ();
		// Set the text property of the Win Text UI to an empty string, making the 'You Win' (game over message) blank
                winTextObject.SetActive(false);
				GameOverTextObject.SetActive(false);
    }

    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);
    }
	
	private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Gold")) 
        {
            other.gameObject.SetActive(false);
			// Add one to the score variable 'count'
			count = count + 1;
			Instantiate (Effect, other.transform.position, other.transform.rotation);

			// Run the 'SetCountText()' function (see below)
			SetCountText ();
        }
    }
	
	private void OnCollisionEnter (Collision other)
	{
	if (other.gameObject.CompareTag("Enemy"))
	{
	LifeCount = LifeCount - 1;
	Instantiate (DamageEffect, transform.position, transform.rotation);
	SetLifeText ();
	}
	}
	
	void SetCountText()
	{
		countText.text = "Count: " + count.ToString();

		if (count >= 4) 
		{
                    // Set the text value of your 'winText'
                    winTextObject.SetActive(true);
		}
	}
 void SetLifeText ()
 {
 LifeText.text = "Lives: " + LifeCount.ToString();
 
 if (LifeCount == 0)
 {
 GameOverTextObject.SetActive(true);
 Player.gameObject.SetActive(false);
 }
 }
}
