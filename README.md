# Windows Runtime Component to enable Communication with Arduino in Windows 10 IoT Core UWPs

The Package currently supports communication over I2C bus.

Include `using RpiUno;` where you need to use the package.

### Modes

The package classifies Arduino operations into three modes:

1) `RecieveSensorData`: Sends a byte '0' to arduino to indicate operation to receive sensor data.
2) `RecieveDeviceState`: Sends a byte '1' to arduino to indicate operation to receive a Pin's state 
3) `SendIOSignal`: Sends a byte '2' to arduino to indicate operation to turn a pin `HIGH` or `LOW`

### Read-Write to Arduino

Make an asynchronus call to `ReadWriteAsync()` method to perform a read or write operation on Arduino.

#### Turn a pin High / Low

`await UnoI2C.ReadWriteAsync("Mention slave address (int)", Mode.SendIOSignal, (Pin Number as byte), PinValue.High);`

#### Read Sensor Data

`await UnoI2C.ReadWriteAsync(Mention slave address (int), Mode.RecieveSensorData,  (Sensor Pin Number as byte));`

#### Read Pin State
`await UnoI2C.ReadWriteAsync(Mention slave address (int), Mode.RecieveDeviceState,  (Sensor Pin Number as byte));`

