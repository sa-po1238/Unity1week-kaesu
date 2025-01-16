using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance_AudioManager;
    private void Awake()
    {
        if (instance_AudioManager == null)
        {
            instance_AudioManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [SerializeField] private AudioData audioData;
    [SerializeField] private AudioSource SESource;
    [SerializeField] private AudioSource BGMSource;
    [SerializeField] private Slider SESlider;
    [SerializeField] private Slider BGMSlider;

    // Start is called before the first frame update
    void Start()
    {
        AudioSource[] tmp = this.GetComponents<AudioSource>();
        this.SESource = tmp[0];
        this.BGMSource = tmp[1];

        CheckOverlap(this.audioData.SE_Data, "SE_Data");
        CheckOverlap(this.audioData.BGM_Data, "BGM_Data");

        SetSESlider();
        SetBGMSlider();

        // SESliderのonValueChangedイベントにリスナーを追加
        if (SESlider != null)
        {
            SESlider.onValueChanged.AddListener(delegate { SetSEVolume(); });

            // PlayerPrefsから保存された音量を読み込み
            if (PlayerPrefs.HasKey("SEVolume"))
            {
                float savedVolume = PlayerPrefs.GetFloat("SEVolume");
                SESlider.value = savedVolume;
                SESource.volume = savedVolume;
            }
        }
        else
        {
            Debug.LogError("SESlider が設定されていません。");
        }

        // BGMSliderのonValueChangedイベントにリスナーを追加
        if (BGMSlider != null)
        {
            BGMSlider.onValueChanged.AddListener(delegate { SetBGMVolume(); });

            // PlayerPrefsから保存された音量を読み込み
            if (PlayerPrefs.HasKey("BGMVolume"))
            {
                float savedVolume = PlayerPrefs.GetFloat("BGMVolume");
                BGMSlider.value = savedVolume;
                BGMSource.volume = savedVolume;
            }
        }
        else
        {
            Debug.LogError("BGMSlider が設定されていません。");
        }
    }

    //オーディオIDが重複していないかを確認する
    private void CheckOverlap(List<Datum> data, string variable_name)
    {
        List<int> vs = new List<int>();
        for (int i = 0; i < data.Count; i++)
        {
            if (vs.Contains(data[i].id))
            {
                Debug.LogError(string.Format("{0} のID {1} が重複しています。", variable_name, data[i].id));
            }
            else
            {
                vs.Add(data[i].id);
            }
        }
    }

    //オーディオIDをindexに変換する
    public int ConvertIdIntoIndex(List<Datum> data, int id)
    {
        for (int index = 0; index < data.Count; index++)
        {
            if (id == data[index].id)
            {
                return index;
            }
        }

        Debug.LogError(string.Format("指定されたid {0} のデータは存在しません。", id));

        return -1;
    }

    public void PlaySE(int id)
    {
        int index = this.ConvertIdIntoIndex(this.audioData.SE_Data, id);
        this.SESource.clip = this.audioData.SE_Data[index].clip;
        this.SESource.volume = this.audioData.SE_Data[index].volume;
        this.SESource.Play();
    }

    public void StopSE()
    {
        this.SESource.Stop();
    }

    public void PauseSE()
    {
        this.SESource.Pause();
    }

    public void UnPauseSE()
    {
        this.SESource.UnPause();
    }

    public void SetSESlider()
    {
        SESlider.value = this.SESource.volume;
    }

    public void SetSEVolume()
    {
        if (SESource == null || SESlider == null) return;

        // Sliderの値をSEの音量に反映
        this.SESource.volume = SESlider.value;

        // PlayerPrefsに音量を保存
        PlayerPrefs.SetFloat("SEVolume", SESlider.value);
        PlayerPrefs.Save();
    }

    public void PlayBGM(int id)
    {
        int index = this.ConvertIdIntoIndex(this.audioData.BGM_Data, id);
        this.BGMSource.clip = this.audioData.BGM_Data[index].clip;
        this.BGMSource.volume = this.audioData.BGM_Data[index].volume;
        this.BGMSource.Play();
    }

    public void StopBGM()
    {
        this.BGMSource.Stop();
    }

    public void PauseBGM()
    {
        this.BGMSource.Pause();
    }

    public void UnPauseBGM()
    {
        this.BGMSource.UnPause();
    }

    public void TempoAdjustBGM(float tempo)
    {
        this.BGMSource.pitch = tempo;
    }

    public void SetBGMSlider()
    {
        BGMSlider.value = this.BGMSource.volume;
    }

    public void SetBGMVolume()
    {   
        if (BGMSource == null || BGMSlider == null) return;

        // Sliderの値をBGMの音量に反映
        this.BGMSource.volume = BGMSlider.value;

        // PlayerPrefsに音量を保存
        PlayerPrefs.SetFloat("BGMVolume", BGMSlider.value);
        PlayerPrefs.Save();
    }

}