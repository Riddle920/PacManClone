using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacStudentController : MonoBehaviour
{
    
    public Animator animator;
    public AudioSource audioSource;
    public Grid grid;

    private float duration;

    private Transform playerTrans;
    private KeyCode lastInput;
    private KeyCode currentInput;
    private Vector3 cellOffset;
    private bool lerping;
    
    // private List<Tween> activeTweens;
    // private int tweenIndex;
    // private const float Speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        playerTrans = gameObject.transform;
        duration = 0.2f;
        
        cellOffset = new Vector3(0.5f, 0.5f, 0);
        Vector3 startPos = grid.CellToWorld(new Vector3Int(-11, 1, 0)) + cellOffset;
        playerTrans.position = startPos;
        
        Vector3 endPos = grid.CellToWorld(new Vector3Int(-10, 1, 0)) + cellOffset;
        MovePlayer(endPos);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            lastInput = KeyCode.W;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            lastInput = KeyCode.A;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            lastInput = KeyCode.S;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            lastInput = KeyCode.D;
        }
    }

    private void MovePlayer(Vector3 targetPos)
    {
        lerping = true;
        StartCoroutine(LerpPlayer(targetPos));
    }
    
    private IEnumerator LerpPlayer(Vector3 targetPos)
    {
        Vector3 startingPos = playerTrans.position;
        float currentTime = 0.0f;
        while (currentTime < duration)
        {
            playerTrans.position = Vector3.Lerp(startingPos, targetPos, currentTime / duration);
            currentTime += Time.deltaTime;
            yield return null;
        }

        yield return null;
        playerTrans.position = targetPos;
        lerping = false;
    }
}
