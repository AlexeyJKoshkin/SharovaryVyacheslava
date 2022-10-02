using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using Core.Parser;
using GameKit.Editor;
using GoogleSheetsToUnity;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;
using static GameKit.Editor.EditorGUIExtensions;
using Debug = UnityEngine.Debug;

namespace Core.EditorCore.Parser
{
    public class UpdateConfigFromGoogleSheetWindow : EditorWindow
    {
        public static UpdateConfigFromGoogleSheetWindow Open(GoogleSheetLoaderConfig current)
        {
            var window = GetWindow<UpdateConfigFromGoogleSheetWindow>();
            window.Init(current);
            window.Show();
            return window;
        }

        private GoogleSheetLoaderConfig _configUpdater;
        private OdinEditor _editor;

        private Vector2 _scrollPosition;

        //private ReorderableList _definitionProviders;
        private bool _isWorking;
        private string _label;
        private readonly List<GoogleSheetGameData> _cashe = new List<GoogleSheetGameData>();

        private GenericMenu _SelectingMainConfigUpdatingMenu;


        private void OnSelectConfigCallback(object userdata)
        {
            if (userdata is GoogleSheetLoaderConfig storage)
            {
                Init(storage);
            }
        }

        private void OnGUI()
        {
            DrawHeaderSelectedScriptableProvider();
            if (_editor == null)
            {
                EditorGUILayout.HelpBox("Клик на кнопку вверху", MessageType.Info);
            }
            else
            {
                EditorGUILayout.Space();
                DoButton($"Open {_configUpdater.Title}", OpenUrl);

                DoButton($"Update Pages {_configUpdater.Title}", UpdatePagesList);


                DrawMainGUIWindow();
                GUILayout.Label(_label);
            }
        }

        private void UpdatePagesList()
        {
            GetSheetState currentPageStatus = GetSheetState.Working;
            _label = $"{currentPageStatus} updatePageList page {_configUpdater.Title} ";

            SpreadsheetManager.GetPages(_configUpdater.SheetId, list =>
                                                                {
                                                                    _configUpdater.HandleGetNewPages(list.Select(o => o.title));
                                                                    EditorUtility.SetDirty(this);
                                                                });
        }

        private void OpenUrl()
        {
            string url = $"https://docs.google.com/spreadsheets/d/{_configUpdater.SheetId}";
            Application.OpenURL(url);
        }

        private void DrawMainGUIWindow()
        {
            using (new EditorGUI.DisabledScope(_isWorking))
            {
                DrawScrollGUI();
                DrawBtnCtrls();
            }
        }

        private void DrawScrollGUI()
        {
            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition, GUILayout.Height(position.height - 300));

            using (new GUILayout.VerticalScope("Box"))
            {
                for (int i = 0; i < _configUpdater.Pages.Count; i++)
                {
                    var pageItem = _configUpdater.Pages[i];

                    using (new EditorGUILayout.HorizontalScope())
                    {
                        DrawPageItem(pageItem);
                    }
                }
            }

            EditorGUILayout.EndScrollView();
        }

        private void DrawPageItem(GoogleSheetLoaderConfig.UpdatePages pageItem)
        {
            if (pageItem.FolderState.HasFlag(FolderState.Ignore))
            {
                return;
            }

            var tempBackColor = GUI.backgroundColor;
            GUI.backgroundColor = GetColor(pageItem.FolderState);

            using (new GUILayout.HorizontalScope())
            {
                EditorGUI.BeginChangeCheck();
                EditorGUILayout.Toggle(GUIContent.none, pageItem.FolderState.HasFlag(FolderState.Update), GUILayout.Width(20));
                if (EditorGUI.EndChangeCheck())
                {
                    pageItem.FolderState = pageItem.FolderState.ToggleFlag(FolderState.Update);
                }

                DoButton(pageItem.PageName, () => pageItem.FolderState = pageItem.FolderState.ToggleFlag(FolderState.Update), EditorStyles.boldLabel);
                GUI.enabled = pageItem.FolderState.HasFlag(FolderState.Update) && !pageItem.FolderState.HasFlag(FolderState.Missed);
                if (GUILayout.Button("Update"))
                {
                    UpdatePage(pageItem.PageName);
                }

                GUI.enabled = true;
            }

            GUI.backgroundColor = tempBackColor;
        }


        private Color GetColor(FolderState dataFolderState)
        {
            if (dataFolderState.HasFlag(FolderState.New))
            {
                return Color.green;
            }

            if (dataFolderState.HasFlag(FolderState.Missed))
            {
                return Color.red;
            }

            return GUI.backgroundColor;
        }

        private void DrawBtnCtrls()
        {
            using (new EditorGUILayout.HorizontalScope())
            {
                if (GUILayout.Button("Update selected"))
                {
                    UpdatePages(_configUpdater.Pages.Where(p => p.FolderState.HasFlag(FolderState.Update) && !p.FolderState.HasFlag(FolderState.Missed)).Select(o => o.PageName));
                }

                if (GUILayout.Button("Update all"))
                {
                    UpdatePages(_configUpdater.Pages.Where(f => !f.FolderState.HasFlag(FolderState.Missed) && !f.FolderState.HasFlag(FolderState.Ignore)).Select(o => o.PageName));
                }
            }
        }

