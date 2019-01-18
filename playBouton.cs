using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class playBouton : MonoBehaviour
{
    Animator animator;
    public PlayableDirector director;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame

    void Update()
    {

    }
   public void click()
    {
        GameObject.Find("UI").SetActive(false);
        animator.SetBool("walk", true);

    }

    void OnEnable()
    {
        director.stopped += OnPlayableDirectorStopped;
    }

    void OnPlayableDirectorStopped(PlayableDirector aDirector)
    {
        if (director == aDirector)
        {
           
            Debug.Log("PlayableDirector named " + aDirector.name + " is now stopped.");
            aDirector.enabled = false;
        }
            
    }

    void OnDisable()
    {
        director.stopped -= OnPlayableDirectorStopped;
    }

}
