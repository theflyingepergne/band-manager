using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class TypewriterEffect : MonoBehaviour
{
    //---References---//
    [Header("Typewriter Settings")]
    [SerializeField] private float charactersPerSecond = 35;
    [SerializeField] private float interpunctuationDelay = 0.3f;

    public bool currentlySkipping = false;

    // CHANGED: Property dynamically checks if typing is active instead of relying on a flaky manual boolean flag
    public bool isTyping => _textBox != null && _textBox.maxVisibleCharacters < _textBox.textInfo.characterCount;

    [Header("Skip options")]
    [SerializeField] private bool quickSkip = false; // Turned off by default since you want the normal speedup skip
    [SerializeField][Min(1)] private int skipSpeedup = 5;

    [SerializeField][Range(0.1f, 0.5f)] private float sendDoneDelay = 0.05f;

    //---Local References---//
    private TMP_Text _textBox;

    private int _currentVisibleCharacterIndex;
    private Coroutine _typewriterCoroutine;

    private WaitForSeconds _skipDelay;
    private WaitForSeconds _simpleDelay;
    private WaitForSeconds _punctuationDelay;
    private WaitForSeconds _textboxFullEventDelay;

    //---Events---//
    public static event System.Action CompleteTextRevealed;
    public static event System.Action<char> CharacterRevealed;

    //---Methods---//
    private void Awake()
    {
        _textBox = GetComponent<TMP_Text>();

        _simpleDelay = new WaitForSeconds(1 / charactersPerSecond);
        _punctuationDelay = new WaitForSeconds(interpunctuationDelay);
        _skipDelay = new WaitForSeconds(1 / (charactersPerSecond * skipSpeedup));
        _textboxFullEventDelay = new WaitForSeconds(sendDoneDelay);
    }

    public void StartTypewriter(string newText)
    {
        if (_typewriterCoroutine != null)
            StopCoroutine(_typewriterCoroutine);

        _textBox.text = newText;
        _textBox.ForceMeshUpdate();

        currentlySkipping = false; // Reset skip state for the fresh sentence
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
                currentlySkipping = false; // Safety fallback reset
                CompleteTextRevealed?.Invoke();
                yield break;
            }

            char character = textInfo.characterInfo[_currentVisibleCharacterIndex].character;

            // Jump text formatting tags instantly
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
        // 1. Guard Clause: If we are already speeding through, ignore further inputs
        if (currentlySkipping) return;

        // 2. Normal Skip Speedup path
        if (!quickSkip || !doSkip)
        {
            currentlySkipping = true;
            StartCoroutine(SkipSpeedupReset());
            return;
        }

        // 3. Quick Skip Instant Completion path
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
        // Wait gracefully until the typewriter finishes the line entirely before lowering the speed flag
        yield return new WaitUntil(() => _textBox.maxVisibleCharacters >= _textBox.textInfo.characterCount);
        currentlySkipping = false;
    }
}