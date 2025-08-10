using System.Collections;
using UnityEngine;
using Utils;

namespace Gameplay
{
    public class CarMotionReplayer
    {
        private Coroutine _replaying;

        public void StartReplaying(Car car, CarRecordsData recordsData)
        {
            Coroutines.Stop(_replaying);
            _replaying = Coroutines.Start(ReplayingRoutine(car, recordsData));
        }

        private IEnumerator ReplayingRoutine(Car car, CarRecordsData recordsData)
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
                    car.Move(record.Position, deltaTime);
                    car.Rotate(record.Rotation, deltaTime);

                    recordIdx++;
                }

                yield return null;
            }
        }
    }
}
