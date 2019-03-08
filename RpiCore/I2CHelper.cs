using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Devices.I2c;
using Windows.Foundation;

namespace RpiCore
{
    public sealed class I2CHelper
    {
        private static bool Lock = false;
        private static string AQS;
        private static DeviceInformationCollection DIS;

        /// <summary>
        /// Sends control signal to the specified slave Arduino and retrieves response bytes.
        /// </summary>
        /// <param name="I2C_Slave_Address">Slave Arduino's address</param>
        /// <param name="Pin">Pin to be set. ONLY VALID FOR MODE-2</param>
        /// <param name="PinValue">Value to be set.</param>
        /// <returns>Returns fourteen response byte.</returns>
        public IAsyncOperation<IEnumerable<byte>> WriteRead(int I2C_Slave_Address, byte Pin, byte PinValue)
        {
            return this.WriteReadHelper(I2C_Slave_Address, Pin, PinValue).AsAsyncOperation();
        }

        /// <summary>
        /// Might use this one for reading from slave
        /// </summary>
        /// <param name="I2C_Slave_Address">Slave Arduino's address</param>
        /// <returns>Returns fourteen response byte.</returns>
        public IAsyncOperation<IEnumerable<byte>> WriteRead(int I2C_Slave_Address)
        {
            return this.WriteReadHelper(I2C_Slave_Address).AsAsyncOperation();
        }


        private async Task<IEnumerable<byte>> WriteReadHelper(int I2C_Slave_Address, byte Pin = 0, byte PinValue = 0)
        {
            while (Lock != false)
            {

            }

            Lock = true;
            // Create response byte array of fourteen
            IEnumerable<byte> Response = null;
            byte[] res = new byte[14];

            try
            {
                // Initialize I2C
                var Settings = new I2cConnectionSettings(I2C_Slave_Address);
                Settings.BusSpeed = I2cBusSpeed.StandardMode;

                if (AQS == null || DIS == null)
                {
                    AQS = I2cDevice.GetDeviceSelector("I2C1");
                    DIS = await DeviceInformation.FindAllAsync(AQS);
                }

                using (I2cDevice Device = await I2cDevice.FromIdAsync(DIS[0].Id, Settings))
                {
                    Device.Write(new byte[] { Pin, PinValue });

                    Device.Read(res);

                    Response = res.ToList();
                }
            }
            catch (Exception)
            {
                // We will see what we can do here!
            }

            Lock = false;
            return Response;
        }

    }
}
