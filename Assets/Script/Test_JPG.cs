using System.Collections;
using System.Collections.Generic;
using NatSuite.Recorders;
using UnityEngine;
using NatSuite.Recorders.Inputs;
using Unity.VisualScripting;

public class Test_JPG : MonoBehaviour
{
    // private RenderTexture renderTexture;
    // [SerializeField] private Texture2D cachedScreenshot;

    // [SerializeField] private Camera arCamera; // AR 카메라 지정

    // private void Start()
    // {
    //     // AR 카메라 찾기
    //     // arCamera = Camera.main; // 필요한 카메라로 변경

    //     // RenderTexture 초기화
    //     renderTexture = new RenderTexture(Screen.width, Screen.height, 24);
    //     arCamera.targetTexture = renderTexture;

    //     StartCoroutine(DelayCaptrue());
    // }

    // private void OnDestroy()
    // {
    //     // 메모리 해제
    //     arCamera.targetTexture = null;
    //     Destroy(renderTexture);
    //     Destroy(cachedScreenshot);
    // }

    // private IEnumerator DelayCaptrue()
    // {
    //     yield return new WaitForSeconds(0.5f);
    //     CaptureScreenAndCache();
    // }

    // public void CaptureScreenAndCache()
    // {
    //     // 현재 화면을 RenderTexture에 캡처
    //     arCamera.Render();

    //     // RenderTexture의 내용을 Texture2D로 복사
    //     if (cachedScreenshot == null)
    //     {
    //         cachedScreenshot = new Texture2D(renderTexture.width, renderTexture.height);
    //     }
    //     RenderTexture.active = renderTexture;
    //     cachedScreenshot.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
    //     cachedScreenshot.Apply();
    //     RenderTexture.active = null;

    //     Debug.Log("Screenshot captured and cached in memory.");
    // }

    //     JPGRecorder jPGRecorder;
    //     private CameraInput cameraInput;

    //     [SerializeField] Texture2D texture2D;

    //     // Start is called before the first frame update
    //     void Start()
    //     {
    //         StartScreenShot();
    //     }

    //     // Update is called once per frame
    //     void Update()
    //     {
    // // ScreenCapture.CaptureScreenshot()
    //     }

    //     private async void StartScreenShot()
    //     {
    //         jPGRecorder = new JPGRecorder(720, 1280);
    //         cameraInput = new CameraInput(jPGRecorder, Camera.main);

    //         var path = await jPGRecorder.FinishWriting();
    //         Debug.Log(path);

    //         StartCoroutine(DelayTexture(path));
    //     }

    //     private IEnumerator DelayTexture(string path)
    //     {
    //         yield return new WaitForSeconds(0.025f);
    //         cameraInput.Dispose();
    //         yield return new WaitForSeconds(1f);

    //         var filePath = System.IO.Path.Combine(path, "1.png");

    //         byte[] imageBytes = System.IO.File.ReadAllBytes(filePath);

    //         texture2D = new Texture2D(1280, 720);
    //         texture2D.LoadImage(imageBytes);

    //     }

    [SerializeField] List<Texture2D> _ListTexture;

    private void Start()
    {
        StartCoroutine(CaptureScreenAndCache());
    }


    public IEnumerator CaptureScreenAndCache()
    {
        yield return new WaitForEndOfFrame();

        for (int i = 0; i < 2; i++)
        {

            Texture2D cachedScreenshot = null;

            // 현재 화면을 Texture2D로 캡처
            int width = Screen.width;
            int height = Screen.height;

            if (cachedScreenshot == null || cachedScreenshot.width != width || cachedScreenshot.height != height)
            {
                cachedScreenshot = new Texture2D(width, height, TextureFormat.RGB24, false);
            }

            // cachedScreenshot.ReadPixels(new Rect(0, 0, width, height), 0, 0);
            // cachedScreenshot.Apply();

            string fileName = "ScreenShot_" + i + ".png";
            string filePath = System.IO.Path.Combine(Application.persistentDataPath, fileName);

            ScreenCapture.CaptureScreenshot(filePath);

            yield return new WaitForSeconds(0.5f);

            byte[] imageByte = System.IO.File.ReadAllBytes(filePath);

            Debug.Log(filePath);

            cachedScreenshot = new Texture2D(720, 1280);
            cachedScreenshot.LoadImage(imageByte);


            _ListTexture.Add(cachedScreenshot);

            Debug.Log("Screenshot captured and cached in memory.");
        }

        yield return null;
    }
}
