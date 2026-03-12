using TMPro;
using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(TMP_Text))]
public class TypewriterEffect : MonoBehaviour
{
    private TMP_Text _textbox;
    [Header("test string")]
    [SerializeField] private string testText;
    private int _currentVisibleCharacterIndex;
    private Coroutine _typewriterCoroutine;
    private WaitForSeconds _simpleDelay;
    private WaitForSeconds _interpunctuationDelay;

    [Header("Typewritter settings")]
    [SerializeField] private float charactersPerSecond = 20;
    [SerializeField] private float interpunctuationDelay = 0.5f;
    private void Awake()
    {
        _textbox = GetComponent<TMP_Text>();
        _simpleDelay = new WaitForSeconds(1 / charactersPerSecond);
        _interpunctuationDelay = new WaitForSeconds(interpunctuationDelay);
    }
    private void Start()
    {
        SetText(testText);
    }
    public void SetText(string text)
    {
        if (_typewriterCoroutine != null)
        {
            StopCoroutine(_typewriterCoroutine);
        }
        _textbox.text = text;
        _textbox.maxVisibleCharacters = 0;
        _currentVisibleCharacterIndex = 0;

        _typewriterCoroutine = StartCoroutine(routine: Typewriter());
    }
    private IEnumerator Typewriter()
    {
        TMP_TextInfo textInfo = _textbox.textInfo;
        while(_currentVisibleCharacterIndex < textInfo.characterCount + 1)
        {
            char character = textInfo.characterInfo[_currentVisibleCharacterIndex].character;
            _textbox.maxVisibleCharacters++;
            if (character == '?' || character == '!' || character == '.' || character == ',' || character == ':' || character == ';' || character == '-')
            {
                yield return _interpunctuationDelay;
            }
            else
            {
                yield return _simpleDelay;
            }
            _currentVisibleCharacterIndex++;
        }
    }
}


