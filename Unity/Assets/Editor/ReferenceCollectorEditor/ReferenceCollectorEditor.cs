using ET;
using Scriban;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
//Object并非C#基础中的Object，而是 UnityEngine.Object
using Object = UnityEngine.Object;

//自定义ReferenceCollector类在界面中的显示与功能
[CustomEditor(typeof (ReferenceCollector))]
public class ReferenceCollectorEditor: Editor
{
    //输入在textfield中的字符串
    private string searchKey
	{
		get
		{
			return _searchKey;
		}
		set
		{
			if (_searchKey != value)
			{
				_searchKey = value;
				heroPrefab = referenceCollector.Get<Object>(searchKey);
			}
		}
	}

	private ReferenceCollector referenceCollector;

	private Object heroPrefab;

	private string _searchKey = "";

	private void DelNullReference()
	{
		var dataProperty = serializedObject.FindProperty("data");
		for (int i = dataProperty.arraySize - 1; i >= 0; i--)
		{
			var gameObjectProperty = dataProperty.GetArrayElementAtIndex(i).FindPropertyRelative("gameObject");
			if (gameObjectProperty.objectReferenceValue == null)
			{
				dataProperty.DeleteArrayElementAtIndex(i);
				EditorUtility.SetDirty(referenceCollector);
				serializedObject.ApplyModifiedProperties();
				serializedObject.UpdateIfRequiredOrScript();
			}
		}
	}

	private void OnEnable()
	{
        //将被选中的gameobject所挂载的ReferenceCollector赋值给编辑器类中的ReferenceCollector，方便操作
        referenceCollector = (ReferenceCollector) target;
	}

	public override void OnInspectorGUI()
	{
        //使ReferenceCollector支持撤销操作，还有Redo，不过没有在这里使用
        Undo.RecordObject(referenceCollector, "Changed Settings");
		var dataProperty = serializedObject.FindProperty("data");
        //开始水平布局，如果是比较新版本学习U3D的，可能不知道这东西，这个是老GUI系统的知识，除了用在编辑器里，还可以用在生成的游戏中
		GUILayout.BeginHorizontal();
        //下面几个if都是点击按钮就会返回true调用里面的东西
		if (GUILayout.Button("添加引用"))
		{
            //添加新的元素，具体的函数注释
            // Guid.NewGuid().GetHashCode().ToString() 就是新建后默认的key
            AddReference(dataProperty, Guid.NewGuid().GetHashCode().ToString(), null);
		}
		if (GUILayout.Button("全部删除"))
		{
			referenceCollector.Clear();
		}
		if (GUILayout.Button("删除空引用"))
		{
			DelNullReference();
		}
		if (GUILayout.Button("排序"))
		{
			referenceCollector.Sort();
		}
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.BeginHorizontal();
        //可以在编辑器中对searchKey进行赋值，只要输入对应的Key值，就可以点后面的删除按钮删除相对应的元素
        searchKey = EditorGUILayout.TextField(searchKey);
        //添加的可以用于选中Object的框，这里的object也是(UnityEngine.Object
        //第三个参数为是否只能引用scene中的Object
        EditorGUILayout.ObjectField(heroPrefab, typeof (Object), false);
		if (GUILayout.Button("删除"))
		{
			referenceCollector.Remove(searchKey);
			heroPrefab = null;
		}
		GUILayout.EndHorizontal();
		EditorGUILayout.Space();

		var delList = new List<int>();
        SerializedProperty property;
        //遍历ReferenceCollector中data list的所有元素，显示在编辑器中
        for (int i = referenceCollector.data.Count - 1; i >= 0; i--)
		{
			GUILayout.BeginHorizontal();
            //这里的知识点在ReferenceCollector中有说
            property = dataProperty.GetArrayElementAtIndex(i).FindPropertyRelative("key");
            EditorGUILayout.TextField(property.stringValue, GUILayout.Width(150));
            property = dataProperty.GetArrayElementAtIndex(i).FindPropertyRelative("gameObject");
            property.objectReferenceValue = EditorGUILayout.ObjectField(property.objectReferenceValue, typeof(Object), true);
			if (GUILayout.Button("X"))
			{
                //将元素添加进删除list
				delList.Add(i);
			}
			GUILayout.EndHorizontal();
		}
		var eventType = Event.current.type;
        //在Inspector 窗口上创建区域，向区域拖拽资源对象，获取到拖拽到区域的对象
        if (eventType == EventType.DragUpdated || eventType == EventType.DragPerform)
		{
			// Show a copy icon on the drag
			DragAndDrop.visualMode = DragAndDropVisualMode.Copy;

			if (eventType == EventType.DragPerform)
			{
				DragAndDrop.AcceptDrag();
				foreach (var o in DragAndDrop.objectReferences)
				{
					AddReference(dataProperty, o.name, o);
				}
			}

			Event.current.Use();
		}

        //遍历删除list，将其删除掉
		foreach (var i in delList)
		{
			dataProperty.DeleteArrayElementAtIndex(i);
		}
		serializedObject.ApplyModifiedProperties();
		serializedObject.UpdateIfRequiredOrScript();
		if (GUILayout.Button("一键导出默认模板代码"))
		{
			OneClickExport();
		}
	}

