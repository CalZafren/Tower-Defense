using UnityEngine;

public class CameraController : MonoBehaviour
{
    
    public float panSpeed = 30f;
    public float panBorderThickness = 10f;
    public float scrollSpeed = 5;
    public float minY = 10;
    public float maxY = 80;
    public float panLeft = -40;
    public float panRight = Screen.width + 40;
    public float panUp = Screen.height + 40;
    public float panDown = -40;

    // Update is called once per frame
    void Update()
    {
        if(!GameManager.gameIsOver && !GameManager.gameIsPaused){
            this.enabled = true;
            if(Input.GetKey("w")){
                transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
            }else if(Input.GetKey("s")){
                transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
            }else if(Input.GetKey("a")){
                transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
            }else if(Input.GetKey("d")){
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Vector3 pos = transform.position;
        pos.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        transform.position = pos;
        }
    }
}
