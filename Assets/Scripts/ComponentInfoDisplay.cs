using UnityEngine;
using TMPro; // Pour TextMeshPro

public class ComponentInfoDisplay : MonoBehaviour
{
    public GameObject infoPanel; // Le panneau d'information
    public TextMeshProUGUI infoText; // Texte pour afficher les d�tails

    private void Start()
    {
        // Masquer le panneau d'information au d�but
        infoPanel.SetActive(false);
    }

    void Update()
    {
        // V�rifie si un clic a �t� effectu�
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // V�rifie si le rayon touche un objet avec un Collider
            if (Physics.Raycast(ray, out hit))
            {
                // Affiche le panneau et met � jour le texte
                infoPanel.SetActive(true);
                infoText.text = GetComponentInfo(hit.collider.gameObject.name); // R�cup�re le nom de l'objet touch�
            }
        }
    }

    private string GetComponentInfo(string componentName)
    {
        // Retourne des informations bas�es sur le nom du composant
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
                return " les fen�tres";
            case "Winbox and Wings":
                return " bo�te � vent,";
            case "Wings Details":
                return "D�tails des ailes,";
            default:
                return "";
        }
    }

    // M�thode pour fermer le panneau d'informations
    public void CloseInfoPanel()
    {
        infoPanel.SetActive(false);
    }
}
