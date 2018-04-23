using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScreenManager : MonoBehaviour {

    [SerializeField]
    Transform hero;
    [SerializeField]
    GameObject cam;
    [SerializeField]
    Text StoryText;
    [SerializeField]
    GameObject Overlay;
    [SerializeField]
    ScreenShake screenControl;

    Bounds cameraBounds;
    float cameraWidth;
    public int levelCounter = 0;
    public int debugLevel = 0;
    Image overlayImage;
    Text overlayText;
    public bool end = false;
    HeroController hc;

    public static Bounds OrthographicBounds(Camera camera)
    {
        float screenAspect = (float)Screen.width / (float)Screen.height;
        float cameraHeight = camera.orthographicSize * 2;
        Bounds bounds = new Bounds(
            camera.transform.position,
            new Vector3(cameraHeight * screenAspect, cameraHeight, 0));
        return bounds;
    }

    void UpdateBounds()
    {
        cameraBounds = OrthographicBounds(cam.GetComponent<Camera>());
    }

    void Start () {
        Screen.SetResolution(1024, 576, false);
        hc = hero.GetComponent<HeroController>();
        overlayImage = Overlay.GetComponent<Image>();
        overlayText = Overlay.GetComponentInChildren<Text>();
        UpdateBounds();
        cameraWidth = cameraBounds.extents.x;
        UpdateStory();
    }

    public void UpdateStory()
    {
        StoryText.text = GameText.Instance.GetStory(levelCounter);
    }

    public void ShowBadendOverlay()
    {
        overlayText.text = GameText.Instance.GetBadEnd();
        overlayImage.enabled = true;
        overlayText.enabled = true;
    }

    public void ShowDeathOverlay()
    {
        overlayText.text = GameText.Instance.GetDeathEnd();
        overlayImage.enabled = true;
        overlayText.enabled = true;
    }

    IEnumerator waitUntilEnd(float time)
    {
        yield return new WaitForSeconds(time);
        overlayImage.enabled = true;
        overlayText.enabled = true;
    }

    public void ShowEnd()
    {
        if (levelCounter < 8)
            return;
        hc.StopSound(HeroController.AudioType.bg);
        StartCoroutine("waitUntilEnd", 1f);
        hc.PlaySound(HeroController.AudioType.end);
        screenControl.VibrateForTime(1f);
        overlayText.fontSize = 20;
        overlayText.text = GameText.Instance.GetEndEnd();
        overlayText.color = Color.black;
        overlayImage.color = Color.red;

    }

    void HideOverlay()
    {
        overlayImage.enabled = false;
        overlayText.enabled = false;
    }

    bool IsOverlaying()
    {
        return overlayImage.enabled;
    }

    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Update () {
        if (levelCounter > 7)
        {
            hc.blockMovement = true;
        }

        if (hc.isDead == true)
        {
            if (IsOverlaying() == false)
                ShowDeathOverlay();
            if (Input.GetKeyDown(KeyCode.Return))
                RestartGame();
        }

        if (end == true)
        {
            if (Input.GetKeyDown(KeyCode.Return))
                RestartGame();
        }

        if (debugLevel > 0)
        {
            levelCounter = debugLevel;
            cam.transform.position = new Vector3(cameraWidth * 2 * debugLevel, 0, -10);
            hero.transform.position = Vector3.right * (cam.transform.position.x - cameraWidth / 2) + Vector3.up * hero.transform.position.y * 1.2f;
            debugLevel = 0;
            UpdateStory();
        }

        if (hero.position.x > cameraBounds.center.x + cameraWidth) {
            cam.transform.position += Vector3.right * cameraWidth * 2;
            levelCounter++;
            UpdateBounds();
            UpdateStory();
        }
        if (hero.position.x < cameraBounds.center.x - cameraWidth)
        {
            cam.transform.position += Vector3.left * cameraWidth * 2;
            levelCounter--;
            UpdateBounds();
            if (levelCounter < 0)
            {
                ShowBadendOverlay();
                end = true;
            }
            else
                UpdateStory();
        }
    }
}
