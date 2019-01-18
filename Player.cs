using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField]
    private float _speed = 5f;

	// Update is called once per frame
	void Update () {
        float horizontal = -Input.GetAxis("Horizontal");
        float vertical = -Input.GetAxis("Vertical");

        this.transform.Translate(new Vector3(horizontal,0,vertical) * _speed * Time.deltaTime);
    }
}
