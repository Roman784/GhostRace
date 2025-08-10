using System;

namespace Gameplay
{
    [Serializable]
    public struct CarRecordsData
    {
        public float DeltaTime;
        public CarRecordData[] Records;
    }
}
