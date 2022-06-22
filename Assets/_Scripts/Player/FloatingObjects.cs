using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingObjects : MonoBehaviour
{
    
    public float     RotateSpeed;
    public float     distance;
    public Transform centre;
    private float _angle;
    private AudioSource audio;
    public bool moving = true;

    public bool reticence = false;


    public GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");
        centre = player.transform;

        audio = GetComponent<AudioSource>();
    }


    void Update()
    {
        if(moving)
        {
            _angle += (RotateSpeed + player.GetComponent<Player_Movement>().collectedSoul) * Time.deltaTime;

            transform.Rotate(new Vector3(0, 0, 10), -360 * Time.deltaTime);

            var offset = new Vector3(Mathf.Sin(_angle), Mathf.Cos(_angle)) * distance;

            transform.position = centre.position + offset;
        }

        if(reticence)
        {
            var offset = new Vector3(Mathf.Sin(_angle), Mathf.Cos(_angle)) * distance;

            transform.position = centre.position + offset;
        }
       
    }

    public void PlaySound()
    {
        audio.Play();
    }

}
