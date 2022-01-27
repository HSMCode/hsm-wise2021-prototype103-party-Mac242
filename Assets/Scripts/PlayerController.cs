using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed; // forwardSpeed
    [SerializeField] private float rotationSpeed;
    public GameObject[] targets; // array with the targets(beer)
    public GameObject bottle;
    public float timer; // Time
    private bool startGame;
    private bool instructionRead;// asking if game has started
    private int collisions; // allowed Collisions
    public int lenghtArray; //Lenght of Array
    public ParticleSystem crashParticle; // particle for hitting beer
    //UI Elements
    public GameObject winPanel;
    public GameObject losePanel;
    public GameObject instructionPanel;
    public TextMeshProUGUI timerText; // shows time in game
    public TextMeshProUGUI timeStopped; // shows stopped time in winningPanel

    public AudioSource[] audioClip = new AudioSource[5];
    public AudioClip audioClipCollision;
    // Start is called before the first frame update
    void Start()
    {
        startGame = false;
        bottle.SetActive(true);
        speed = 50f;
        rotationSpeed = 10f;
        winPanel.SetActive(false);
        collisions = 3;
        instructionPanel.SetActive(true);
        losePanel.SetActive(false);
        GetComponent<AudioSource>();
        audioClip[3].Play();
        instructionRead = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.S)&& startGame==false && instructionRead==false) // Getting the game almost started
        {
            instructionPanel.SetActive(false);
            audioClip[2].Play();
            audioClip[3].Pause();
            instructionRead = true;
        }       

        //collisionText.text = "Allowed Collisions: " + collisions;
        
        if (Input.GetKey(KeyCode.Space)&& startGame==false && instructionRead ==true) //cork is in the bottle, space to release the cork
        {
            bottle.SetActive(false);
            startGame = true; // cork is released
            
            audioClip[2].Pause();
            audioClip[0].Play();
            
        }
        
        if (startGame == true) //cork is released , game started, time is running
        {
            Time.timeScale = 1;
            DisplayTime();
            transform.Translate(Vector3.up * Time.deltaTime * speed);
        }
        

        if (Input.GetKey(KeyCode.Space)&& startGame==true) // movement, rotate the player when hitting Space
        {
            transform.Rotate(new Vector3(0,0,15) * Time.deltaTime * rotationSpeed);
        }

        if (collisions <= 0) // GameOver Condition
        {
            
            GameOver();
            Debug.Log("GameOver!");
            
        }

        if (targets.Length <= 0)  // Win Condition
        {
            
            Win();
            Debug.Log("Win!");
            
        }
    }

    private void OnTriggerEnter(Collider other) // destroy beer
    {
        if (other.gameObject.CompareTag("Finish"))
        {

            crashParticle.Play();
            Destroy(other.gameObject, 0.1f); // destroy object

            lenghtArray = targets.Length;
            Array.Resize(ref targets, lenghtArray -1 ); // remove an arrayelemet from the array
        }
    }

    private void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.CompareTag("Enemy")) // decrease speed if the player collides with partyguest
        {
            audioClip[4].PlayOneShot(audioClipCollision);
            speed /= 1.5f;
            collisions--; // counting down collisions
        }
        
        if (other.gameObject.CompareTag("Respawn")) // rotate the Player if he collides with the bounds
        {

            gameObject.transform.Rotate(0,0,180 );
        }
    }

    public void GameOver()
    {
        Time.timeScale = 0; // stop gametime
        losePanel.SetActive(true); // GameOver activated
    }
    
    public void Win()
    {
        Time.timeScale = 0; // stop gametime
        winPanel.SetActive(true); // Win activated
        timeStopped.text = "" + timerText.text; // show time player needed to crash everything
    }
    
    public void DisplayTime()
    {
        // Timer / Countdown Set up
        timer += Time.deltaTime;  // counting seconds down
        int minutes = Mathf.FloorToInt(timer / 60f); 
        int seconds = Mathf.FloorToInt(timer % 60f);
        float fraction = Mathf.FloorToInt(timer * 1000f);
        fraction = (fraction % 1000f);
        timerText.text =  "" + string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, fraction); // Display Timer ; Format / Text
    }
}
