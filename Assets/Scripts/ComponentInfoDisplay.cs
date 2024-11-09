//using UnityEngine;
//using TMPro; // Pour TextMeshPro

//public class ComponentInfoDisplay : MonoBehaviour
//{
//    public GameObject infoPanel; // Le panneau d'information
//    public TextMeshProUGUI infoText; // Texte pour afficher les détails

//    private void Start()
//    {
//        // Masquer le panneau d'information au début
//        infoPanel.SetActive(false);
//    }

//    void Update()
//    {
//        // Vérifie si un clic a été effectué
//        if (Input.GetMouseButtonDown(0))
//        {
//            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//            RaycastHit hit;

//            // Vérifie si le rayon touche un objet avec un Collider
//            if (Physics.Raycast(ray, out hit))
//            {
//                // Affiche le panneau et met à jour le texte
//                infoPanel.SetActive(true);
//                infoText.text = GetComponentInfo(hit.collider.gameObject.name); // Récupère le nom de l'objet touché
//            }
//        }
//    }

//    private string GetComponentInfo(string componentName)
//    {
//        // Retourne des informations basées sur le nom du composant
//        switch (componentName)
//        {
//            case "Egnines":
//                return " moteur ";
//            case "Fuselage":
//                return " fuselage";
//            case "Fuselage Details":
//                return "fuselage";
//            case "Landing Gear":
//                return " train d'atterrissage.";
//            case "Red Light":
//                return " feu rouge.";
//            case "Windows":
//                return " les fenêtres";
//            case "Winbox and Wings":
//                return " boîte à vent,";
//            case "Wings Details":
//                return "Détails des ailes,";
//            default:
//                return "";
//        }
//    }

//    // Méthode pour fermer le panneau d'informations
//    public void CloseInfoPanel()
//    {
//        infoPanel.SetActive(false);
//    }
//}


using UnityEngine;
using TMPro; // Pour TextMeshPro
using UnityEngine.SceneManagement;

public class ComponentInfoDisplay : MonoBehaviour
{
    public GameObject infoPanel; // Le panneau d'information
    public TextMeshProUGUI infoText; // Texte pour afficher les détails
    private GameObject currentSelectedObject; // Référence à l'objet actuellement sélectionné
    private Color originalColor; // La couleur d'origine de l'objet sélectionné

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
                // Si un objet a déjà été sélectionné, réinitialiser sa couleur
                if (currentSelectedObject != null)
                {
                    ResetObjectColor();
                }

                // Mettre à jour l'objet sélectionné
                currentSelectedObject = hit.collider.gameObject;

                // Change la couleur de l'objet sélectionné
                ChangeObjectColor(currentSelectedObject);

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
                return "Moteur: Le cœur de l'avion qui propulse l'appareil.";
            case "Fuselage":
                return "Fuselage: La structure principale de l'avion, qui abrite la cabine et les passagers.";
            case "Fuselage Details":
                return "Détails du fuselage: Informations supplémentaires sur la structure extérieure.";
            case "Landing Gear":
                return "Train d'atterrissage: Système qui permet à l'avion de se poser et de rouler au sol.";
            case "Red Light":
                return "Feu rouge: Indicateur visuel qui signale une alerte ou un danger.";
            case "Windows":
                return "Fenêtres: Les ouvertures permettant aux passagers de voir à l'extérieur.";
            case "Winbox and Wings":
                return "Boîte à vent et ailes: Composants permettant de stabiliser l'avion en vol.";
            case "Wings Details":
                return "Détails des ailes: Informations supplémentaires sur la conception des ailes.";
            default:
                return "Composant inconnu. Veuillez cliquer sur un composant valide.";
        }
    }

    // Méthode pour changer la couleur de l'objet sélectionné
    private void ChangeObjectColor(GameObject obj)
    {
        // Sauvegarder la couleur d'origine
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            originalColor = renderer.material.color; // Sauvegarde de la couleur initiale
            renderer.material.color = Color.red; // Changer la couleur (par exemple en rouge)
        }
    }

    // Méthode pour réinitialiser la couleur de l'objet
    private void ResetObjectColor()
    {
        if (currentSelectedObject != null)
        {
            Renderer renderer = currentSelectedObject.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = originalColor; // Restaure la couleur d'origine
            }
        }
    }

    // Méthode pour fermer le panneau d'informations
    public void CloseInfoPanel()
    {
        infoPanel.SetActive(false);
        infoText.text = ""; // Réinitialiser le texte lorsque le panneau est fermé
    }
    public void RetourMenuu()
    {
        // Code pour retourner au menu principal, exemple :
        SceneManager.LoadScene("Scene plane 1");
    }
}

