using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AudioCube : MonoBehaviour {
	[SerializeField]
	private AudioSource audioSource;

	private int sampleSize = 256;

	private float[] sample;

	float cubeScale = 0f;
	
	void Start () {
		sample = new float[sampleSize];
	}
	
	void Update () {
		UpdateByOutput();
		// UpdateBySpectrum();
	}

	private void UpdateByOutput () {
		audioSource.GetOutputData(sample, 1);
		var volume = sample.Select(x => x * x).Sum() / sample.Length;
		Debug.Log(volume);
		transform.localScale = Vector3.one + Vector3.one * volume * 100;
	}

	private void UpdateBySpectrum () {
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
}
