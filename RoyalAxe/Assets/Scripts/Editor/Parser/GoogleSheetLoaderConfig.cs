using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Core.EditorCore.Parser
{
    /**
     * Содержит все данные для получения гуглтаблички.
     * и дальнейшая передача данных в парсер и сохранение.
     */
    [CreateAssetMenu(menuName = "Utility/GoogleSheetLoader", fileName = "GoogleSheetLoader")]
    public class GoogleSheetLoaderConfig : ScriptableObject
    {
        [Serializable]
        public class UpdatePages
        {
            [ReadOnly] public string PageName;
            public FolderState FolderState;
        }

        public string Title => name;
        public string SheetId => _sheetId;


        [SerializeField, Tooltip("Гугл табличка")]
        private string _sheetId = "";

        [SerializeReference] private IGoogleSheetDataToGameConfigConverter _configUpdater;

        public IReadOnlyList<UpdatePages> Pages => _pages;

        [SerializeField] private List<UpdatePages> _pages;

        [Button, EnableIf("_sheetId", "")]
        private void ShowUpdateWindow()
        {
            UpdateConfigFromGoogleSheetWindow.Open(this);
        }


        public void HandleGetNewPages(IEnumerable<string> availablePages)
        {
            var pagesFrom = availablePages.ToList();
            foreach (var savedPageData in _pages) // обходим текущие папки
            {
                savedPageData.FolderState = savedPageData.FolderState.UnsetFlag(FolderState.Missed);

                savedPageData.FolderState = pagesFrom.Contains(savedPageData.PageName) // если страница есть
                    ? savedPageData.FolderState.UnsetFlag(FolderState.New)             
                    : savedPageData.FolderState.SetFlag(FolderState.Missed);           
                pagesFrom.Remove(savedPageData.PageName);                              // удаляем папку из списка
            }

            // итого у нас остались только новые

            foreach (var page in pagesFrom) _pages.Add(new UpdatePages {PageName = page, FolderState = FolderState.New | FolderState.Update});
        }


        public void UpdateNewCellData(GoogleSheetGameData[] result)
        {
            if (_configUpdater == null) return;

            try
            {
                _configUpdater.ParseSheetData(result);
            }
            catch (Exception e)
            {
                Debug.LogError("Ups. Something go wrong");
                Debug.LogException(e);
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}