using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class movementMan : MonoBehaviour
{
    public string seinfeldChar = "George";
    public GameObject targetObj;
    private Transform target;
    public string targetString;
    public float speed;
    // Start is called before the first frame update
    public GameObject source;
    private Animator anim;
    private UnityEngine.AI.NavMeshAgent agent;
    private Transform character;

    void Start()
    {
        anim = source.GetComponent<Animator>();
        agent = source.GetComponent<UnityEngine.AI.NavMeshAgent>();
        character = source.GetComponent<Transform>();
        
    }
    public bool active = false;


    void Update()
    {
        if (active == true) {

            StartCoroutine(setCharacter(seinfeldChar, targetString));

            agent.SetDestination(target.position);

        if (Vector3.Distance(character.position, target.position) < 2)
            {
                anim.SetBool("Walking", false);
            active = false;
     
            }

            else
            {
                anim.SetBool("Walking", true);
    
            }

        }
    }

    IEnumerator setCharacter(string tag, string targetString)
    {

        source = GameObject.FindGameObjectsWithTag(tag)[0];
        anim = source.GetComponent<Animator>();
        agent = source.GetComponent<UnityEngine.AI.NavMeshAgent>();
        character = source.GetComponent<Transform>();

        targetObj = GameObject.FindGameObjectsWithTag(targetString)[0];
        target = targetObj.GetComponent<Transform>();

        yield return 0;
    }
}
