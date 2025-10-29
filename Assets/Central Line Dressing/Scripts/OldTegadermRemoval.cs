using UnityEngine;

public class OldTegadermRemoval : MonoBehaviour
{
    private StepManager stepManager;

    public float distance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Find and assign Step Manager
        stepManager = GameObject.Find("Step Manager").GetComponent<StepManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //Calculate the distance the old tegaderm is to the body to decide if it has been pulled off
        distance = Vector3.Distance(gameObject.transform.position, gameObject.transform.parent.position);

        //If the tegaderm has been removed, destroy it and mark step as completed
        if (distance > 0.02f)
        {
            stepManager.UpdateChecklist(0);
            Destroy(gameObject);
        }
    }
}
