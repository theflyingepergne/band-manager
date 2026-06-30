using UnityEngine;

public static class GameBootstrapper
{
    // This runs before any scene is loaded (no need to add to scene hierarchy)
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void ExecuteGameInitialization()
    {
        Debug.Log("--- Game Boot Sequence Started ---");

        // Init Save/Load system first so it's ready to load from
        // SaveLoadSystem.Initialize();

        // Init DateManager which will get the date from SaveLoadSystem
        DateManager.Initialize();

        // Init BandManager, which will safely pull data from the SaveLoadSystem
        BandManager.Initialize();

        ScheduleManager.Initialize();

        RecruitmentManager.Initialize();

        Debug.Log("--- Game Boot Sequence Complete ---");
    }
}