    //添加元素，具体知识点在ReferenceCollector中说了
    private void AddReference(SerializedProperty dataProperty, string key, Object obj)
	{
		int index = dataProperty.arraySize;
		dataProperty.InsertArrayElementAtIndex(index);
		var element = dataProperty.GetArrayElementAtIndex(index);
		element.FindPropertyRelative("key").stringValue = key;
		element.FindPropertyRelative("gameObject").objectReferenceValue = obj;
	}

	//一键导出默认模板代码
	private void OneClickExport()
	{
		string componentText =
@"using System;
using System.Net;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
	public class {{model.component_name}}: Entity, IAwake
	{
		
	}
}
";
		string systemText =
@"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    public static class {{model.system_name}}
    {
        //初始化界面。
        public static void OnInit(this {{model.component_name}} self)
        {
            
        }

        //界面打开。
        public static void OnOpen(this {{model.component_name}} self)
        {
		}

		//界面关闭。
		public static void OnClose(this {{model.component_name}} self)
		{

		}

		//界面回收。
		public static void OnRecycle(this {{model.component_name}} self)
		{

		}

		//界面暂停。
		public static void OnPause(this {{model.component_name}} self)
		{

		}

		//界面暂停恢复。
		public static void OnResume(this {{model.component_name}} self)
		{

		}

		//界面遮挡。
		public static void OnCover(this {{model.component_name}} self)
		{

		}

		//界面遮挡恢复。
		public static void OnReveal(this {{model.component_name}} self)
		{

		}

		//界面激活。
		public static void OnRefocus(this {{model.component_name}} self)
		{

		}

		//界面深度改变。
		public static void OnDepthChanged(this {{model.component_name}} self)
		{

		}
	}
}";
		string handleText =
@"using System;
using UnityEngine;

namespace ET
{
	[AUIEvent(UIType.{{model.ui_name}})]
	public  class {{model.event_handler}} : IAUIEventHandler
	{
        //初始化界面。
        public void OnInit(UIForm uiForm, int serialId, string uiFormAssetName, bool pauseCoveredUIForm, object userData)
        {
            uiForm.OnInit(serialId, uiFormAssetName, pauseCoveredUIForm, userData);
            var childForm = uiForm.AddComponent<{{model.component_name}}>();
            childForm?.OnInit();
        }

        //界面打开。
        public void OnOpen(UIForm uiForm, GameObject obj)
        {
            InitUIForm(uiForm, obj);
            uiForm.OnOpen();
            var childForm = uiForm.GetComponent<{{model.component_name}}>();
            childForm?.OnOpen();
        }

        //界面关闭。
        public void OnClose(UIForm uiForm, bool isShutdown, object userData)
        {
            uiForm.OnClose(isShutdown,userData);
            var childForm = uiForm.GetComponent<{{model.component_name}}>();
            childForm?.OnClose();
        }

        //界面回收。
        public void OnRecycle(UIForm uiForm)
        {
            uiForm.OnRecycle();
            var childForm = uiForm.GetComponent<{{model.component_name}}>();
            childForm?.OnRecycle();
        }

        //界面暂停。
        public void OnPause(UIForm uiForm)
        {
            uiForm.OnPause();
            var childForm = uiForm.GetComponent<{{model.component_name}}>();
            childForm?.OnPause();
        }

        //界面暂停恢复。
        public void OnResume(UIForm uiForm)
        {
            uiForm.OnResume();
            var childForm = uiForm.GetComponent<{{model.component_name}}>();
            childForm?.OnResume();
        }

        //界面遮挡。
        public void OnCover(UIForm uiForm)
        {
            uiForm.OnCover();
            var childForm = uiForm.GetComponent<{{model.component_name}}>();
            childForm?.OnCover();
        }

