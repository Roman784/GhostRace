using System.Collections;
using UnityEngine;
using Utils;

namespace Gameplay
{
    public class CarMotionReplayer
    {
        private Coroutine _replaying;

        public void StartReplaying(CarMotionActor car, CarRecordsData recordsData)
        {
            Coroutines.Stop(_replaying);
            _replaying = Coroutines.Start(ReplayingRoutine(car, recordsData));
        }

        public void StopReplaying()
        {
            Coroutines.Stop(_replaying);
        }

        private IEnumerator ReplayingRoutine(CarMotionActor car, CarRecordsData recordsData)
        {
            var deltaTime = recordsData.DeltaTime;
            var records = recordsData.Records;
            var recordIdx = 0;
            var time = 0f;

            while (recordIdx < records.Length - 1)
            {
                time += Time.deltaTime;

                var record = records[recordIdx];
                if (time >= deltaTime * recordIdx)
                {
                    // Delta multiplication is needed to smooth out the movement so that there are no micro stops.
                    car.Move(record.Position, deltaTime * 1.1f);
                    car.Rotate(record.Rotation, deltaTime * 1.1f);

                    recordIdx++;
                }

                yield return null;
            }
        }
    }
}
