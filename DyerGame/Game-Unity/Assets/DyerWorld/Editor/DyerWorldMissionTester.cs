using UnityEditor;
using UnityEngine;
using System.Threading.Tasks;

public class DyerWorldMissionTester : EditorWindow {
    private DyerGameCopilot copilot;
    private Vector2 scroll;
    private string[] missionIds = new string[0];

    [MenuItem("DyerWorld/Mission Tester")]
    public static void ShowWindow() => GetWindow<DyerWorldMissionTester>("Mission Tester");

    async void OnEnable() {
        copilot = Object.FindObjectOfType<DyerGameCopilot>();
        if (copilot != null) {
            var missions = await copilot.GetMissionsAsync();
            if (missions != null) missionIds = missions.missions.ConvertAll(m => m.id).ToArray();
        }
    }

    void OnGUI() {
        if (copilot == null) {
            EditorGUILayout.HelpBox("Drop a DyerGameCopilot prefab into your scene.", MessageType.Warning);
            return;
        }
        scroll = EditorGUILayout.BeginScrollView(scroll);
        foreach (var id in missionIds) {
            if (GUILayout.Button("Load " + id)) {
                Debug.Log("Would load mission scene: " + id);
                // Here you could call SceneManager.LoadScene with mission.sceneId
            }
        }
        EditorGUILayout.EndScrollView();
    }
}
