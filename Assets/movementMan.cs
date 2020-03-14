using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movementMan : MonoBehaviour
{
    public List<Transform> targets = new List<Transform>();
    public Transform target;
    public Transform character;
    public float speed;
    // Start is called before the first frame update
    public GameObject animationSource;
    private Animator anim;
    private UnityEngine.AI.NavMeshAgent agent;

    void Start()
    {
        anim = animationSource.GetComponent<Animator>();
        agent = animationSource.GetComponent<UnityEngine.AI.NavMeshAgent>();
       
    }
    int i = 0;

    // Update is called once per frame
    void Update()
    {

        //  if (i < targets.Count)
        //  {

        agent.SetDestination(target.position);

        if (Vector3.Distance(character.position, target.position) < 1.2)
            {
                anim.SetBool("Walking", false);
               // i++;
            }

            else
            {
                anim.SetBool("Walking", true);
             //   agent.SetDestination(targets[i].position);
            }
      //  }

           /* float step = speed * Time.deltaTime;
        character.transform.position = Vector3.Lerp(character.position, target.position, step);
        character.transform.LookAt(target.position);
        if (speed >= .2)
        {
            anim.SetBool("Walking", true);
        }
        else if (speed < .2)
        {
            anim.SetBool("Walking", false);
        }
        */
    }
}
