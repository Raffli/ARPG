using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Prototype.NetworkLobby
{
    public class LobbyTopPanel : MonoBehaviour
    {

        public bool isInGame = false;
        public GameObject exitDialog;
        protected bool isDisplayed = true;
        protected Image panelImage;
        protected bool exitIsDisplayed = false;

        void Start()
        {
            panelImage = GetComponent<Image>();
        }


        void Update()
        {

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                print(exitIsDisplayed);
                ToggleVisibilityOfExitDialog(!exitIsDisplayed);
            }

        }

        public void ToggleVisibility(bool visible)
        {
            isDisplayed = visible;
            foreach (Transform t in transform)
            {
                t.gameObject.SetActive(isDisplayed);
            }

            if (panelImage != null)
            {
                panelImage.enabled = isDisplayed;
            }
        }

        public void ToggleVisibilityOfExitDialog(bool visible)
        {
            exitIsDisplayed = visible;
            foreach (Transform t in exitDialog.transform)
            {
                t.gameObject.SetActive(exitIsDisplayed);
            }
        }
    }
}