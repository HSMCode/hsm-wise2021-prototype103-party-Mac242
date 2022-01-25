using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;
    public GameObject[] targets;
    public GameObject bottle;
    public ParticleSystem crashParticle;
    public float timer;
    private bool startGame;
    public TextMeshProUGUI timerText;
    //public TextMeshProUGUI collisionText;
    private int collisions;
    public GameObject winPanel;
    public GameObject instructionPanel;
    public TextMeshProUGUI timeStopped;
    public int lenghtArray;
    
    
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
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.S)&& startGame==false)
        {
            instructionPanel.SetActive(false);
        }       

        //collisionText.text = "Allowed Collisions: " + collisions;
        
        if (Input.GetKey(KeyCode.Space)&& startGame==false)
        {
            bottle.SetActive(false);
            startGame = true;
        }
        
        if (startGame == true)
        {
            DisplayTime();
            transform.Translate(Vector3.up * Time.deltaTime * speed);
        }
        

        if (Input.GetKey(KeyCode.Space)&& startGame==true)
        {
            transform.Rotate(new Vector3(0,0,15) * Time.deltaTime * rotationSpeed);
        }

        if (collisions <= 0)
        {
            GameOver();
            Debug.Log("GameOver!");
        }

        if (targets.Length <= 0) 
        {
            Win();
            Debug.Log("Win!");
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {

            crashParticle.Play();
            Destroy(other.gameObject, 0.1f);

            lenghtArray = targets.Length;
            Array.Resize(ref targets, lenghtArray -1 );

        }
        
        if (other.gameObject.CompareTag("Enemy"))
        {
            speed /= 1.5f;
            collisions--;
            
        }
    }

    public void GameOver()
    {
        Time.timeScale = 0;
    }
    
    public void Win()
    {
        Time.timeScale = 0;
        winPanel.SetActive(true);
        timeStopped.text = "" + timerText.text;
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
