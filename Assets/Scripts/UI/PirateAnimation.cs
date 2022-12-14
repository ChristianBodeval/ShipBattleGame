using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateAnimation : MonoBehaviour
{
    // Start is called before the first frame update

    Animation animator;
  public  TestTextWriter testTextWriter;

    //Animator getPirateAnimator()
    //{
    //    return animator;
    //}
    //private Animator setPirateAnimator(Animator animator)
    //{
    //    this.animator = animator;
    //    return this.animator;
    //}


    void Start()
    {
        animator = GetComponent<Animation>();
        testTextWriter = GetComponent<TestTextWriter>();
    }

    // Update is called once per frame
    void Update()
    {

        if (testTextWriter.GetIndex() == 2)
        {
            animator.Stop();
        }
        else
        {
            animator.Play();
        }
    }
}
