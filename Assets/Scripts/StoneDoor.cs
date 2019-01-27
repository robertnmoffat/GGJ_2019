using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneDoor : Interactible
{

    Animator animator;
    public MeshCollider front;
    public MeshCollider back;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("e"))
        {
            if (!animator.GetBool("isOpen"))
            {
                open();
            } else {
            if (animator.GetBool("isOpen"))
                {
                    close();
                }
            }
        }
    }

        

    public StoneDoor(int x, int y, int z)
        : base(x, y, z)
    {

    }

    public void open()
    {
        animator.SetTrigger("onInteract");
        animator.SetBool("isOpen", true);

    }

    public void close()
    {
        animator.SetTrigger("onInteract");
        animator.SetBool("isOpen", false);

    }

    public void accessModulate()
    {
        if (front.enabled == false)
        {
            front.enabled = true;
            back.enabled = true;
        } else
        {
            if (front.enabled == true)
            {
                front.enabled = false;
                back.enabled = false;
            }
        }
    }

}