// (c) Copyright Cleverous 2022. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cleverous.VaultSystem;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Cleverous.VaultDashboard
{
    public class DatabaseBuilder : IPreprocessBuildWithReport
    {
        private static bool DUMPLOG = false;
        private static StringBuilder dump;

        public int callbackOrder => 100;
        public void OnPreprocessBuild(BuildReport report)
        {
            Reload();
        }

        public static void CallbackAfterScriptReload()
        {
            if (Vault.Db == null)
            {
                Debug.LogWarning("Is this the first time loading Vault? You may want to restart the editor and run the Data Upgrader at 'Tools/Cleverous/Vault/Data Upgrader'.");
                return;
            }
            Reload();
        }

        /// <summary>
        /// Rebuild lists of static groups, custom groups, and all data entities.
        /// </summary>
        [MenuItem("Tools/Cleverous/Vault/DB Soft Reload", priority = 10)]
        public static void Reload()
        {
            List<DataEntity> entities = FindDataEntities();
            FindStaticGroups(entities);
            EditorUtility.SetDirty(Vault.Db);
            AssetDatabase.SaveAssetIfDirty(Vault.Db);
            AssetDatabase.Refresh();

            // check if any data has a key of int.MinValue, which is default unassigned and suggests a problem or old data.
            if (Vault.Db.Get(int.MinValue))
            {
                bool pressedOk = EditorUtility.DisplayDialog(
                    "Invalid entries detected",
                    "A entry in the Database with the default state int.MinValue (-2147483648) was found. This may be because Vault was just upgraded, and is normal. \n\n" +
                    "Please set the DB Key Starting Value in the Vault Dashboard footer and then run '/Tools/Cleverous/Vault/Data Upgrader' to upgrade any DB keys.",
                    "Ok",
                    "Open Online Help");
                if (!pressedOk)
                {
                    Application.OpenURL("https://lanefox.gitbook.io/vault/vault-inventory/create-new-database-items/manage-unique-asset-ids#set-your-local-unique-id");
                }
            }

            // if (VaultDashboard.Instance != null) VaultDashboard.Instance.RebuildFull();
            //Debug.Log($"<color=orange> DB Reload Callback Completed : Entities[{Vault.Db.GetEntityCount()}], Static Groups[{Vault.Db.GetAllStaticGroups().Count}], Custom Groups[{Vault.Db.GetAll<VaultCustomDataGroup>().Count}]</color>");
        }

        private static void FindStaticGroups(List<DataEntity> projectAssets)
        {
            if (DUMPLOG)
            {
                dump = new StringBuilder();
                dump.Append("VAULT DATABASE BUILDER DUMP LOG\n [...]\n");
            }

            Vault.Db.ClearStaticGroups();
            TypeCache.TypeCollection allTypesAvailable = TypeCache.GetTypesDerivedFrom(typeof(DataEntity));

            Dictionary<Type, List<DataEntity>> content = new Dictionary<Type, List<DataEntity>>();
            foreach (Type type in allTypesAvailable)
            {
                if (type.Name == "None") continue;
                if (!content.ContainsKey(type)) content.Add(type, new List<DataEntity>());
            }
            foreach (DataEntity asset in projectAssets)
            {
                Type assetType = asset.GetType();
                RecursiveGroupPopulation(content, asset, assetType);
            }
            foreach (KeyValuePair<Type, List<DataEntity>> dataGroup in content)
            {
                VaultStaticDataGroup group = new VaultStaticDataGroup(dataGroup.Key)
                {
                    SourceType = dataGroup.Key,
                    Content = dataGroup.Value
                };
                Vault.Db.SetStaticGroup(group);
                if (DUMPLOG) dump.Append($" ----{group.SourceType.Name} [{group.Content.Count} entities] \n");
            }
            if (DUMPLOG) Debug.Log(dump);
        }
        private static List<DataEntity> FindDataEntities()
        {
            List<DataEntity> list = new List<DataEntity>();
            string[] guids = AssetDatabase.FindAssets($"t:{typeof(DataEntity)}");
            list.AddRange(guids.Select(guid => AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(guid), typeof(DataEntity)) as DataEntity));

            Vault.Db.ClearData();
            foreach (DataEntity x in list)
            {
                Vault.Db.Add(x, false);
            }

            EditorUtility.SetDirty(Vault.Db);
            return list;
        }

        private static void RecursiveGroupPopulation(Dictionary<Type, List<DataEntity>> content, DataEntity asset, Type assetType)
        {
            if (!content.ContainsKey(assetType)) content.Add(assetType, new List<DataEntity>());
            content[assetType].Add(asset);
            if (assetType.BaseType != typeof(ScriptableObject)) RecursiveGroupPopulation(content, asset, assetType.BaseType);
        }

        [MenuItem("Tools/Cleverous/Vault/Data Key Upgrader (safe)", priority = 100)]
        public static void DataUpgrader()
        {
            bool proceed = EditorUtility.DisplayDialog(
                "Upgrade Data",
                "This will find every DataEntity in the project that has a key of int.MinValue and assign it a new one.",
                "Proceed",
                "Abort");
            if (!proceed) return;

            if (Vault.Db == null)
            {
                Debug.Log("DB Not found. Please restart the editor and open Vault Dashboard.");
                return;
            }

            List<DataEntity> data = GetAllDataEntitiesOfTypeInProject(typeof(DataEntity));
            int changed = 0;
            foreach (DataEntity x in data.Where(x => x.GetDbKey() == int.MinValue))
            {
                x.SetDbKey(Vault.Db.GenerateUniqueId());
                EditorUtility.SetDirty(x);
                changed++;
            }

            Reload();

            EditorUtility.DisplayDialog(
                "Complete",
                $"Changed {changed} entity keys.",
                "Ok");
        }

        [MenuItem("Tools/Cleverous/Vault/Data Key Reset (danger)", priority = 100)]
        public static void DataReset()
        {
            bool proceed = EditorUtility.DisplayDialog(
                "Reset Data Keys",
                "This will find every DataEntity in the project and assign a new DB Key to each one.",
                "Proceed",
                "Abort");
            if (!proceed) return;

            if (Vault.Db == null)
            {
                Debug.Log("DB Not found. Please restart the editor and open Vault Dashboard.");
                return;
            }

            List<DataEntity> data = GetAllDataEntitiesOfTypeInProject(typeof(DataEntity));
            int changed = 0;
            foreach (DataEntity x in data)
            {
                x.SetDbKey(Vault.Db.GenerateUniqueId());
                EditorUtility.SetDirty(x);
                changed++;
            }

            Reload();

            EditorUtility.DisplayDialog(
                "Complete",
                $"Changed {changed} entity keys.",
                "Ok");
        }

        /// <summary>
        /// Forces a refresh of assets serialization.
        /// </summary>
        [MenuItem("Tools/Cleverous/Vault/Reimport Assets - By Type (safe)", priority = 100)]
        public static void ReimportAllByType()
        {
            bool confirm = EditorUtility.DisplayDialog(
                "Reimport Vault Asset Files",
                $"Reimport all of the Vault Data Assets?\n\n" +
                $"This reimports all DataEntity type Assets. Won't fix issues related to mismatching class/file names.\n\n This is generally a safe operation.",
                "Proceed",
                "Abort!");

            if (!confirm) return;

            int count = 0;
            AssetDatabase.StartAssetEditing();
            try
            {
                string storage = VaultEditorUtility.GetPathToVaultCoreStorageFolder();
                if (storage[storage.Length - 1] == '/') storage = storage.Remove(storage.Length - 1);
                string[] files = AssetDatabase.FindAssets("t:DataEntity", new[] { storage });
                for (int i = 0; i < files.Length; i++)
                {
                    EditorUtility.DisplayProgressBar("Importing...", AssetDatabase.GUIDToAssetPath(files[i]), (float)i / files.Length);
                    AssetDatabase.ImportAsset(AssetDatabase.GUIDToAssetPath(files[i]), ImportAssetOptions.ForceUpdate);
                    Debug.Log($"{AssetDatabase.GUIDToAssetPath(files[i])}");
                    count++;
                }
            }
            finally
            {
                AssetDatabase.StopAssetEditing();
                EditorUtility.ClearProgressBar();
                EditorUtility.DisplayDialog(
                    "Done reimporting",
                    $"{count} assets were reimported and logged to the console.",
                    "Great");
            }
        }

        /// <summary>
        /// Forces a refresh of assets serialization.
        /// </summary>
        [MenuItem("Tools/Cleverous/Vault/Reimport Assets - By Name (safe)", priority = 100)]
        public static void ReimportAllByName()
        {
            bool confirm = EditorUtility.DisplayDialog(
                "Reimport Vault Asset Files",
                "Reimport all of the Vault Data Assets?\n\n" +
                "This reimports all files with names including 'Data-' which is the built-in prefix for saved Vault Files.\n\n This is generally a safe operation.",
                "Proceed",
                "Abort");

            if (!confirm) return;

            int count = 0;
            AssetDatabase.StartAssetEditing();
            try
            {
                string storage = VaultEditorUtility.GetPathToVaultCoreStorageFolder();
                if (storage[storage.Length - 1] == '/') storage = storage.Remove(storage.Length - 1);
                string[] files = AssetDatabase.FindAssets("Data-", new[] { storage });
                for (int i = 0; i < files.Length; i++)
                {
                    EditorUtility.DisplayProgressBar("Importing...", AssetDatabase.GUIDToAssetPath(files[i]), (float)i / files.Length);
                    AssetDatabase.ImportAsset(AssetDatabase.GUIDToAssetPath(files[i]), ImportAssetOptions.ForceUpdate);
                    Debug.Log($"{AssetDatabase.GUIDToAssetPath(files[i])}");
                    count++;
                }
            }
            finally
            {
                AssetDatabase.StopAssetEditing();
                EditorUtility.ClearProgressBar();
                EditorUtility.DisplayDialog(
                    "Done reimporting",
                    $"{count} assets were reimported and logged to the console.",
                    "Great");
            }
        }

        [MenuItem("Tools/Cleverous/Vault/Cleanup Vault (semi-safe)", priority = 100)]
        public static void CleanupStorageFolder()
        {
            bool confirm = EditorUtility.DisplayDialog(
                "Cleanup Vault",
                "This will check all asset files with the 'Data-' prefix and ensure validity. Invalid files can be deleted. This is primarily for identifying or removing assets which have broken script connections due to class name mismatches or class deletions. \n\n" +
                "You will be able to confirm delete/skip for each file individually.\n\n" +
                "You may NOT want to do this if the data found is broken accidentally and you're trying to restore it. This does not restore data, it validates assets and offers deletion if they are problematic. While this cleans up the project, it does DELETE the data asset file.\n",
                "Proceed",
                "Abort");

            if (!confirm) return;

            int found = 0;
            int deleted = 0;
            int failed = 0;
            int ignored = 0;
            AssetDatabase.StartAssetEditing();
            try
            {
                string storage = VaultEditorUtility.GetPathToVaultCoreStorageFolder();
                if (storage[storage.Length - 1] == '/') storage = storage.Remove(storage.Length - 1);
                string[] files = AssetDatabase.FindAssets("Data-", new[] { storage });
                for (int i = 0; i < files.Length; i++)
                {
                    EditorUtility.DisplayProgressBar("Scanning...", AssetDatabase.GUIDToAssetPath(files[i]), (float)i / files.Length);

                    string path = AssetDatabase.GUIDToAssetPath(files[i]);
                    DataEntity dataFile = AssetDatabase.LoadAssetAtPath<DataEntity>(AssetDatabase.GUIDToAssetPath(files[i]));
                    if (dataFile == null)
                    {
                        found++;

                        // how the heck do i get the object if we're literally dealing with objects that don't cast.
                        //EditorGUIUtility.PingObject();

                        bool deleteFaulty = EditorUtility.DisplayDialog(
                            "Faulty file found",
                            $"{path}\n\n" +
                            "This file seems to be broken. Please check:\n\n" +
                            "* File is actually a Vault Data file.\n" +
                            "* Class file still exists.\n" +
                            "* Class filename matches class name.\n" +
                            "* Assemblies are not black-listed.\n\n" +
                            "What do you want to do?", "Delete file", "Ignore file");

                        if (deleteFaulty)
                        {
                            bool success = AssetDatabase.DeleteAsset(path);
                            if (success) deleted++;
                            else failed++;
                        }
                        else ignored++;
                    }
                }
            }
            finally
            {
                AssetDatabase.StopAssetEditing();
                EditorUtility.ClearProgressBar();
                EditorUtility.DisplayDialog(
                    "Done cleaning.",
                    $"{found} assets were faulty.\n" +
                    $"{deleted} assets were deleted.\n" +
                    $"{failed} assets failed to delete.\n" +
                    $"{ignored} assets were ignored.\n",
                    "Excellent");
            }

        }

        public static List<VaultCustomDataGroup> GetAllCustomDataGroupAssets()
        {
            List<VaultCustomDataGroup> result = new List<VaultCustomDataGroup>();

            foreach (KeyValuePair<int, DataEntity> group in Vault.Db.Data)
            {
                if (!(group.Value is VaultCustomDataGroup value)) continue;

                //Debug.Log($"Added '{group}' to custom group list");
                result.Add(value);
            }

            return result;
        }

        public static List<T> GetAllAssetsInProject<T>() where T : DataEntity
        {
            // The AssetDatabase does not work correctly during callback for assembly reload and script reload.
            // Use a direct method instead during those times.

            List<T> list = new List<T>();
            string[] guids = AssetDatabase.FindAssets($"t:{typeof(T)}");
            foreach (string guid in guids)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guid);
                DataEntity asset = (DataEntity)AssetDatabase.LoadAssetAtPath(assetPath, typeof(T));
                list.Add(asset as T);
            }
            return list;
        }

        public static List<DataEntity> GetAllDataEntitiesOfTypeInProject(Type t)
        {
            // ~8ms per call

            List<DataEntity> list = new List<DataEntity>();
            string[] guids = AssetDatabase.FindAssets($"t:{t.FullName}");
            foreach (string guid in guids)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guid);
                DataEntity asset = (DataEntity)AssetDatabase.LoadAssetAtPath(assetPath, typeof(DataEntity));
                list.Add(asset);
            }
            return list;
        }
    }
}