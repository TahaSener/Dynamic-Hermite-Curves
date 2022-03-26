using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hermite : MonoBehaviour
{
    public Transform StartP;
    public Transform StartD;
    public Transform EndP;
    public Transform EndD;
    List<GameObject> positions;
    Vector3 startPosOfSD;
    Vector3 startPosOfED;
    Vector3 EndPosOfSD;
    Vector3 EndPosOfED;
    float desiredDuration = 2f;
    float elapsedTime;
    bool downMovementDone=false;
    public GameObject sprite;

    
    private void Awake()
    {
        positions = new List<GameObject>();
    }
    private void Start()
    {
        
        startPosOfSD = StartD.position;
        startPosOfED = EndD.position;
        EndPosOfED = startPosOfSD;
        EndPosOfSD = startPosOfED;
        for (float i = 0; i < 1f; i += .001f)
        {
            float x = ((2 * Mathf.Pow(i, 3) - 3 * Mathf.Pow(i, 2) + 1) * StartP.position.x) + ((-2 * Mathf.Pow(i, 3) + 3 * Mathf.Pow(i, 2)) * EndP.position.x) + ((Mathf.Pow(i, 3) - 2 * Mathf.Pow(i, 2) + i) * StartD.position.x) + ((Mathf.Pow(i, 3) - Mathf.Pow(i, 2)) * EndD.position.x);
            float y = ((2 * Mathf.Pow(i, 3) - 3 * Mathf.Pow(i, 2) + 1) * StartP.position.y) + ((-2 * Mathf.Pow(i, 3) + 3 * Mathf.Pow(i, 2)) * EndP.position.y) + ((Mathf.Pow(i, 3) - 2 * Mathf.Pow(i, 2) + i) * StartD.position.y) + ((Mathf.Pow(i, 3) - Mathf.Pow(i, 2)) * EndD.position.y);
            Vector2 pos = new Vector2(x, y);
            
            positions.Add(Instantiate(sprite, pos, Quaternion.identity));
        }
        
    }

    
    void Update()
    {
        elapsedTime += Time.deltaTime;
        float percentage = elapsedTime / desiredDuration;
        if (!downMovementDone)
        {
            StartD.position = Vector2.Lerp(startPosOfSD, EndPosOfSD, Mathf.SmoothStep(0, 1, percentage));
            EndD.position = Vector2.Lerp(startPosOfED, EndPosOfED, Mathf.SmoothStep(0, 1, percentage));
        }
        else
        {
            StartD.position = Vector2.Lerp(EndPosOfSD, startPosOfSD, Mathf.SmoothStep(0, 1, percentage));
            EndD.position = Vector2.Lerp(EndPosOfED, startPosOfED, Mathf.SmoothStep(0, 1, percentage));
        }
        
        for (float i = 0; i < 1f; i += .001f)
        {
            float x = ((2 * Mathf.Pow(i, 3) - 3 * Mathf.Pow(i, 2) + 1) * StartP.position.x) + ((-2 * Mathf.Pow(i, 3) + 3 * Mathf.Pow(i, 2)) * EndP.position.x) + ((Mathf.Pow(i, 3) - 2 * Mathf.Pow(i, 2) + i) * StartD.position.x) + ((Mathf.Pow(i, 3) - Mathf.Pow(i, 2)) * EndD.position.x);
            float y = ((2 * Mathf.Pow(i, 3) - 3 * Mathf.Pow(i, 2) + 1) * StartP.position.y) + ((-2 * Mathf.Pow(i, 3) + 3 * Mathf.Pow(i, 2)) * EndP.position.y) + ((Mathf.Pow(i, 3) - 2 * Mathf.Pow(i, 2) + i) * StartD.position.y) + ((Mathf.Pow(i, 3) - Mathf.Pow(i, 2)) * EndD.position.y);
            int index = Mathf.RoundToInt(i * 1000);
            positions[index].transform.position=new Vector2(x,y);
        }
        
        if (percentage > 1)
        {
            elapsedTime = 0;
            if (downMovementDone)
            {
                downMovementDone = false;
                
            }
            else
            {
                downMovementDone = true;
                
            }
        }
        
    }
    private void OnDrawGizmos()
    {
        if (positions != null)
        {
            
        }
        
    }
}
