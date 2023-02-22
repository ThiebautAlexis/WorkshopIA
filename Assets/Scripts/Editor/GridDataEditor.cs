using UnityEditor;
using UnityEngine; 

[CustomEditor(typeof(GridData))]
public class GridDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        if(GUILayout.Button("Generate Grid Data"))
        {
            (target as GridData).GenerateGridData(); 
        }

        if(GUILayout.Button("Find Path"))
        {
            (target as GridData).FindPath();
        }
        base.OnInspectorGUI();
    }

}
