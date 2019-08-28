using UnityEngine;

public class PlatformAttach : MonoBehaviour
{
    [SerializeField]
    private Player player;

    [SerializeField]
    private Platform platform;

    bool isInside;


    private void LateUpdate()
    {
        if (isInside)
        {
            player.GetComponent<CharacterController>().Move(platform.deltaPos);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player.gameObject)
        {
            
            //player.transform.parent = transform.parent;
            player.gravity = -1;
            isInside = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        
        if (other.gameObject == player.gameObject)
        {
            //player.transform.parent = null;
            player.gravity = -12;
            isInside = false;
        }
    }
}