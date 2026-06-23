using System.Collections;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class TypewriterEffect : MonoBehaviour
{
    //---References---//
    [Header("Typewriter Settings")]
    [SerializeField] private bool autoPlay = false;
    [SerializeField] private float charactersPerSecond = 35;
    [SerializeField] private float interpunctuationDelay = 0.3f;

    public bool currentlySkipping = false;
    public bool IsTyping => _textBox != null && _textBox.maxVisibleCharacters < _textBox.textInfo.characterCount;

    [Header("Skip options")]
    [SerializeField] private bool quickSkip = false;
    [SerializeField][Min(1)] private int skipSpeedup = 5;

    [SerializeField][Range(0.1f, 0.5f)] private float sendDoneDelay = 0.05f;

    //---Local References---//
    private TMP_Text _textBox;
    private string text;

    private int _currentVisibleCharacterIndex;
    private Coroutine _typewriterCoroutine;

    private WaitForSeconds _skipDelay;
    private WaitForSeconds _simpleDelay;
    private WaitForSeconds _punctuationDelay;
    private WaitForSeconds _textboxFullEventDelay;

    //---Events---//
    public event System.Action CompleteTextRevealed;
    public event System.Action<char> CharacterRevealed;

    //---Initialisation---//
    private void Awake()
    {
        _textBox = GetComponent<TMP_Text>();

        _simpleDelay = new WaitForSeconds(1 / charactersPerSecond);
        _punctuationDelay = new WaitForSeconds(interpunctuationDelay);
        _skipDelay = new WaitForSeconds(1 / (charactersPerSecond * skipSpeedup));
        _textboxFullEventDelay = new WaitForSeconds(sendDoneDelay);
    }

    private void OnEnable()
    {
        if (_textBox != null) text = _textBox.text;

        if (autoPlay)
        {
            _textBox.text = "";
            StartTypewriter(text);
        }
    }

    //---Typewriter---//
    public void StartTypewriter(string newText)
    {
        if (_typewriterCoroutine != null)
            StopCoroutine(_typewriterCoroutine);

        _textBox.text = newText;
        _textBox.ForceMeshUpdate();

        currentlySkipping = false;
        _textBox.maxVisibleCharacters = 0;
        _currentVisibleCharacterIndex = 0;

        _typewriterCoroutine = StartCoroutine(Typewriter());
    }

    private IEnumerator Typewriter()
    {
        TMP_TextInfo textInfo = _textBox.textInfo;

        while (_currentVisibleCharacterIndex < textInfo.characterCount + 1)
        {
            var lastCharacterIndex = textInfo.characterCount - 1;

            if (_currentVisibleCharacterIndex >= lastCharacterIndex)
            {
                _textBox.maxVisibleCharacters = textInfo.characterCount;
                yield return _textboxFullEventDelay;
                currentlySkipping = false;
                CompleteTextRevealed?.Invoke();
                yield break;
            }

            char character = textInfo.characterInfo[_currentVisibleCharacterIndex].character;

            // ignore style tags
            if (character == '<')
            {
                while (_currentVisibleCharacterIndex < textInfo.characterCount &&
                    textInfo.characterInfo[_currentVisibleCharacterIndex].character != '>')
                {
                    _currentVisibleCharacterIndex++;
                }
                _currentVisibleCharacterIndex++;

                if (_currentVisibleCharacterIndex >= textInfo.characterCount) continue;

                character = textInfo.characterInfo[_currentVisibleCharacterIndex].character;
            }

            _textBox.maxVisibleCharacters = _currentVisibleCharacterIndex + 1;

            if (!currentlySkipping && (character == '.' || character == ','))
            {
                yield return _punctuationDelay;
            }
            else
            {
                yield return currentlySkipping ? _skipDelay : _simpleDelay;
            }

            CharacterRevealed?.Invoke(character);
            _currentVisibleCharacterIndex++;
        }

        currentlySkipping = false;
        _typewriterCoroutine = null;
    }

    public void Skip(bool doSkip = false)
    {
        // If we are already speeding through, ignore further inputs
        if (currentlySkipping) return;

        // If we are skipping
        if (!quickSkip || !doSkip)
        {
            currentlySkipping = true;
            StartCoroutine(SkipSpeedupReset());
            return;
        }

        // Quick Skip Instant Completion path
        if (_typewriterCoroutine != null)
        {
            StopCoroutine(_typewriterCoroutine);
        }

        _textBox.maxVisibleCharacters = _textBox.textInfo.characterCount;
        currentlySkipping = false;
        CompleteTextRevealed?.Invoke();
    }

    private IEnumerator SkipSpeedupReset()
    {
        // Wait until the typewriter finishes the line before setting currentlySkipping to false
        yield return new WaitUntil(() => _textBox.maxVisibleCharacters >= _textBox.textInfo.characterCount);
        currentlySkipping = false;
    }
}