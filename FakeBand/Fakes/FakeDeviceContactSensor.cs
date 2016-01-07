﻿using System.Threading.Tasks;
using Microsoft.Band.Sensors;
using System;
using System.Collections.Generic;
using FakeBand.Utils;

namespace FakeBand.Fakes
{
    internal class FakeDeviceContactSensor : FakeBandSensor<IBandContactReading>, IBandContactSensor
    {
        public FakeDeviceContactSensor() : 
            base(new List<BandType>
        {
            BandType.Cargo,
            BandType.Envoy
        }, new Dictionary<TimeSpan, SubscriptionType>
        {
            {
                TimeSpan.Zero,
                SubscriptionType.DeviceContact
            }
        })
        {
            State = new FakeBandDeviceContactReading(BandContactState.Worn);
        }

        private TimeSpan _reportingInterval = TimeSpan.FromSeconds(30);

        public override TimeSpan ReportingInterval
        {
            get
            {
                return _reportingInterval;
            }

            set
            {
                if (_reportingInterval == value)
                    return;
                _reportingInterval = value;
            }
        }

        private FakeBandDeviceContactReading State { get; set; }

        public override IBandSensorReading CreateReading()
        {
            return State;
        }

        public async Task<IBandContactReading> GetCurrentStateAsync()
        {
            await Task.Delay(300);
            return State;
        }
    }
}