using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour {

    public GameObject Door;
    public Animator Ani = null;
    public bool m_IsOpen;

    private void Start()
    {
        Ani = Door.GetComponent<Animator>();
        m_IsOpen = true;
    }
    // Update is called once per frame


    void Update()
    {
        Ani.SetBool("IsOpen", m_IsOpen);
    }
}
