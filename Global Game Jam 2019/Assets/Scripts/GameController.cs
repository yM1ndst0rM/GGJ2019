using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public enum InputType { mouse, controller };
    InputType inputType = InputType.mouse;   

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetInputType(int index)
    {
        inputType = (InputType)index;
        
    }

    public InputType GetControlls()
    {
        return inputType;
    }
}
