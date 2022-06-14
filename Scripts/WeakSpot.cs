using UnityEngine;

public class WeakSpot : MonoBehaviour
{
    // OBJET A DETRUIRE (A DEFINIR DANS UNITY)
    public GameObject objectToDestroy;

    // FONCTION QUI VA SE DECLENCHER LORSQUE LE JOUEUR ENTRERA EN COLLISION AVEC L'ENNEMI
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // SI APRES COMPARAISON, L'ELEMENT ENTRER DANS LA ZONE DE COLLISION EST LE JOUEUR
        if(collision.CompareTag("Player"))
        {
            // ALORS ON DETRUIT L'OBJET
            Destroy(objectToDestroy);
        }
    }
}
