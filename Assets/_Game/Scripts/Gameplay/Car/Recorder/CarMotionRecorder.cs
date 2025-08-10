using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Gameplay
{
    public class CarMotionRecorder
    {
        private CarRecordsData _recordsData;
        private Queue<CarRecordData> _records;
        private Coroutine _recording;

        public void StartRecording(Car car, float deltaTime)
        {
            _recordsData.DeltaTime = deltaTime;
            _records = new Queue<CarRecordData>();

            Coroutines.Stop(_recording);
            _recording = Coroutines.Start(RecordingRoutine(car, deltaTime));
        }

        public CarRecordsData StopRecording()
        {
            if (_records == null)
                throw new Exception("Failed to get the record because it was not started!");

            _recordsData.Records = _records.ToArray();
            return _recordsData;
        }

        private IEnumerator RecordingRoutine(Car car, float deltaTime)
        {
            while (true)
            {
                _records.Enqueue(
                    new CarRecordData()
                    {
                        Position = car.Position,
                        Rotation = car.Rotation
                    });

                yield return new WaitForSeconds(deltaTime);
            }
        }
    }
}
