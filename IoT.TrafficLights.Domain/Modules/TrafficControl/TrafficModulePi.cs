using System.Device.Gpio;

namespace IoT.TrafficLights.Domain.Modules.TrafficControl;

public class TrafficModulePi : ITrafficModule
{
    private const int Red = 26;
    private const int Amber = 19;
    private const int Green = 13;
    private readonly GpioController _gpio = new();

    public void Start()
    {
        _gpio.OpenPin(Red, PinMode.Output);
        _gpio.Write(Red, PinValue.High);
        Thread.Sleep(1000);
        _gpio.OpenPin(Amber, PinMode.Output);
        _gpio.Write(Amber, PinValue.High);
        Thread.Sleep(2000);
        _gpio.Write(Red, PinValue.Low);
        _gpio.Write(Amber, PinValue.Low);
        _gpio.ClosePin(Red);
        _gpio.ClosePin(Amber);
        _gpio.OpenPin(Green, PinMode.Output);
        _gpio.Write(Green, PinValue.High);
    }

    public void Stop()
    {
        _gpio.OpenPin(Green, PinMode.Output);
        _gpio.Write(Green, PinValue.Low);
        _gpio.ClosePin(Green);
        _gpio.OpenPin(Amber, PinMode.Output);
        _gpio.Write(Amber, PinValue.High);
        Thread.Sleep(2000);
        _gpio.Write(Amber, PinValue.Low);
        _gpio.ClosePin(Amber);
        _gpio.OpenPin(Red, PinMode.Output);
        _gpio.Write(Red, PinValue.High);
    }

    public void Standby()
    {
        Thread.Sleep(500);
        _gpio.OpenPin(Amber, PinMode.Output);
        _gpio.Write(Amber, PinValue.High);
        Thread.Sleep(1000);
        _gpio.Write(Amber, PinValue.Low);
        _gpio.ClosePin(Amber);
        Thread.Sleep(500);
    }

    public void Shut()
    {
        _gpio.OpenPin(Red, PinMode.Output);
        _gpio.Write(Red, PinValue.Low);
        _gpio.ClosePin(Red);
        _gpio.OpenPin(Amber, PinMode.Output);
        _gpio.Write(Amber, PinValue.Low);
        _gpio.ClosePin(Amber);
        _gpio.OpenPin(Green, PinMode.Output);
        _gpio.Write(Green, PinValue.Low);
        _gpio.ClosePin(Green);
    }
}