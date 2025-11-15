using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoTimelineController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public Slider timelineSlider;

    private bool isDragging;

    void Start()
    {
        // Cuando el video tiene datos listos
        videoPlayer.prepareCompleted += OnVideoPrepared;

        // Preparar el video
        videoPlayer.Prepare();
    }

    void OnVideoPrepared(VideoPlayer vp)
    {
        // Configurar slider según la duración
        timelineSlider.minValue = 0;
        timelineSlider.maxValue = (float)vp.length;
    }

    void Update()
    {
        if (videoPlayer.isPlaying && !isDragging)
        {
            timelineSlider.value = (float)videoPlayer.time;
        }
    }

    // Cuando el usuario empieza a arrastrar
    public void StartDrag()
    {
        isDragging = true;
    }

    // Cuando termina de arrastrar
    public void EndDrag()
    {
        isDragging = false;
        videoPlayer.time = timelineSlider.value;
    }
}