        private async void UpdatePages(IEnumerable<string> pages)
        {
            _cashe.Clear();
            foreach (string pageName in pages)
            {
                _label = pageName;
                Repaint();
                var state = await UpdateData(_configUpdater.SheetId, pageName);

                //if (state != GetSheetState.Ok) _failedList.Add(uiAdapter);

                if (state == GetSheetState.TryReconnect)
                {
                    _label = "Обновление токена";
                    Repaint();
                    EditorApplication.update += Repaint;
                    await Task.Run(WaitGetGoogleToken);
                    EditorApplication.update -= Repaint;
                }
            }

            _configUpdater.UpdateNewCellData(_cashe.ToArray());
            _cashe.Clear();
            _isWorking = false;
            _label     = "Обновление закончено";
        }

        private async void UpdatePage(string pageName)
        {
            _isWorking = true;
            _label     = pageName;
            Repaint();
            var state = await UpdateData(_configUpdater.SheetId, pageName);

            if (state == GetSheetState.TryReconnect)
            {
                _label = "Обновление токена";
                Repaint();
                await Task.Run(WaitGetGoogleToken);

                state = await UpdateData(_configUpdater.SheetId, pageName);
            }

            _configUpdater.UpdateNewCellData(_cashe.ToArray());
            _cashe.Clear();
            _label = "Успешно обновлено";
            Repaint();

            _isWorking = false;
        }

        private void DrawHeaderSelectedScriptableProvider()
        {
            if (GUILayout.Button("Выбрать конфиг для обновления"))
            {
                if (_SelectingMainConfigUpdatingMenu == null)
                {
                    _SelectingMainConfigUpdatingMenu = new GenericMenu();
                    var assets = EditorUtils.FindAssets<GoogleSheetLoaderConfig>().ToList();

                    foreach (var sheetConfigUpdater in assets)
                        _SelectingMainConfigUpdatingMenu.AddItem(new GUIContent(sheetConfigUpdater.name), sheetConfigUpdater == _configUpdater,
                                                                 OnSelectConfigCallback, sheetConfigUpdater);
                }

                _SelectingMainConfigUpdatingMenu.ShowAsContext();
            }
        }


        private void WaitGetGoogleToken()
        {
            bool  isDone = false;
            Timer timer  = new Timer(60000);
            timer.Elapsed += (sender, args) => isDone = true;
            timer.Start();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            while (!GoogleAuthrisationHelper.GetTokenFlag && !isDone) _label = $"Обновление токена {60 - stopwatch.ElapsedMilliseconds / 1000f}";

            if (isDone)
            {
                Debug.LogError("Failed Get Token");
            }
        }


        internal Task<GetSheetState> UpdateData(string sheetID, string pageName)
        {
            //  var definitionTypeProvider = uiAdapter.Provider;
            if (string.IsNullOrEmpty(pageName))
            {
                return Task.Run(() => GetSheetState.Error);
            }

            //  Type defItemType = definitionTypeProvider.DefinitionType;
            // if (defItemType == null) return Task.Run(() => GetSheetState.Error);
            GetSheetState currentPageStatus = GetSheetState.Working;
            _label = $"{currentPageStatus} page {pageName} ";
            var requestParameters = new GSTU_Search(sheetID, pageName);
            SpreadsheetManager.Read(requestParameters, sheet =>
                                                       {
                                                           if (sheet.Cells.Count == 0)
                                                           {
                                                               _label = "REFRESHING TOKEN";
                                                               Repaint();
                                                               currentPageStatus = GetSheetState.TryReconnect;
                                                               return;
                                                           }

                                                           _label = $"Parsing {pageName} to SOMEONE";

                                                           var values = sheet.rows.primaryDictionary.Values.ToList();

                                                           var result = new GoogleSheetGameData
                                                           {
                                                               SheetName = "!!!Вставить имя!!!",
                                                               PageName  = pageName,
                                                               RowNames  = values[0].Select(o => o.value).ToArray()
                                                           };
                                                           values.RemoveAt(0);
                                                           result.Cells = values.Select(o => o.Select(e =>
                                                                                                      {
                                                                                                          ICellValue cell = new CellValue {ColumnName = e.columnId, Value = e.value};
                                                                                                          return cell;
                                                                                                      }).ToList()).ToList();

                                                           _cashe.Add(result);


                                                           /*var updater =
                                                             (IScriptableRepositoryDefUpdater) Activator.CreateInstance(
                                                                                                                        typeof(GenericRepositoryDefUpdater<>).MakeGenericType(defItemType));*/
                                                           //  updater.ParseSheetTo(sheet, definitionTypeProvider);
                                                           currentPageStatus = GetSheetState.Ok;
                                                       });

            return Task.Run(() =>
                            {
                                bool  isDone = false;
                                Timer timer  = new Timer(60000);
                                timer.Elapsed += (sender, args) => isDone = true;
                                while (currentPageStatus == GetSheetState.Working && !isDone) { }

                                return isDone ? GetSheetState.Error : currentPageStatus;
                            });
        }

        private void Init(GoogleSheetLoaderConfig current)
        {
            _configUpdater = current;
            titleContent   = new GUIContent($"Update {current.Title} ");
            _editor        = Editor.CreateEditor(current) as OdinEditor;
        }

        internal enum GetSheetState
        {
            Working,
            Error,
            Ok,
            TryReconnect
        }
    }

    public class CellValue : ICellValue
    {
        public string Value { get; set; }
        public string ColumnName { get; set; }
    }
}