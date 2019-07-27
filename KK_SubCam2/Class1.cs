#define DEBUG

#undef DEBUG


using System.Linq;

using System.Collections;

using System.ComponentModel;


using BepInEx;


using UnityEngine;

using UnityEngine.SceneManagement;

using ConfigurationManager;


#if DEBUG
    using BepInEx.Logging;
#endif



[BepInPlugin(nameof(KK_SubCam), nameof(KK_SubCam), "1.0")]
public class KK_SubCam : BaseUnityPlugin
{

    private Camera mainCamera;

    private GameObject subCameraObject;

    private Camera SubCamera;

    private bool SubCam_Enabled = false;



    #region Config properties
    public static SavedKeyboardShortcut SubCam_EnableKey { get; private set; }
    #endregion

    void Awake()
    {

        SubCam_EnableKey = new SavedKeyboardShortcut("SubCam key", this, new KeyboardShortcut(KeyCode.Keypad1));

    }

    void Update()
    {

        if (SubCam_EnableKey.IsDown())
        {

            SubCam_Enabled = !SubCam_Enabled;


            if (SubCam_Enabled)
            {

                mainCamera = Camera.main;

                SubCam_Init();

            }
            else
            {
                SubCam_Kill();
            }

        }

    }
    void SubCam_Init()
    {

        subCameraObject = GameObject.Instantiate(mainCamera.gameObject);

        SubCamera = subCameraObject.GetComponent<Camera>();

        SubCamera.CopyFrom(mainCamera);

        SubCamera.rect = new Rect(0.75f, 0.75f, 0.75f, 1.0f);



    }

    void SubCam_Kill()
    {

        SubCam_Enabled = false;

        mainCamera.enabled = true;

        if (subCameraObject != null)
        {
            GameObject.DestroyImmediate(subCameraObject);
        }

    }

    void OnDestroy()
    {
        if (SubCam_Enabled)
        {
            SubCam_Kill();
        }
    }




}