using UnityEngine;
// Захват видео в сиквенцию, v2.0. Назначать камере.
public class ScreenshotMovie_v2 : MonoBehaviour
{
  // В папку мы помещаем все скриншоты внутри.
  // Если папка существует, мы добавим числа, чтобы создать пустую папку.
  public string folder = "ScreenshotMovieOutput";
  public int frameRate = 25;
  public int sizeMultiplier = 1;
  public int startFrame = 2; // с какого кадра начинать захват (минимум со 2-го)
  public int endFrame = 100; // с какого кадра закончить захват и выключить play
  private string realFolder = "";

  void Start()
  {
    // Установить частоту кадров воспроизведения!
    // (реальное время больше не влияет на время)
    Time.captureFramerate = frameRate;

    // Найдите папку, которая еще не существует, добавив номера!
    realFolder = folder;
    int count = 1;

    while (System.IO.Directory.Exists(realFolder))
    {
      realFolder = folder + count;
      count++;
    }

    // Create the folder
    System.IO.Directory.CreateDirectory(realFolder);
  }

  void Update()
  {
    Debug.Log($"Current frame: {Time.frameCount}");

    if (Time.frameCount >= startFrame)
    {
      // name is "realFolder/shot 0005.png"
      var name = string.Format("{0}/shot {1:D04}.png", realFolder, Time.frameCount);
      // Capture the screenshot
      ScreenCapture.CaptureScreenshot(name, sizeMultiplier);
    }

    // отрендерили последний кадр, выходим.
    if (Time.frameCount == endFrame)
    {
      UnityEditor.EditorApplication.isPlaying = false;
    }
  }
}
