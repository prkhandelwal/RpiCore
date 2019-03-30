using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Devices.I2c;
using Windows.Foundation;

namespace RpiUNO
{
    public sealed class UnoI2C
    {
        private static bool Lock = false;
        private static string AQS;
        private static DeviceInformationCollection DIS;

        /// <summary>
        /// Sends control signal to the specified slave Arduino and retrieves response bytes.
        /// </summary>
        /// <param name="I2C_Slave_Address">Slave Arduino's address</param>
        /// <param name="ControlMode">Select specific control mode</param>
        /// <param name="Pin">Pin to be set. ONLY VALID FOR MODE-2</param>
        /// <param name="PinValue">Value to be set.</param>
        /// <returns>Returns fourteen response byte.</returns>
        public static IAsyncOperation<IEnumerable<byte>> ReadWriteAsync(int I2C_Slave_Address, byte Mode, byte Pin, byte PinValue)
        {
            return WriteReadHelper(I2C_Slave_Address, Mode, Pin, PinValue).AsAsyncOperation();
        }

        /// <summary>
        /// Reads form specifird slave Arduino.
        /// </summary>
        /// <param name="I2C_Slave_Address">Slave Arduino's address</param>
        /// <returns>Returns fourteen response byte.</returns>
        //public static IAsyncOperation<IEnumerable<byte>> Read(int I2C_Slave_Address)
        //{
        //    return WriteReadHelper(I2C_Slave_Address).AsAsyncOperation();
        //}


        private static async Task<IEnumerable<byte>> WriteReadHelper(int I2C_Slave_Address, byte Pin = 0, byte Mode = 1, byte PinValue = 0)
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
                var Settings = new I2cConnectionSettings(I2C_Slave_Address)
                {
                    BusSpeed = I2cBusSpeed.StandardMode
                };

                if (AQS == null || DIS == null)
                {
                    AQS = I2cDevice.GetDeviceSelector("I2C1");
                    DIS = await DeviceInformation.FindAllAsync(AQS);
                }

                using (I2cDevice Device = await I2cDevice.FromIdAsync(DIS[0].Id, Settings))
                {
                    Device.Write(new byte[] { Mode, Pin, PinValue });

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

    /// <summary>
    /// Contains Getters for Modes
    /// </summary>
    public sealed class Mode
    {
        private static readonly byte _RecieveSensorData = 0;

        private static readonly byte _RecieveDeviceState = 1;

        private static readonly byte _SendIOSignal = 2;

        /// <summary>
        /// Retrieves sensor data from specified I2C slave Arduino
        /// </summary>
        public static byte ReceiveSensorData
        {
            get
            {
                return _RecieveSensorData;
            }
        }

        /// <summary>
        /// Retrieves devices state from specified I2C slave Arduino
        /// </summary>
        public static byte RecieveDeviceState
        {
            get
            {
                return _RecieveDeviceState;
            }
        }

        /// <summary>
        /// Sends IO signal to pin of specified I2C slave Arduino
        /// </summary>
        public static byte SendIOSignal
        {
            get
            {
                return _SendIOSignal;
            }
        }

    }
}
