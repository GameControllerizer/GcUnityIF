using UnityEngine;
using UnityEngine.InputSystem;

public class GameCtrl : MonoBehaviour
{
    GameObject text;
    Gamepad gamepad;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 30;
        
        text = GameObject.Find("Text");
        gamepad = Gamepad.current;

        if (gamepad == null)
        {
            Debug.Log("Gamepad not found");
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif    
        }
        }

    // Update is called once per frame
    void Update()
    {
        float r = gamepad.dpad.x.ReadValue() * -1.0f;
        text.transform.Rotate(new Vector3(0, 0, r));
    }
}
