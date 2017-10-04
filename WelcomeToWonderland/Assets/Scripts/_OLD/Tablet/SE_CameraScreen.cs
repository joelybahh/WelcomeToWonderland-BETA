using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SE_CameraScreen : MonoBehaviour {

    [Header("Camera Settings")]
    [SerializeField] private Camera m_frontCamera;
    [SerializeField] private Camera m_backCamera;
    [SerializeField] private int m_maxPhotos;
    [SerializeField] private float m_minDistToKeyItem;
    [SerializeField] private Text m_photoT;

    [Header("Render Texture Settings")]
    [SerializeField] private RenderTexture m_frontTex;
    [SerializeField] private RenderTexture m_backTex;

    [Header("Camera Material Settings")]
    [SerializeField] private MeshRenderer m_cameraScreen;

    [Header("Paper Print Settings")]
    [SerializeField] private Transform paperSpawnPoint;
    [SerializeField] private GameObject paperToSpawn;

    private Camera m_activeCamera;
    private RenderTexture m_activeRendText;
    private bool m_isBackCam;
    private int m_photosTaken;
    private string m_photoTag;
    public int PhotosTaken { get { return m_photosTaken; } }


	void Start () {
        m_isBackCam = true;
        m_activeRendText = m_backTex;
        m_activeCamera = m_backCamera;
	}

	void Update () {
        if (m_isBackCam) {
            // Set render tcture to be the back texture
            m_cameraScreen.material.mainTexture = m_backTex;
            m_activeRendText = m_backTex;
            m_activeCamera = m_backCamera;
        } else {
            // set the render tecture to be the front texture
            m_cameraScreen.material.mainTexture = m_frontTex;
            m_activeRendText = m_frontTex;
            m_activeCamera = m_frontCamera;
        }

        m_photoT.text = m_photosTaken + "/5 photos taken";
    }

    public void SwapCamera() {
        m_isBackCam = !m_isBackCam;
    }


    public IEnumerator TakePhoto() {

        yield return new WaitForEndOfFrame();
        RaycastHit hit;
        if(Physics.Raycast(m_activeCamera.transform.position, m_activeCamera.transform.forward, out hit, m_minDistToKeyItem)) {
            Debug.DrawRay(m_activeCamera.transform.position, m_activeCamera.transform.forward * m_minDistToKeyItem, Color.red, 2);
            if (hit.collider != null) {
                Debug.Log(hit.collider.tag);
                Debug.DrawRay(m_activeCamera.transform.position, m_activeCamera.transform.forward * m_minDistToKeyItem, Color.cyan, 2);
                m_photoTag = hit.collider.tag;
            }
        } 
        RenderTexture.active = m_activeRendText; // one camera is only ever rendering in a scene at once
                                                 // therefore, for this frame I have to update the active render texture,
                                                 // in order to be taking the correct photo.
        Texture2D photoText = new Texture2D(m_activeRendText.width, m_activeRendText.height, TextureFormat.ARGB32, false);
        photoText.ReadPixels(new Rect(0, 0, m_activeRendText.width, m_activeRendText.height), 0, 0);
        photoText.Apply();

        PrintPhoto(photoText);
        m_photosTaken++;

        //m_outputFrame.material.mainTexture = photoText;
    }

    void PrintPhoto(Texture2D a_photoText) {
        //TODO: Fix printing, or keep the photos just dropping to the floor? :P
        GameObject p = Instantiate(paperToSpawn, paperSpawnPoint.position, paperSpawnPoint.rotation);
        p.GetComponent<MeshRenderer>().material.mainTexture = a_photoText;
        p.tag = m_photoTag;
    }
}
