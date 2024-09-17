using System.Device.Gpio;
using Iot.Device.Hcsr501;

namespace IoT.TrafficLights.Domain.Modules.TrafficSensor;

public class SensorModuleMock : ISensorModule
{
    public event EventHandler<Models.TrafficSensor.TrafficSensor>? SensorValueChangedEvent;

    public void Invoke()
    {
        Task.Run(() =>
        {
            while (true)
            {
                if (DateTime.Now.Minute % 2 == 0)
                {
                    SensorValueChanged(new Hcsr501ValueChangedEventArgs(PinValue.High));
                }
                else
                {
                    SensorValueChanged(new Hcsr501ValueChangedEventArgs(PinValue.Low));
                }
            }
            // ReSharper disable once FunctionNeverReturns
        });
    }

    private void SensorValueChanged(Hcsr501ValueChangedEventArgs args)
    {
        var sensor = new Models.TrafficSensor.TrafficSensor
        {
            HighValue = args.PinValue == PinValue.High
        };

        SensorValueChangedEvent!.Invoke(this, sensor);
    }
}