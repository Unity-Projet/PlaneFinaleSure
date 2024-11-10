//using UnityEngine;
//using TMPro; // Pour TextMeshPro

//public class ComponentInfoDisplay : MonoBehaviour
//{
//    public GameObject infoPanel; // Le panneau d'information
//    public TextMeshProUGUI infoText; // Texte pour afficher les d�tails

//    private void Start()
//    {
//        // Masquer le panneau d'information au d�but
//        infoPanel.SetActive(false);
//    }

//    void Update()
//    {
//        // V�rifie si un clic a �t� effectu�
//        if (Input.GetMouseButtonDown(0))
//        {
//            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//            RaycastHit hit;

//            // V�rifie si le rayon touche un objet avec un Collider
//            if (Physics.Raycast(ray, out hit))
//            {
//                // Affiche le panneau et met � jour le texte
//                infoPanel.SetActive(true);
//                infoText.text = GetComponentInfo(hit.collider.gameObject.name); // R�cup�re le nom de l'objet touch�
//            }
//        }
//    }

//    private string GetComponentInfo(string componentName)
//    {
//        // Retourne des informations bas�es sur le nom du composant
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
//                return " les fen�tres";
//            case "Winbox and Wings":
//                return " bo�te � vent,";
//            case "Wings Details":
//                return "D�tails des ailes,";
//            default:
//                return "";
//        }
//    }

//    // M�thode pour fermer le panneau d'informations
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
    public TextMeshProUGUI infoText; // Texte pour afficher les d�tails
    private GameObject currentSelectedObject; // R�f�rence � l'objet actuellement s�lectionn�
    private Color originalColor; // La couleur d'origine de l'objet s�lectionn�

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
                // Si un objet a d�j� �t� s�lectionn�, r�initialiser sa couleur
                if (currentSelectedObject != null)
                {
                    ResetObjectColor();
                }

                // Mettre � jour l'objet s�lectionn�
                currentSelectedObject = hit.collider.gameObject;

                // Change la couleur de l'objet s�lectionn�
                ChangeObjectColor(currentSelectedObject);

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
                return "Moteur: Le c�ur de l'avion qui propulse l'appareil.";
            case "Fuselage":
                return "Fuselage: La structure principale de l'avion, qui abrite la cabine et les passagers.";
            case "Fuselage Details":
                return "D�tails du fuselage: Informations suppl�mentaires sur la structure ext�rieure.";
            case "Landing Gear":
                return "Train d'atterrissage: Syst�me qui permet � l'avion de se poser et de rouler au sol.";
            case "Red Light":
                return "Feu rouge: Indicateur visuel qui signale une alerte ou un danger.";
            case "Windows":
                return "Fen�tres: Les ouvertures permettant aux passagers de voir � l'ext�rieur.";
            case "Winbox and Wings":
                return "Bo�te � vent et ailes: Composants permettant de stabiliser l'avion en vol.";
            case "Wings Details":
                return "D�tails des ailes: Informations suppl�mentaires sur la conception des ailes.";
            default:
                return "Composant inconnu. Veuillez cliquer sur un composant valide.";
        }
    }

    // M�thode pour changer la couleur de l'objet s�lectionn�
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

    // M�thode pour r�initialiser la couleur de l'objet
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

    // M�thode pour fermer le panneau d'informations
    public void CloseInfoPanel()
    {
        infoPanel.SetActive(false);
        infoText.text = ""; // R�initialiser le texte lorsque le panneau est ferm�
    }
    public void RetourMenuu()
    {
        // Code pour retourner au menu principal, exemple :
        SceneManager.LoadScene("Scene plane 1");
    }
}