        //界面遮挡恢复。
        public void OnReveal(UIForm uiForm)
        {
            uiForm.OnReveal();
            var childForm = uiForm.GetComponent<{{model.component_name}}>();
            childForm?.OnReveal();
        }

        //界面激活。
        public void OnRefocus(UIForm uiForm, object userData)
        {
            uiForm.OnRefocus(userData);
            var childForm = uiForm.GetComponent<{{model.component_name}}>();
            childForm?.OnRefocus();
        }

        //界面深度改变。
        public void OnDepthChanged(UIForm uiForm, int uiGroupDepth, int depthInUIGroup)
        {
            uiForm.OnDepthChanged(depthInUIGroup, depthInUIGroup);
            var childForm = uiForm.GetComponent<{{model.component_name}}>();
            childForm?.OnDepthChanged();
        }

        //初始化UIForm。
        private void InitUIForm(UIForm uiForm, GameObject obj)
        {
            var uiComponent = uiForm.ZoneScene().GetComponent<UIComponent>();
            uiForm.obj = obj;
            try
            {
                if (obj == null)
                {
                    if (uiComponent.m_UIFormsToReleaseOnLoad.Contains(uiForm.m_SerialId))
                    {
                        uiComponent.m_UIFormsToReleaseOnLoad.Remove(uiForm.m_SerialId);
                    }
                    throw new GameFrameworkException(""Open UI form info is invalid."");

				}

                if (uiForm == null)
                {
                    Log.Error(""Can not create UI form in UI form helper."");
                }

				uiComponent.m_UIFormsBeingLoaded.Remove(uiForm.m_SerialId);
				Game.EventSystem.Publish(new UICommonEventType.OpenUIFormSuccessEventArgs());
			}

			catch (Exception)
			{
				Game.EventSystem.Publish(new UICommonEventType.OpenUIFormFailureEventArgs());
				throw;
			}
			uiForm.GetParent<UIGroup>().Refresh();
			uiForm.rf = uiForm.obj.GetComponent<ReferenceCollector>();
        }
    }
}

";
		var referenceModelData = new ReferenceModelData
		{
			uiName = referenceCollector.gameObject.name,
			componentName = referenceCollector.gameObject.name + "Component",
			systemName = referenceCollector.gameObject.name + "ComponentSystem",
			eventHandler = referenceCollector.gameObject.name + "EventHandler"
		};
		var tpl1 = Template.Parse(componentText);
		var res1 = tpl1.Render(new{model = referenceModelData});
		var filePath1 = Path.Combine(UnityEngine.Application.dataPath.Substring(
					0, UnityEngine.Application.dataPath.LastIndexOf('/')), Utility.Text.Format("Codes/ModelView/Demo/UI/{0}", referenceModelData.uiName));
		if (!Directory.Exists(filePath1))
		{
			Directory.CreateDirectory(filePath1);
		}
		File.WriteAllText(Utility.Text.Format("{0}/{1}.cs", filePath1, referenceModelData.componentName), res1);

		var tpl2 = Template.Parse(systemText);
		var res2 = tpl2.Render(new { model = referenceModelData });
		var filePath2 = Path.Combine(UnityEngine.Application.dataPath.Substring(
			0, UnityEngine.Application.dataPath.LastIndexOf('/')), Utility.Text.Format("Codes/HotfixView/Demo/UI/{0}", referenceModelData.uiName));
		if (!Directory.Exists(filePath2))
		{
			Directory.CreateDirectory(filePath2);
		}
		File.WriteAllText(Utility.Text.Format("{0}/{1}.cs", filePath2, referenceModelData.systemName), res2);

		var tpl3 = Template.Parse(handleText);
		var res3 = tpl3.Render(new { model = referenceModelData });
		var filePath3 = Path.Combine(UnityEngine.Application.dataPath.Substring(
			0, UnityEngine.Application.dataPath.LastIndexOf('/')), Utility.Text.Format("Codes/HotfixView/Demo/UI/{0}/Event", referenceModelData.uiName));
		if (!Directory.Exists(filePath3))
		{
			Directory.CreateDirectory(filePath3);
		}
		File.WriteAllText(Utility.Text.Format("{0}/{1}.cs", filePath3, referenceModelData.eventHandler), res3);

		EditorUtility.DisplayDialog("提示", "陛下，您的脚本已为您导出成功！！", "朕知道了！");
	}
}
