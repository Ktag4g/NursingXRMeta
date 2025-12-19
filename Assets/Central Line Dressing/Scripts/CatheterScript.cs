using UnityEngine;
using Oculus.Interaction;

public class CatheterScript : MonoBehaviour
{
    private OldTegadermRemoval tegadermScript;
    public PokeInteractable catheterButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Finds and assigns the tegaderm script
        tegadermScript = GameObject.Find("Old Tegaderm").GetComponent<OldTegadermRemoval>();

        tegadermScript.catheterIsHeldDown = false;
    }
    private void Update()
    {
        //A Meta poke interactor button is being used to determine if the catheter is being held down or not
        if (catheterButton.State == InteractableState.Select)
        {
            //If the user is touching the catheter, tells the tegaderm script that the catheter is now being held down
            tegadermScript.catheterIsHeldDown = true;
        }
        else if (catheterButton.State == InteractableState.Normal)
        {
            //If the user stops touching the catheter, tells the tegaderm script that the catheter is no longer being held down
            tegadermScript.catheterIsHeldDown = false;
        }
    }
}
