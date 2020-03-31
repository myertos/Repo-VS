using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;             //Floating point variable to store the player's movement speed.

    private Rigidbody2D rb2d;       //Store a reference to the Rigidbody2D component required to use 2D Physics.

    public int startingHealth = 100;                            // The amount of health the player starts the game with.
    public int currentHealth;                                   // The current health the player has.
                                                                //public Slider healthSlider;                                 // Reference to the UI's health bar.
                                                                //public Image damageImage;                                   // Reference to an image to flash on the screen on being hurt.

    //PlayerShooting playerShooting;                              // Reference to the PlayerShooting script.
    bool isDead;                                                // Whether the player is dead.
    bool damaged;                                               // True when the player gets damaged.

    // Use this for initialization
    void Start()
    {
        //Получаем и храним свойства Rigidbody2D component соответственно мы получаем доступ к нему.
        rb2d = GetComponent<Rigidbody2D>();
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Укажем код для управления физикой объекта (Put physics code here).
    void FixedUpdate()
    {
        //Храним фактические данные ввода в (Store the current horizontal input in the) float moveHorizontal.
        float moveHorizontal = Input.GetAxis("Horizontal");

        //Храним фактические данные ввода в (Store the current vertical input in the) float moveVertical.
        float moveVertical = Input.GetAxis("Vertical");

        //Используем два хранилища (числового типа с плавающей запятой), чтоб задать новоей напрвление (Use the two store floats to create a new) Vector2 variable movement.
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        //Вызываем Эдфорс, который задаст направление движения тела учитывая множитель скорости (Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player).
        rb2d.AddForce(movement * speed);
    }
    //OnTriggerEnter2D is called whenever this object overlaps with a trigger collider.
    void OnTriggerEnter2D(Collider2D other)
    {
        //Check the provided Collider2D parameter other to see if it is tagged "PickUp", if it is...
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
        }
    }
    void Update()
    {
        // If the player has just been damaged...
        if (damaged)
        {
            // ... set the colour of the damageImage to the flash colour.
            //damageImage.color = flashColour;
        }
        // Otherwise...
        else
        {
            // ... transition the colour back to clear.
            //damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }

        // Reset the damaged flag.
        damaged = false;
    }


    public void TakeDamage(int amount)
    {
        // Set the damaged flag so the screen will flash.
        damaged = true;

        // Reduce the current health by the damage amount.
        currentHealth -= amount;

        // Set the health bar's value to the current health.
        //healthSlider.value = currentHealth;

        // Play the hurt sound effect.
        //playerAudio.Play();

        // If the player has lost all it's health and the death flag hasn't been set yet...
        if (currentHealth <= 0 && !isDead)
        {
            // ... it should die.
            Death();
        }
    }

    void Death()
    {
        // Set the death flag so this function won't be called again.
        isDead = true;

        // Turn off any remaining shooting effects.
        //playerShooting.DisableEffects();

        // Tell the animator that the player is dead.
        //anim.SetTrigger("Die");

        // Set the audiosource to play the death clip and play it (this will stop the hurt sound from playing).
        //playerAudio.clip = deathClip;
        //playerAudio.Play();

        // Turn off the movement and shooting scripts.
        //playerMovement.enabled = false;
        //playerShooting.enabled = false;
    }
}

