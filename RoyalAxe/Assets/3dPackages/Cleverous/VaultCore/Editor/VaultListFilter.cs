// (c) Copyright Cleverous 2022. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Cleverous.VaultDashboard;
using Cleverous.VaultSystem;
using UnityEngine;

public static class VaultListFilter
{
    // *** PROPERTY SELECTION ***
    // Property Name

    // *** TYPES ***
    // (#) int
    // (#) float
    // (A) string
    // (A) enum

    // *** OPERATIONS ***
    // Includes / Exact (default, no operator)
    // Greater Than >
    // Less Than <
    // Between - (require lhs/rhs vals, only numerical properties)
    
    public enum FilterType { String, Float, Int }
    public enum FilterOp
    {
        Contains, 
        GreaterThan, 
        LessThan
    }
    public static List<string> FilterOpSymbols = new List<string>
    {
        "=",
        ">",
        "<"
    };

    public static List<string> GetFilterablePropertyNames()
    {
        Type targetType = VaultDashboard.CurrentSelectedGroup.SourceType;
        List<string> results = (from field in targetType.GetFields() where Attribute.IsDefined(field, typeof(VaultFilterableAttribute)) select field.Name).ToList();
        results.AddRange(from prop in targetType.GetProperties() where Attribute.IsDefined(prop, typeof(VaultFilterableAttribute)) select prop.Name);
        results.AddRange(from method in targetType.GetMethods() where Attribute.IsDefined(method, typeof(VaultFilterableAttribute)) select method.Name);
        return results;
    }

    /// <summary>
    /// Filters the current Group based on class field/property criteria.
    /// </summary>
    /// <returns>A list of DataEntity assets from the selected Group that meet the filter criteria.</returns>
    public static List<DataEntity> FilterList()
    {
        string input = VaultDashboard.Instance.GetAssetFilterPropertyValue();

        // if the list is too small, just return the list.
        if (VaultDashboard.CurrentSelectedGroup.Content.Count < 2 || string.IsNullOrEmpty(input)) return VaultDashboard.CurrentSelectedGroup.Content;

        List<DataEntity> filteredListResult = new List<DataEntity>();
        Type targetType = VaultDashboard.CurrentSelectedGroup.SourceType;
        FilterOp operation = VaultDashboard.Instance.GetAssetFilterOperation();
        FilterType filterType = VaultDashboard.Instance.GetAssetFilterPropertyType();

        // ********* FIGURE OUT THE OPERATOR ********* //
        if (filterType == FilterType.Float)
        {
            foreach (DataEntity asset in VaultDashboard.CurrentSelectedGroup.Content)
            {
                FieldInfo field = targetType.GetField(VaultDashboard.Instance.GetAssetFilterPropertyName());
                object val;
                if (field == null)
                {
                    PropertyInfo property = targetType.GetProperty(VaultDashboard.Instance.GetAssetFilterPropertyName());
                    if (property == null) continue; // it's not a field or a property.
                    val = property.GetValue(asset);
                }
                else val = field.GetValue(asset);

                float assetValue = Convert.ToSingle(val);
                float targetValue = VaultDashboard.Instance.AssetFilterValueFloat;

                switch (operation)
                {
                    case FilterOp.Contains:
                        if (Mathf.Approximately(assetValue, targetValue)) filteredListResult.Add(asset);
                        break;
                    case FilterOp.GreaterThan:
                        if (assetValue > targetValue) filteredListResult.Add(asset);
                        break;
                    case FilterOp.LessThan:
                        if (assetValue < targetValue) filteredListResult.Add(asset);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
        else if (filterType == FilterType.Int)
        {
            foreach (DataEntity asset in VaultDashboard.CurrentSelectedGroup.Content)
            {
                FieldInfo field = targetType.GetField(VaultDashboard.Instance.GetAssetFilterPropertyName());
                object val;
                if (field == null)
                {
                    PropertyInfo property = targetType.GetProperty(VaultDashboard.Instance.GetAssetFilterPropertyName());
                    if (property == null) continue; // it's not a field or a property.
                    val = property.GetValue(asset);
                }
                else val = field.GetValue(asset);

                int assetValue = Convert.ToInt32(val);
                int targetValue = VaultDashboard.Instance.AssetFilterValueInt;

                switch (operation)
                {
                    case FilterOp.Contains:
                        if (assetValue == targetValue) filteredListResult.Add(asset);
                        break;
                    case FilterOp.GreaterThan:
                        if (assetValue > targetValue) filteredListResult.Add(asset);
                        break;
                    case FilterOp.LessThan:
                        if (assetValue < targetValue) filteredListResult.Add(asset);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
        else if (filterType == FilterType.String)
        {
            foreach (DataEntity asset in VaultDashboard.CurrentSelectedGroup.Content)
            {
                FieldInfo field = targetType.GetField(VaultDashboard.Instance.GetAssetFilterPropertyName());
                object val;
                if (field == null)
                {
                    PropertyInfo property = targetType.GetProperty(VaultDashboard.Instance.GetAssetFilterPropertyName());
                    if (property == null) continue; // it's not a field or a property.
                    val = property.GetValue(asset);
                }
                else val = field.GetValue(asset);

                string assetValue = Convert.ToString(val);
                string targetValue = VaultDashboard.Instance.AssetFilterValueString;

                if (assetValue.ToLower().Contains(targetValue.ToLower())) filteredListResult.Add(asset);
            }
        }

        return filteredListResult;
    }
}