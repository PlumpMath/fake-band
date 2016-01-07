﻿using System;
using Microsoft.Band.Sensors;
using System.Collections.Generic;
using FakeBand.Utils;

namespace FakeBand.Fakes
{
    internal class FakeAltimeterSensor : FakeBandSensor<IBandAltimeterReading>
    {
        public FakeAltimeterSensor() :
            base(new List<BandType>
        {
            BandType.Envoy
        }, new Dictionary<TimeSpan, SubscriptionType>
        {
            {
                TimeSpan.FromSeconds(1.0),
                SubscriptionType.Elevation
            }
        })
        {
        }

        public override IBandSensorReading CreateReading()
        {
            return new FakeAltimeterReading();
        }
    }
}