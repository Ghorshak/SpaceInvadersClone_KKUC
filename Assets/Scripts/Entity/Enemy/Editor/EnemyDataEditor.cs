using UnityEditor;
using UnityEngine;

namespace SpaceInvadersClone.Entities.Enemies
{
    [CustomEditor(typeof(EnemyData), true)]
    public class EnemyDataEditor : Editor
    {
        private EnemyData _target;

        private void OnEnable()
        {
            _target = target as EnemyData;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (_target.Sprite == null)
                return;

            Texture2D sprite = AssetPreview.GetAssetPreview(_target.Sprite);

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label("", GUILayout.Height(120), GUILayout.Width(120));
            GUI.DrawTexture(GUILayoutUtility.GetLastRect(), sprite);
            GUILayout.EndHorizontal();
        }
    }
}
