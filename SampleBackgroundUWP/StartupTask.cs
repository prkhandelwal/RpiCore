using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using Windows.ApplicationModel.Background;
using RpiCore;
using System.Threading.Tasks;

// The Background Application template is documented at http://go.microsoft.com/fwlink/?LinkID=533884&clcid=0x409

namespace SampleBackgroundUWP
{
    public sealed class StartupTask : IBackgroundTask
    {
        public void Run(IBackgroundTaskInstance taskInstance)
        {

        }

        /// <summary>
        /// Turns off device
        /// </summary>
        /// <returns>Returns the status of device after operation</returns>
        public async Task TurnOff()
        {
            var kk = new I2CHelper();
            var Response = await kk.WriteRead(64, Mode.ReceiveSensorData, (byte)11, 0);
        }
    }
}
