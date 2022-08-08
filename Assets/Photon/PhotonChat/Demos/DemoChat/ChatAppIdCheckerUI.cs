// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Exit Games GmbH"/>
// <summary>Demo code for Photon Chat in Unity.</summary>
// <author>developer@exitgames.com</author>
// --------------------------------------------------------------------------------------------------------------------


using UnityEngine;

<<<<<<< Updated upstream

using UnityEngine.UI;
#if PHOTON_UNITY_NETWORKING
using Photon.Pun;
#endif
=======
#if PHOTON_UNITY_NETWORKING
using UnityEngine.UI;
using Photon.Pun;
>>>>>>> Stashed changes

namespace Photon.Chat.Demo
{
    /// <summary>
    /// This is used in the Editor Splash to properly inform the developer about the chat AppId requirement.
    /// </summary>
    [ExecuteInEditMode]
    public class ChatAppIdCheckerUI : MonoBehaviour
    {
        public Text Description;
<<<<<<< Updated upstream
        public bool WizardOpenedOnce;   // avoid opening the wizard again and again

        // TODO: maybe this can run on Start(), not on Update()?!
        public void Update()
        {
            bool showWarning = false;
            string descriptionText = string.Empty;

            #if PHOTON_UNITY_NETWORKING
            showWarning = string.IsNullOrEmpty(PhotonNetwork.PhotonServerSettings.AppSettings.AppIdChat);
            if (showWarning)
            {
                descriptionText = "<Color=Red>WARNING:</Color>\nPlease setup a Chat AppId in the PhotonServerSettings file.";
            }
            #else
            ChatGui cGui = FindObjectOfType<ChatGui>(); // TODO: this could be a serialized reference instead of finding this each time

            showWarning = cGui == null || string.IsNullOrEmpty(cGui.chatAppSettings.AppIdChat);
            if (showWarning)
            {
                descriptionText = "<Color=Red>Please setup the Chat AppId.\nOpen the setup panel: Window, Photon Chat, Setup.</Color>";
                
                #if UNITY_EDITOR
                if (!WizardOpenedOnce)
                {
                    WizardOpenedOnce = true;
                    UnityEditor.EditorApplication.ExecuteMenuItem("Window/Photon Chat/Setup");
                }
                #endif
            }
            #endif

            this.Description.text = descriptionText;
        }
    }
}
=======

        public void Update()
        {
            if (string.IsNullOrEmpty(PhotonNetwork.PhotonServerSettings.AppSettings.AppIdChat))
            {
                if (this.Description != null)
                {
                    this.Description.text = "<Color=Red>WARNING:</Color>\nPlease setup a Chat AppId in the PhotonServerSettings file.";
                }
            }
            else
            {
                if (this.Description != null)
                {
                    this.Description.text = string.Empty;
                }
            }
        }
    }
}

#else

namespace Photon.Chat.Demo
{
    public class ChatAppIdCheckerUI : MonoBehaviour
    {
        // empty class. if PUN is not present, we currently don't check Chat-AppId "presence".
    }
}

#endif
>>>>>>> Stashed changes
