using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabController : MonoBehaviour
{

    public GameObject crab;
    public Animator animator;
    public AudioSource audioSource;

    private List<Tween> activeTweens;
    private int tweenIndex;
    private const float Speed = 5f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
