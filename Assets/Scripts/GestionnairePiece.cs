using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class GestionnairePiece : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI textePiece;
    private int nombrePiece;

    [SerializeField]
    private int TempsPause = 5;
    public event Action<bool> evenementPause;

    private void Start()
    {
        PlayerInput controles = ControleurJeu.Instance.Controles;
        controles.actions.FindAction("Pause").performed += Pause;
    }

    private void OnDestroy()
    {
        if (ControleurJeu.Instance == null)
            return;

        PlayerInput controles = ControleurJeu.Instance.Controles;

        if (controles == null)
            return;

        controles.actions.FindAction("Pause").performed -= Pause;
    }

    private void Awake()
    {
        nombrePiece = 0;
    }

    /// <summary>
    /// lance une evenement qui indique que le mouvement doit arreter
    /// </summary>
    /// <param name="contexte"></param>
    private void Pause(InputAction.CallbackContext contexte)
    {
        if(nombrePiece >= 5)
        {

            nombrePiece -= 5;
            ControleurJeu controleurJeu = ControleurJeu.Instance;
            controleurJeu.SurfaceMouvement = false;
            evenementPause?.Invoke(false);
            StartCoroutine(CoroutineSortiePause());
            Actualiser();
        }
    }

    /// <summary>
    /// attend le temps desirer et lance un evenement qui indique de relancer l'animation
    /// </summary>
    /// <returns></returns>
    private IEnumerator CoroutineSortiePause()
    {

        yield return new WaitForSeconds(TempsPause);
        evenementPause?.Invoke(true);
        ControleurJeu controleurJeu = ControleurJeu.Instance;
        controleurJeu.SurfaceMouvement = true;
    }


    public void AjouterPiece()
    {

        nombrePiece++;
        Actualiser();
    }
    private void Actualiser()
    {
        textePiece.text = $"Piece : {nombrePiece}";
    }
}
