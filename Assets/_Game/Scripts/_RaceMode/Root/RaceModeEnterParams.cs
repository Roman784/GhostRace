using Gameplay;
using GameRoot;

namespace RaceMode
{
    public class RaceModeEnterParams : SceneEnterParams
    {
        public readonly CarRecordsData Records;

        public RaceModeEnterParams(CarRecordsData records) : base(Scenes.RACE_MODE)
        {
            Records = records;
        }
    }
}
