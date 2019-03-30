using RpiUNO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SampleRpiUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void TurnOn_Click(object sender, RoutedEventArgs e)
        {
            await UnoI2C.ReadWriteAsync(42, Mode.SendIOSignal, 11, PinValue.High);
        }

        private async void TurnOff_Click(object sender, RoutedEventArgs e)
        {
            await UnoI2C.ReadWriteAsync(42, Mode.SendIOSignal, 11, PinValue.Low);
        }
    }
}
