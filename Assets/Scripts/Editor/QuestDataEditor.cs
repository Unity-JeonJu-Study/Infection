using Sirenix.OdinInspector.Editor;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

public class QuestDataEditor : OdinMenuEditorWindow
{
   private CreateNewQuestData createNewQuestData;

   [MenuItem("Tools/Quest Data")]
   private static void OpenWindow()
   {
      GetWindow<QuestDataEditor>().Show();
   }
   
   protected override void OnBeginDrawEditors()
   {
      var selected = this.MenuTree.Selection;
   }

   protected override void OnDestroy()
   {
      base.OnDestroy();
      
      if(createNewQuestData != null)
         DestroyImmediate(createNewQuestData.questList);
   }

   protected override OdinMenuTree BuildMenuTree()
   {
      var tree = new OdinMenuTree();

      createNewQuestData = new CreateNewQuestData();
      tree.Add("Create New", createNewQuestData);
      tree.AddAllAssetsAtPath("Quest Data", "Assets/Resources/Data/Quest/", typeof(QuestData));

      return tree;
   }

   public class CreateNewQuestData
   {
      [InlineEditor(ObjectFieldMode = InlineEditorObjectFieldModes.Hidden)]
      public QuestList questList;

      public CreateNewQuestData()
      {
         questList = ScriptableObject.CreateInstance<QuestList>();
      }
      
      [Button("Add New Quest Data", ButtonHeight = 30)]
      private void CreateNewData()
      {
         AssetDatabase.CreateAsset(questList, "Assets/Resources/Data/Quest/" + questList.flag + ".asset");
         AssetDatabase.SaveAssets();
            
         // create new
         questList = ScriptableObject.CreateInstance<QuestList>();
      }
   }
}
