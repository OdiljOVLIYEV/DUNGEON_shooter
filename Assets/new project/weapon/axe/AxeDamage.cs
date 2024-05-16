using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeDamage : MonoBehaviour
{
    // Start is called before the first frame update

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonUp("Fire1"))
        {

            anim.SetBool("attack", true);
            Invoke("BACK", 0.25f);
        }
        
        
    }

    private void BACK()
    {
        anim.SetBool("attack", false);
    }
}
