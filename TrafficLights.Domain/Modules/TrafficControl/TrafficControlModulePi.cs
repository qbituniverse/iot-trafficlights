using System.Device.Gpio;

namespace TrafficLights.Domain.Modules.TrafficControl;

public class TrafficControlModulePi : ITrafficControlModule
{
    private const int Red = 18;
    private const int Amber = 17;
    private const int Green = 23;
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

    public void Test(int blinkTime, int pinNumber)
    {
        _gpio.OpenPin(pinNumber, PinMode.Output);
        _gpio.Write(pinNumber, PinValue.High);
        Thread.Sleep(blinkTime);
        _gpio.Write(pinNumber, PinValue.Low);
        _gpio.ClosePin(pinNumber);
    }
}