using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tweener : MonoBehaviour
{

    public GameObject crab;
    
    private List<Tween> activeTweens;
    private int tweenIndex;
    private const float Speed = 5f;
    
    // Start is called before the first frame update
    void Start()
    {
        activeTweens = new List<Tween>();
        tweenIndex = 0;
        
        Vector3 topLeft = new Vector3(-12.1f, 13.45f, 0);
        Vector3 topRight = new Vector3(-1.06f, 13.45f, 0);
        Vector3 botRight = new Vector3(-1.06f, 9.65f, 0);
        Vector3 botLeft = new Vector3(-12.1f, 9.65f, 0);
        
        AddTween(crab.transform, topLeft, topRight);
        AddTween(crab.transform, topRight, botRight);
        AddTween(crab.transform, botRight, botLeft);
        AddTween(crab.transform, botLeft, topLeft);
        AddTween(crab.transform, topLeft, topLeft);
    }

    // Update is called once per frame
    void Update()
    {
        Tween activeTween = activeTweens[tweenIndex];
        float distance = Vector3.Distance(activeTween.Target.position, activeTween.EndPos);
        
            if (distance > 0.1f)
            {
                float elapsedTime = Time.time - activeTween.StartTime;
                float time = elapsedTime / activeTween.Duration;
                activeTween.Target.position = Vector3.Lerp(activeTween.StartPos, activeTween.EndPos, time);
            }
            else if (distance == 0)
            {
                float elapsedTime = Time.time - activeTween.StartTime;
                float time = elapsedTime / activeTween.Duration;
                activeTween.Target.position = Vector3.Lerp(activeTween.StartPos, activeTween.EndPos, time);
                
                if (activeTween.Duration - elapsedTime <= 0)
                {
                    activeTween.Target.position = activeTween.EndPos;
                    tweenIndex = (tweenIndex + 1) % activeTweens.Count;
                    activeTweens[tweenIndex].StartTime = Time.time;
                }
            }
            else
            {
                activeTween.Target.position = activeTween.EndPos;
                tweenIndex = (tweenIndex + 1) % activeTweens.Count;
                activeTweens[tweenIndex].StartTime = Time.time;
            }   
    }
    
    public void AddTween(Transform targetObject, Vector3 startPos, Vector3 endPos)
    {
        float distance = Vector3.Distance(startPos, endPos);
        float duration = distance / Speed;
        if (distance == 0)
        {
            duration = 3f;
        }
        activeTweens.Add(new Tween(targetObject, startPos, endPos, Time.time, duration));
    }
}
