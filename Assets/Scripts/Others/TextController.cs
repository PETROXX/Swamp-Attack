using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//почему не анимация
public class TextController : MonoBehaviour
{
    [SerializeField] private float _sizeChangeSpeed;

    private bool _isSizeChanging;
    private bool _isIncreasing;

    private TMP_Text _text;

    private float _minSize;
    private float _maxSize;

    private void Start()
    {
        _text = GetComponent<TMP_Text>();
        _minSize = _text.fontSize * 0.8f;
        _maxSize = _text.fontSize * 1.2f;
        _isIncreasing = false;
    }

    public void SetText(string text, Color color)
    {
        _text.text = text;
        _text.color = color;
        _isSizeChanging = true;
    }

    public void SetText(string text, Color color, float timer)
    {
        _text.text = text;
        _text.color = color;
        _isSizeChanging = true;
        StartCoroutine(ClearTextTimer(timer));
    }

    private IEnumerator ClearTextTimer(float time)
    {
        yield return new WaitForSeconds(time);
        _text.text = "";
        _isSizeChanging = false;
    }

    private void Update()
    {
        if (!_isSizeChanging)
            return;

        TextAnimation();
    }

    private void TextAnimation()
    {
        if(_isIncreasing)
        {
            _text.fontSize = Mathf.MoveTowards(_text.fontSize, _maxSize, _sizeChangeSpeed * Time.deltaTime);
            if (_text.fontSize >= _maxSize)
                _isIncreasing = false;
        }
        else
        {
            _text.fontSize = Mathf.MoveTowards(_text.fontSize, _minSize, _sizeChangeSpeed * Time.deltaTime);
            if (_text.fontSize <= _minSize)
                _isIncreasing = true;
        }
    }
}
