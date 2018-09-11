using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AudioCube : MonoBehaviour
{
  [SerializeField]
  private AudioSource audioSource;

  private int sampleSize = 256;

  private float[] sample;

  float cubeScale = 0f;

	private AudioClip audioClip;
	private float max;

  void Start()
  {
    sample = new float[sampleSize];
		audioClip = audioSource.clip;
		var data = new float[audioClip.samples * audioClip.channels];
		audioClip.GetData(data, 0);
		Debug.Log(data.Max());
		max = data.Max();
		// foreach(float e in data)
		// {
		// 	Debug.Log(e);
		// }
  }

  void Update()
  {
    UpdateByOutput();
    // UpdateBySpectrum();
  }

  private void UpdateByOutput()
  {
    audioSource.GetOutputData(sample, 1);
		// if (max * 0.9 <= sample.Max()) Debug.Log("aaa");
    var volume = sample.Select(x => x * x).Sum() / sample.Length;
    // Debug.Log(volume);
    // transform.localScale = Vector3.one + Vector3.one * volume * 100;
		UpdateScale(volume * 10);
  }

  private void UpdateBySpectrum()
  {
    audioSource.GetSpectrumData(sample, 0, FFTWindow.BlackmanHarris);
    var deltaFreq = AudioSettings.outputSampleRate / (sampleSize * 2);
    // for (int i = 0; i < sampleSize; i++) {
    // 	var freq = deltaFreq * i;
    // 	if (freq <= 7350) cubeScale += sample[i];
    // }
    // cubeScale *= 10;

    // Debug.Log(sample.Max());
    float max = sample.Max();
    if (max > 0.015) cubeScale = max * 20f;
    else cubeScale = 0f;
    transform.localScale = Vector3.one + Vector3.one * cubeScale;
  }

  private void UpdateScale(float value)
	{
		var scale = Mathf.Lerp(1, 2f, value);
		transform.localScale = new Vector3(scale, scale, scale);
	}
}
