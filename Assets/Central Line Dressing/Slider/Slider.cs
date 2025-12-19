using UnityEngine;

public class Slider : MonoBehaviour
{
    //Pieces of the Slider
    public GameObject sliderKnob;
    public Transform StartPoint;
    public Transform EndPoint;

    //Tracks the slider knob's 
    public float value;

    // Update is called once per frame
    void Update()
    {
        value = Mathf.InverseLerp(StartPoint.position.x, EndPoint.position.x, sliderKnob.transform.position.x);
    }
}