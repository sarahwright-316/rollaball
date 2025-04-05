using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player; //references the player GameObject's position
    private Vector3 offset; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        offset = transform.position - player.transform.position; // camera position - player position

        
    }

    // Update is called once per frame
    void LateUpdate() //runs after other updates are done 
    {
        //only matches players postion, not rotation of sphere as if it were its child
        transform.position = player.transform.position + offset; //camera moved into new pos each frame

    }
}
