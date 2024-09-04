using System.Device.Gpio;
using Iot.Device.Hcsr501;

namespace TrafficLights.Domain.Modules.TrafficSensor;

public class TrafficSensorModulePi : ITrafficSensorModule
{
    private const int Output = 21;

    public event EventHandler<Models.TrafficSensor.TrafficSensor>? SensorValueChangedEvent;

    public void Invoke()
    {
        Hcsr501 hcsr501 = new(Output);
        hcsr501.Hcsr501ValueChanged += SensorValueChanged;
    }

    private void SensorValueChanged(object sender, Hcsr501ValueChangedEventArgs args)
    {
        var sensor = new Models.TrafficSensor.TrafficSensor
        {
            HighValue = args.PinValue == PinValue.High
        };
            
        SensorValueChangedEvent!.Invoke(this, sensor);
    }
}