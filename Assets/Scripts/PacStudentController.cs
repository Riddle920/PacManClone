using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PacStudentController : MonoBehaviour
{
    
    public Animator animator;
    public AudioSource moveSound;
    public AudioClip walk;
    public AudioClip eat;
    public Grid grid;
    public Tilemap walls;
    public Tilemap pellets;
    public ParticleSystem trail;
    
    private float duration;
    private Transform playerTrans;
    private Vector3Int startPos;
    private Vector3Int lastInput;
    private Vector3Int currentInput;
    private Vector3 cellOffset;
    private bool lerping;
    private Coroutine currentRoutine;
    
    // private List<Tween> activeTweens;
    // private int tweenIndex;
    // private const float Speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        playerTrans = gameObject.transform;
        duration = 0.2f;
        lerping = false;
        moveSound.clip = eat;
        
        cellOffset = new Vector3(0.5f, 0.5f, 0);
        startPos = new Vector3Int(-11, 1, 0);
        playerTrans.position = grid.CellToWorld(startPos) + cellOffset;
        animator.SetFloat("Horizontal", 0);
        animator.SetFloat("Vertical", 0);
        animator.SetFloat("Speed", 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            lastInput = Vector3Int.up;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            lastInput = Vector3Int.left;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            lastInput = Vector3Int.down;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            lastInput = Vector3Int.right;
        }

        if (!lerping && lastInput != Vector3Int.zero)
        {
            MovePlayer();
            MakeSound();
        }
    }

    private void MovePlayer()
    {
        Vector3Int targetPos;
        Vector3 tarWorldPos;
        
        if (lastInput != currentInput)
        {
            targetPos = startPos + lastInput;
            tarWorldPos = grid.CellToWorld(targetPos) + cellOffset;
            
            if (walls.GetTile(targetPos) is null)
            { 
                currentInput = lastInput;
                startPos = targetPos;
                lerping = true;
                StartCoroutine(LerpPlayer(tarWorldPos));
                return;
            }
        }
        
        targetPos = startPos + currentInput;
        tarWorldPos = grid.CellToWorld(targetPos) + cellOffset;

        if (walls.GetTile(targetPos) is null && currentInput != Vector3Int.zero)
        {
            startPos = targetPos;
            lerping = true;
            StartCoroutine(LerpPlayer(tarWorldPos));
        }
        else
        {
            animator.SetFloat("Horizontal", 0);
            animator.SetFloat("Vertical", 0);
            animator.SetFloat("Speed", 0);
        }
    }

    private IEnumerator LerpPlayer(Vector3 target)
    {
        Vector3 startingPos = playerTrans.position;
        float currentTime = 0.0f;
        while (currentTime < duration)
        {
            animator.SetFloat("Horizontal", (target - startingPos).normalized.x);
            animator.SetFloat("Vertical", (target - startingPos).normalized.y);
            animator.SetFloat("Speed", target.sqrMagnitude);
            
            trail.Play();

            playerTrans.position = Vector3.Lerp(startingPos, target, currentTime / duration);
            currentTime += Time.deltaTime;
            yield return null;
        }

        playerTrans.position = target;
        lerping = false;

    }

    private void MakeSound()
    {
        moveSound.clip = pellets.GetTile(startPos) is not null ? eat : walk;

        if (moveSound.clip != eat || !moveSound.isPlaying)
        {
            if (animator.GetFloat("Speed") != 0)
            {
                moveSound.Play();
            }
        }
    }

}
