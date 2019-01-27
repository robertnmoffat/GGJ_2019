using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneDoor : MonoBehaviour {

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
        if (Input.GetKeyUp("e"))
        {
            if (!animator.GetBool("isOpen"))
            {
                open();
            }
            else
            {
                if (animator.GetBool("isOpen"))
                {
                    close();
                }
            }
        }
    }





public void open()
{
    animator.SetTrigger("onInteract");
    animator.SetBool("isOpen", true);
        accessModulate();

}

public void close()
{
    animator.SetTrigger("onInteract");
    animator.SetBool("isOpen", false);
        accessModulate();

}

public void accessModulate()
{
    if (front.enabled)
    {
        front.enabled = false;
            back.enabled = false;
    }
    else
    {
        if (!front.enabled)
        {
            front.enabled = true;
            back.enabled = true;
        }
    }
}
}
