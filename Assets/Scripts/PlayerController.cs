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
    public ParticleSystem crashParticle;
    public float timer;
    public TextMeshProUGUI timerText;
    public GameObject winPanel;
    public TextMeshProUGUI timeStopped;
    public int lenghtArray;
    
    
    // Start is called before the first frame update
    void Start()
    {
        speed = 25f;
        rotationSpeed = 15f;
        winPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        DisplayTime();
        
        transform.Translate(Vector3.up * Time.deltaTime * speed);

        if (Input.GetKey(KeyCode.Space))
        {
            transform.Rotate(new Vector3(0,0,15) * Time.deltaTime * rotationSpeed);
        }

        if (speed <= 5f)
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
            speed /= 2f;
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
