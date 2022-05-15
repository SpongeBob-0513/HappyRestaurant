using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCut : MonoBehaviour
{
    private Animator anim;
    public GameObject robotInCutPlace1;
    public GameObject robotInCutPlace2;

    private bool place1CanCut = false;
    private bool place2CanCut = false;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                
            }
        }
    }

    private bool CanCut()
    {
        var z = gameObject.transform.position.z;

        if (z >= -8.72 && z <= -8.66)
        {
            var x = gameObject.transform.position.x;
            if (x > 6.4 && x < 6.6)
            {
                place1CanCut = true;
                return true;
            }
            else if (x > 7.9 && x < 8.12)
            {
                place2CanCut = true;
                return true;
            }
        }

        place1CanCut = false;
        place2CanCut = false;
        return false;
    }
}
