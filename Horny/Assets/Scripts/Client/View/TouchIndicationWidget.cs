using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Dino.Client.Scripts.View.Ui
{
    public class TouchIndicationWidget : MonoBehaviour
    {
        public GameObject TouchPrefabElement;
        public RectTransform TouchTrail;
        public TrailRenderer TrailRenderer;
        private List<GameObject> _instancedItems = new List<GameObject>();
        private float _scaleFactor;
        private float _initialtime;

        public bool Enabled { get; set; } = true;

        private bool _mousePresent;

        public void Awake()
        {
            TouchPrefabElement.SetActive(false);
            _scaleFactor = EventSystem.current.GetComponent<Canvas>().scaleFactor;
            TrailRenderer = TouchTrail.GetComponent<TrailRenderer>();
            _initialtime = TrailRenderer.time;
        }

        private void Update()
        {
            if (!Enabled)
                return;

            TrailRenderer.time = _initialtime * Time.timeScale;

            var baseInput = EventSystem.current.currentInputModule.input;

            for (int i = 0; i < baseInput.touchCount; i++)
            {
                var t = baseInput.GetTouch(i);

                if (t.phase == TouchPhase.Began)
                {
                    DrawTouch(t.position);
                }

                if (t.fingerId == 0)
                {
                    if (t.phase == TouchPhase.Began)
                    {
                        TrailRenderer.emitting = false;
                        TouchTrail.gameObject.SetActive(false);
                        TouchTrail.anchoredPosition = t.position / _scaleFactor;
                        TouchTrail.gameObject.SetActive(true);
                        TrailRenderer.emitting = true;
                    }
                    else if (t.phase == TouchPhase.Moved)
                    {
                        TouchTrail.anchoredPosition = t.position / _scaleFactor;
                    }
                    else if (t.phase == TouchPhase.Ended || t.phase == TouchPhase.Canceled)
                    {
                        TouchTrail.anchoredPosition = t.position / _scaleFactor;
                    }
                }
            }
#if UNITY_EDITOR
            ProcessMouseInEditor(baseInput);
#endif
        }

        private void ProcessMouseInEditor(BaseInput baseInput)
        {
            if (baseInput.GetMouseButtonDown(0))
            {
                DrawTouch(baseInput.mousePosition);
            }

            if (baseInput.GetMouseButtonDown(0))
            {
                _mousePresent = true;
                TrailRenderer.emitting = false;
                TouchTrail.gameObject.SetActive(false);
                TouchTrail.anchoredPosition = baseInput.mousePosition / _scaleFactor;
                TouchTrail.gameObject.SetActive(true);
                TrailRenderer.emitting = true;
            }
            else if (baseInput.GetMouseButtonUp(0))
            {
                TouchTrail.anchoredPosition = baseInput.mousePosition / _scaleFactor;
                _mousePresent = false;
            }
            else if (baseInput.GetMouseButton(0) && _mousePresent)
            {
                TouchTrail.anchoredPosition = baseInput.mousePosition / _scaleFactor;
            }
        }

        private void DrawTouch(Vector2 tPosition)
        {
            _scaleFactor = EventSystem.current.GetComponent<Canvas>().scaleFactor;

            tPosition = tPosition / _scaleFactor;
            GameObject _instanced;

            if (_instancedItems.Count != 0)
            {
                _instanced = _instancedItems[0];
                _instancedItems.RemoveAt(0);
            }
            else
            {
                _instanced = GameObject.Instantiate(TouchPrefabElement, this.transform, false);
            }

            _instanced.GetComponent<RectTransform>().anchoredPosition = tPosition;

            _instanced.SetActive(true);

            _instanced.transform.localScale = Vector3.one * 0.3f;
            var image = _instanced.GetComponent<Image>();
            var c = image.color;
            c.a = 1;
            image.color = c;

            DOTween.Sequence().SetUpdate(UpdateType.Normal, true)
                .Insert(0, image.DOFade(0.55f, 0f))
                .Append(_instanced.transform.DOScale(Vector3.one, 0.5f))
                .Insert(0.1f, image.DOFade(0, 0.5f))
                .OnComplete(() =>
                {
                    _instanced.SetActive(false);
                    _instancedItems.Add(_instanced);
                }).SetAutoKill(true);
        }
    }
}