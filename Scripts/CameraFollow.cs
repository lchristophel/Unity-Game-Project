using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // JOUEUR
    public GameObject player;

    // DECALAGE DE TEMPS
    public float timeOffset;

    // DECALAGE DE POSITION
    public Vector3 posOffset;

    private Vector3 velocity;

    void Update()
    {
        // AFFECTE A LA CAMERA UN DEPLACEMENT LENT ENTRE LA POSITION DE LA CAMERA ET LE JOUEUR LORSQU'IL SE DEPLACE 
        transform.position = Vector3.SmoothDamp(transform.position, player.transform.position + posOffset, ref velocity, timeOffset);
    }
}
