using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    public bool isWalk = false;
    
    private float speed = 3;
    private Animator anim;
    private static readonly int IsWalk = Animator.StringToHash("IsWalk");

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        
        float res = Mathf.Max(Mathf.Abs(h), Mathf.Abs((v)));

        if (res > 0)
        {
            anim.SetBool(IsWalk, true);
            
            transform.Translate(new Vector3(h, 0, v) * (speed * Time.deltaTime), Space.World) ;
            transform.rotation = Quaternion.LookRotation(new Vector3(-h, 0, -v));
            isWalk = true;
        }
        else
        {
            anim.SetBool(IsWalk, false);
            isWalk = false;
        }
    }
}
