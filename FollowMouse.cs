using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    private void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {

        if(BaseGameManager.instance.hasStarted())
        {

            Cursor.visible = false;
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0f, 0f, 1f);
            if(BaseGameManager.instance.roundCompleted())
            {
                Cursor.visible = true;
            }
        }
        else
        {
            Cursor.visible = true;
        }
    }
}
