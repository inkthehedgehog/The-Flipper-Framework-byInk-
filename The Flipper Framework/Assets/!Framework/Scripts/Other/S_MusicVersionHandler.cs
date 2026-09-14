using UnityEngine;

public class S_Control_MusicVersionHandler : MonoBehaviour
{
	public AudioSource NormalSource;
	public AudioSource DistortedSource;

	[Header("Crossfade")]
	public float FadeSeconds = 0.5f; // ajustable desde el Inspector

	private float _normalTargetVolume;
	private float _distortedTargetVolume;

	private bool _isBoostVersion;

	private Coroutine _normalFade;
	private Coroutine _distortedFade;

	private void Awake () {
		_normalTargetVolume = NormalSource.volume;
		_distortedTargetVolume = DistortedSource.volume;

		DistortedSource.volume = 0f;
	}

	public void SwitchToBoostVersion () {
		if (_isBoostVersion) return;
		_isBoostVersion = true;
		Debug.Log("Cambiando a boost");

		if (_normalFade != null) StopCoroutine(_normalFade);
		if (_distortedFade != null) StopCoroutine(_distortedFade);

		_normalFade = StartCoroutine(S_S_Objects.LerpAudioSourceVolume(NormalSource, FadeSeconds, 0f));
		_distortedFade = StartCoroutine(S_S_Objects.LerpAudioSourceVolume(DistortedSource, FadeSeconds, _distortedTargetVolume));
	}

	public void SwitchToNormalVersion () {
		if (!_isBoostVersion) return;
		_isBoostVersion = false;
		Debug.Log("Cambiando a normal");

		if (_normalFade != null) StopCoroutine(_normalFade);
		if (_distortedFade != null) StopCoroutine(_distortedFade);

		_normalFade = StartCoroutine(S_S_Objects.LerpAudioSourceVolume(NormalSource, FadeSeconds, _normalTargetVolume));
		_distortedFade = StartCoroutine(S_S_Objects.LerpAudioSourceVolume(DistortedSource, FadeSeconds, 0f));
	}
}