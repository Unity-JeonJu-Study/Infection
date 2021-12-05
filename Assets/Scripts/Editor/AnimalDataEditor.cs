using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

public class AnimalDataEditor : OdinMenuEditorWindow
{
    private CreateNewAnimalData createNewAnimalData;

    [MenuItem("Tools/Animal Data")]
    private static void OpenWindow()
    {
        GetWindow<AnimalDataEditor>().Show();
    }

    protected override void OnBeginDrawEditors()
    {
        var selected = this.MenuTree.Selection;

        SirenixEditorGUI.BeginHorizontalToolbar();
        {
            GUILayout.FlexibleSpace();

            if (SirenixEditorGUI.ToolbarButton(("Delete Current")))
            {
                var asset = selected.SelectedValue as AnimalData;
                var path = AssetDatabase.GetAssetPath(asset);
                AssetDatabase.DeleteAsset(path);
                AssetDatabase.SaveAssets();
            }
        }
        SirenixEditorGUI.EndHorizontalToolbar();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        
        if(createNewAnimalData != null)
            DestroyImmediate(createNewAnimalData.animalData);
    }

    protected override OdinMenuTree BuildMenuTree()
    {
        var tree = new OdinMenuTree();

        createNewAnimalData = new CreateNewAnimalData();
        tree.Add("Create New", createNewAnimalData);
        tree.AddAllAssetsAtPath("Animal Data", "Assets/Resources/Data/Animal/", typeof(AnimalData));
        
        return tree;
    }

    public class CreateNewAnimalData
    {
        public CreateNewAnimalData()
        {
            animalData = ScriptableObject.CreateInstance<AnimalData>();
            animalData.animalName = "동물 이름을 입력하세요";
        }
        
        [InlineEditor(ObjectFieldMode =  InlineEditorObjectFieldModes.Hidden)]
        public AnimalData animalData;

        [Button("Add New Animal Data", ButtonHeight = 30)]
        private void CreateNewData()
        {
            AssetDatabase.CreateAsset(animalData, "Assets/Resources/Data/Animal/" + animalData.animalName + ".asset");
            AssetDatabase.SaveAssets();
            
            // create new
            animalData = ScriptableObject.CreateInstance<AnimalData>();
            animalData.animalName = "New Animal Data";
        }
    }
}
