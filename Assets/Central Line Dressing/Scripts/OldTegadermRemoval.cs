using UnityEngine;

public class OldTegadermRemoval : MonoBehaviour
{
    private StepManager stepManager;
    
    public Slider slider;
    public SkinnedMeshRenderer mesh;

    //private float distance;
    public bool catheterIsHeldDown;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Find and assign Step Manager
        stepManager = GameObject.Find("Step Manager").GetComponent<StepManager>();
    }

    // Update is called once per frame
    void Update()
    {
        /*Calculate the distance the old tegaderm is to the body to decide if it has been pulled off
        distance = Vector3.Distance(gameObject.transform.position, gameObject.transform.parent.position);
        */

        mesh.SetBlendShapeWeight(0, slider.value * 50);
        mesh.SetBlendShapeWeight(1, slider.value * 50);
        mesh.SetBlendShapeWeight(2, slider.value * 50);

        //If the tegaderm has been removed, disable it and mark step as completed
        if (slider.value >= 1)
        {
            slider.gameObject.SetActive(false);

            stepManager.UpdateChecklist(0);
            gameObject.SetActive(false);

            //Check to see if the catheter is being held down while the tegaderm is being removed
            //NOTE: MAYBE ADD A VISUAL FOR THAT THE CATHETER MOVES IF NOT HELD DOWN PROPERLY?
            if (catheterIsHeldDown)
            {
                //If the catheter is being held down when the tegaderm is removed, then mark that step complete
                stepManager.UpdateChecklist(1);
            }
        }
    }
}
