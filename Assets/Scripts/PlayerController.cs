using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;

    [SerializeField] public ParticleSystem crashParticle;
    // Start is called before the first frame update
    void Start()
    {
        speed = 25f;
        rotationSpeed = 15f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * speed);

        if (Input.GetKey(KeyCode.Space))
        {
            transform.Rotate(new Vector3(0,0,15) * Time.deltaTime * rotationSpeed);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject. CompareTag("Finish"))
        {
            
            Destroy(other.gameObject, 0.2f);
        }
    }
}
