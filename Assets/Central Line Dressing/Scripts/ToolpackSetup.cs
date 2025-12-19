using System.Collections;
using UnityEngine;
using Oculus.Interaction;

public class ToolpackSetup : MonoBehaviour
{
    public GrabInteractable grabInteractable;
    public Slider slider;

    public SkinnedMeshRenderer mesh;

    public GameObject[] tools;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Puts all of the tools into the tool package (under Child "Tool Store" to prevent tools from warping local scale) until it is opened
        for (int i = 0; i < tools.Length; i++)
        {
            tools[i].transform.parent = gameObject.transform.GetChild(0).transform;
            tools[i].GetComponentInChildren<GrabInteractable>().enabled = false;
        }

        mesh.SetBlendShapeWeight(0, 1);
        mesh.SetBlendShapeWeight(1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        mesh.SetBlendShapeWeight(0, slider.value * 100);
        mesh.SetBlendShapeWeight(1, slider.value * 100);

        if (slider.value >= 1)
        {
            slider.gameObject.SetActive(false);

            //If the package is opened, takes all of the tools out and destroys the packaging
            for (int i = 0; i < tools.Length; i++)
            {
                tools[i].transform.parent = null;
                tools[i].GetComponentInChildren<GrabInteractable>().enabled = true;
            }

            gameObject.SetActive(false);
        }    
    }
}
