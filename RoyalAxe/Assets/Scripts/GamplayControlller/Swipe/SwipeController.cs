using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class SwipeController : MonoBehaviour
{
    public event Action<int> OnStartMoveFromIndexEvent
    {
        add
        {
            _onStartMoveFromIndexEvent -= value;
            _onStartMoveFromIndexEvent += value;
        }
        remove => _onStartMoveFromIndexEvent -= value;
    }
    public event Action<int> OnFinishMoveToIndexEvent
    {
        add
        {
            _onFinishMoveToIndexEvent -= value;
            _onFinishMoveToIndexEvent += value;
        }
        remove => _onFinishMoveToIndexEvent -= value;
    }

    private Action<int> _onStartMoveFromIndexEvent, _onFinishMoveToIndexEvent;

    [Header("Swipe"), Space(15), SerializeField]
    private GameObject _swipeConteiner;
    [SerializeField] private GameObject[] _lowBarElement;
    [SerializeField] private float _swipeOffset;
    private float _swipePositionX;
    private int _swipeElement = 2;
    private bool _swipeComplete = true;

    [Header("SwipeSetting"), SerializeField]
    private int _idEnableObj;
    [SerializeField] private GameObject _setActiveObject;

    [Header("SwipeSprite"), Space(15), SerializeField]
    private Image[] _lowBar;
    [SerializeField] private Image[] _lowBarFrame;

    [SerializeField] private Color _activeColor;
    [SerializeField] private Color _unactiveColor;

    [SerializeField] private Color _activeFrameColor;
    [SerializeField] private Color _unactiveFrameColor;

    private void Start()
    {
        UpdateImageLowBar(_swipeElement);
    }

    private void UpdateImageLowBar(int id)
    {
        for (int i = 0; i < _lowBar.Length; i++)
            if (i == id)
            {
                _lowBar[i].color      = _activeColor;
                _lowBarFrame[i].color = _activeFrameColor;
            }
            else
            {
                _lowBar[i].color      = _unactiveColor;
                _lowBarFrame[i].color = _unactiveFrameColor;
            }
    }

    [ButtonGroup("LeftRight")]
    public void LeftSwipe()
    {
        if (_swipeComplete)
        {
            _swipeComplete = false;
            if (_swipeElement >= 1)
            {
                _swipeElement--;
                DisableElementUi(_swipeElement);
                _swipePositionX -= _swipeOffset;
                MovingSequence(_swipeElement + 1, _swipeElement, _swipeConteiner.transform.DOLocalMoveX(_swipePositionX, 1));
                UpdateImageLowBar(_swipeElement);
            }
            else
            {
                _swipeComplete = true;
            }
        }
    }

    [ButtonGroup("LeftRight")]
    public void RightSwipe()
    {
        if (_swipeComplete)
        {
            _swipeComplete = false;
            if (_swipeElement < 4)
            {
                _swipeElement++;
                DisableElementUi(_swipeElement);
                _swipePositionX += _swipeOffset;
                MovingSequence(_swipeElement - 1, _swipeElement, _swipeConteiner.transform.DOLocalMoveX(_swipePositionX, 1));
                UpdateImageLowBar(_swipeElement);
            }
            else
            {
                _swipeComplete = true;
            }
        }
    }

    [Button]
    public void SwapElement(int id)
    {
        if (_swipeComplete)
        {
            DisableElementUi(id);

            _swipeElement = id;
            var localPos = _lowBarElement[id].transform.localPosition;
            _swipePositionX = localPos.x;
            MovingSequence(_swipeElement, id, _swipeConteiner.transform.DOLocalMove(localPos, 1));
            _swipeElement = id;

            UpdateImageLowBar(id);
        }
        else
        {
            _swipeComplete = true;
        }
    }

    [Button]
    private void DisableElementUi(int id)
    {
        if (_idEnableObj == id)
        {
            _setActiveObject.transform.DOLocalMoveX(-1000, 1).OnComplete(() => { _setActiveObject.SetActive(false); });
        }
        else
        {
            _setActiveObject.SetActive(true);
            _setActiveObject.transform.DOLocalMoveX(-570, 1);
        }
    }

    private Sequence MovingSequence(int current, int next, Tween moveTween)
    {
        return DOTween.Sequence()
                      .PrependCallback(() => { _onStartMoveFromIndexEvent?.Invoke(current); })
                      .Append(moveTween)
                      .AppendCallback(() => { _onFinishMoveToIndexEvent.Invoke(next); })
                      .OnComplete(() => _swipeComplete = true);
    }


#if UNITY_EDITOR
    [Button]
    private void UpdatePositionSwapContainer()
    {
        try
        {
            var size = _swipeConteiner.transform.childCount / 2 * -_swipeOffset;

            for (int i = 0; i < _swipeConteiner.transform.childCount; i++)
            {
                var cell = _swipeConteiner.transform.GetChild(i).gameObject;
                cell.transform.localPosition =  new Vector2(size, 0);
                size                         += _swipeOffset;
            }

            Debug.Log("Transform position cell, sucssesful ");
        }
        catch
        {
            Debug.LogError("Null referens asset");
        }
    }
#endif
}