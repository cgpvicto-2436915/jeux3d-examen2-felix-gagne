using UnityEngine;

public class Piece : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Boule boule))
        {
            boule.AjouterPiece();
            Destroy(gameObject);
        }
    }
}
