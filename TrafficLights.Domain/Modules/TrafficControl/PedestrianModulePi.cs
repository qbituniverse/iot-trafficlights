using System.Device.Gpio;

namespace TrafficLights.Domain.Modules.TrafficControl;

public class PedestrianModulePi : IPedestrianModule
{
    private const int Red = 27;
    private const int Green = 17;
    private readonly GpioController _gpio = new();

    public void Start()
    {
        _gpio.OpenPin(Red, PinMode.Output);
        _gpio.Write(Red, PinValue.High);
        Thread.Sleep(500);
        _gpio.Write(Red, PinValue.Low);
        _gpio.ClosePin(Red);
        _gpio.OpenPin(Green, PinMode.Output);
        _gpio.Write(Green, PinValue.High);
        _gpio.ClosePin(Green);
    }

    public void Stop()
    {
        _gpio.OpenPin(Green, PinMode.Output);
        for (int i = 0; i < 10; i++)
        {
            _gpio.Write(Green, PinValue.High);
            Thread.Sleep(100);
            _gpio.Write(Green, PinValue.Low);
            Thread.Sleep(100);
        }
        _gpio.ClosePin(Green);
        _gpio.OpenPin(Red, PinMode.Output);
        _gpio.Write(Red, PinValue.High);
        _gpio.ClosePin(Red);
    }

    public void Shut()
    {
        _gpio.OpenPin(Green, PinMode.Output);
        _gpio.Write(Green, PinValue.Low);
        _gpio.ClosePin(Green);
        _gpio.OpenPin(Red, PinMode.Output);
        _gpio.Write(Red, PinValue.Low);
        _gpio.ClosePin(Red);
    }
}