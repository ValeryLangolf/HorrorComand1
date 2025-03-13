using UnityEngine;

public class Exiter : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }
}