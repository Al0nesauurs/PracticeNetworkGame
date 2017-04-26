using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {


    public GameObject bulletPrefab;
    public Transform bulletSpawn;

	void Update ()
    {
        //not move other player
        if(!isLocalPlayer)
        {
            return;
        }

        //Controlling
        float x = Input.GetAxis("Horizontal") * Time.deltaTime * 150;
        float z = Input.GetAxis("Vertical") * Time.deltaTime * 3;
        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            CmdFire();
        }
	}

    [Command]
    void CmdFire()
    {
        //Create the bullet from prefab
        GameObject bullet = (GameObject)Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        //Add velocity to bullet
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6;

        //Spawn the bullet on the clients
        NetworkServer.Spawn(bullet);

        //Destroy the bullet after 2 seconds
        Destroy(bullet, 2);
    }
    //Set player to color blue
    public override void OnStartLocalPlayer() 
    {
        GetComponent<MeshRenderer>().material.color = Color.blue;
    }
}
