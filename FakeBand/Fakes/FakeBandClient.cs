﻿using FakeBand.Utils;
using Microsoft.Band;
using Microsoft.Band.Notifications;
using Microsoft.Band.Personalization;
using Microsoft.Band.Sensors;
using Microsoft.Band.Tiles;
using System.Threading;
using System.Threading.Tasks;
using System;
using FakeBand.Fakes;

namespace FakeBand.Fakes
{
    public enum BandVersion
    {
        BandOne,
        BandTwo,
    };

    public class FakeBandClient : IBandClient, IAppIdProvider
    {
        internal CargoVersions FirmwareVersions
        {
            get;
            private set;
        }

        internal BandTypeConstants BandTypeConstants
        {
            get
            {
                if (this._bandVersion == BandVersion.BandOne)
                {
                    return BandTypeConstants.Cargo;
                }
                return BandTypeConstants.Envoy;
            }
        }
        public IBandNotificationManager NotificationManager
        {
            get
            {
                return new FakeBandNotificationManager(this, _container);
            }
        }

        public IBandPersonalizationManager PersonalizationManager
        {
            get
            {
                return new FakeBandPersonalizationManager();
            }
        }

        private IBandSensorManager _sensorManager = new FakeBandSensorManager();
        private IBandInfo bandInfo;
        private BandVersion _bandVersion;

        public FakeBandClient(IBandInfo bandInfo, BandVersion version = BandVersion.BandTwo)
        {
            this.bandInfo = bandInfo;
            this._bandVersion = version;
        }

        internal static Guid GetApplicationIdFromName(byte[] nameAndOwnerId, ushort friendlyNameLength)
        {
            throw new NotImplementedException();
        }

        public IBandSensorManager SensorManager
        {
            get
            {
                return _sensorManager;
            }
        }

        IBandTileManager _tileManager;
        private FakeTileContainer _container = new FakeTileContainer();

        public IBandTileManager TileManager
        {
            get
            {
                if (_tileManager == null)
                {
                    IBandConstants consts = null;
                        
                    if (_bandVersion == BandVersion.BandOne)
                    {
                        consts = new FakeBandOneConstants();
                    }
                    else
                    {
                        consts = new FakeBandTwoConstants();
                    }

                    _tileManager = new FakeBandTileManager(consts, this, _container);
                }
                return _tileManager;
            }
        }

        public void Dispose()
        {
        }

        public async Task<string> GetFirmwareVersionAsync()
        {
            await Task.Delay(200);
            return "1.0.0.0";
        }

        public async Task<string> GetFirmwareVersionAsync(CancellationToken token)
        {
            await Task.Delay(200, token);
            return "1.0.0.0";
        }

        public async Task<string> GetHardwareVersionAsync()
        {
            await Task.Delay(200);
            return "1.0.0.0";
        }

        public async Task<string> GetHardwareVersionAsync(CancellationToken token)
        {
            await Task.Delay(200, token);
            return "1.0.0.0";
        }

        public Guid GetAppId()
        {
            return new Guid("C7600DBA-0220-4866-A7BA-08B23C2225C8"); // TODO: Fix this - all this to be configured..
        }
    }
}