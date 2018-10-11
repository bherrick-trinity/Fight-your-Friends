using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {

	public AudioClip jump;
	public AudioClip punch;
	private AudioSource aud;

	// Use this for initialization
	void Start () {
		aud = GetComponent<AudioSource> ();
	}

	public void PunchSound() {
		;

	}

	public void JumpSound() {

		aud.PlayOneShot (jump, 1f);
	}
}
