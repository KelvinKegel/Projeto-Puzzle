using UnityEngine;

public class PlatformAttach : MonoBehaviour
{
    [SerializeField]
    private Player player;

    [SerializeField]
    private Platform platform;

    bool isInside;



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player.gameObject)
        {
            print("asdfadf");
            //player.transform.parent = transform.parent;
            //player.gravity = 0;
            isInside = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        print("sai");
        if (other.gameObject == player.gameObject)
        {
            //player.transform.parent = null;
            player.gravity = -12;
            isInside = false;
        }
    }
}