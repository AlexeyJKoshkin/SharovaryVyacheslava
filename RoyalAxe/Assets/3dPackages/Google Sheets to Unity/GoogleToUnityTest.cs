using GoogleSheetsToUnity;
using UnityEngine;

public class GoogleToUnityTest : MonoBehaviour
{
    public string SheetId;

    public string WorksheetName;
    // Start is called before the first frame update
    void Start()
    {
        SpreadsheetManager.Read(new GSTU_Search(SheetId,WorksheetName), Callback);
    }

    private void Callback(GstuSpreadSheet gstuSpreadSheet)
    {
        foreach (var row in gstuSpreadSheet.rows.primaryDictionary)
        {
            var str = string.Empty;
            foreach (var cell in row.Value)
            {
                str += $" {cell.value}";
            }
            Debug.Log(str);
        }
    }
}
