using UnityEngine;
using TMPro; // Pour TextMeshPro

public class ComponentInfoDisplay : MonoBehaviour
{
    public GameObject infoPanel; // Le panneau d'information
    public TextMeshProUGUI infoText; // Texte pour afficher les détails

    private void Start()
    {
        // Masquer le panneau d'information au début
        infoPanel.SetActive(false);
    }

    void Update()
    {
        // Vérifie si un clic a été effectué
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Vérifie si le rayon touche un objet avec un Collider
            if (Physics.Raycast(ray, out hit))
            {
                // Affiche le panneau et met à jour le texte
                infoPanel.SetActive(true);
                infoText.text = GetComponentInfo(hit.collider.gameObject.name); // Récupère le nom de l'objet touché
            }
        }
    }

    private string GetComponentInfo(string componentName)
    {
        // Retourne des informations basées sur le nom du composant
        switch (componentName)
        {
            case "Egnines":
                return " moteur ";
            case "Fuselage":
                return " fuselage";
            case "Fuselage Details":
                return "fuselage";
            case "Landing Gear":
                return " train d'atterrissage.";
            case "Red Light":
                return " feu rouge.";
            case "Windows":
                return " les fenêtres";
            case "Winbox and Wings":
                return " boîte à vent,";
            case "Wings Details":
                return "Détails des ailes,";
            default:
                return "";
        }
    }

    // Méthode pour fermer le panneau d'informations
    public void CloseInfoPanel()
    {
        infoPanel.SetActive(false);
    }
}
