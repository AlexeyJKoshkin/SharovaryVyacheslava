using System;
using Core.Data.Provider;
using RoyalAxe.CoreLevel;
using RoyalAxe.GameEntitas;
using TMPro;
using UnityEngine;

namespace RoyalAxe 
{
    public class DevelopSelectLevelParamsUIView : UIViewComponent 
    { 
    //TMP_Dropdown   
    }

    [Serializable]
    public class SomeView
    {
        [SerializeField]
        public TMP_Dropdown Dropdown;

        public int Level
        {
            get { return string.IsNullOrEmpty(_input.text) ? 0 : int.Parse(_input.text); }
            set { _input.text = value.ToString(); }
        }

        [SerializeField]
        private TMP_InputField _input;
    }

    public class DevelopViewController
    {
        public IDataStorage Storage;
        private SomeView _view;
        private GameRootLoopContext _rootLoopContext;

        void Init()
        {

        }

    }



}