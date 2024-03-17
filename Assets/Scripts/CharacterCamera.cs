using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCamera : MonoBehaviour
{
    public GameObject player;
    public GameObject mCamera;

    Vector2 rotation = Vector2.zero;
    public float speed = 2.0f;
    public float lookSpeed = 2.0f;

    public float maxLook = 45;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
       
        mCamera.transform.position = new Vector3(player.transform.position.x, (float)(player.transform.position.y - 0.5), player.transform.position.z);
        rotation.y += Input.GetAxis("Mouse X");
        rotation.x += -Input.GetAxis("Mouse Y");

        transform.eulerAngles = (Vector2)rotation * speed;

        player.transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);

        rotation.x = Mathf.Clamp(rotation.x, -maxLook, maxLook);
        
    }
}
