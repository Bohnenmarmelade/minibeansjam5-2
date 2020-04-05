using System;
using UnityEngine;

namespace Utils {
    public class MusicController : MonoBehaviour {
        public AudioClip gameMusic;
        public AudioClip comicMusic;
        private AudioClip _clipToPlay;

        private AudioSource _source;

        private bool _fadeIn = false;
        private bool _fadeOut = false;

        private float _fadeTime = 1f;
        private float _startVolume;

        private void Awake() {
            _source = GetComponent<AudioSource>();
            _clipToPlay = gameMusic;
        }

        private void OnEnable() {
            EventManager.StartListening(Events.MUSIC_GAME, OnPlayGameMusic);
            EventManager.StartListening(Events.SHOW_COMIC, OnPlayComicMusic);
            _startVolume = _source.volume;
            _source.volume = 0.0f;
            _fadeIn = true;
        }

        private void OnDisable() {
            EventManager.StopListening(Events.MUSIC_GAME, OnPlayGameMusic);
            EventManager.StopListening(Events.SHOW_COMIC, OnPlayComicMusic);
        }

        private void Update () {
            if (_fadeOut) {
                _source.volume -= _startVolume * Time.deltaTime / _fadeTime;
                if (_source.volume < 0.1) {
                    _fadeOut = false;
                    _fadeIn = true;
                    _source.clip = _clipToPlay;
                    _source.Play();
                }
            } else if (_fadeIn) {
                _source.volume += _startVolume * Time.deltaTime / _fadeTime;
                if (_source.volume > 0.8) {
                    _fadeIn = false;
                }
            }
        }

        private void OnPlayGameMusic(string payload) {
            _clipToPlay = gameMusic;
        }

        public void OnPlayComicMusic(string payload) {
            _clipToPlay = comicMusic;
        }
    }
}
