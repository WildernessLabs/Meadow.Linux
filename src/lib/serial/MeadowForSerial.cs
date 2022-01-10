using Meadow.Devices;
using Meadow.Hardware;
using Meadow.Units;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Meadow
{
    internal static partial class Ftdi
    {
        // dev note: this is all ported from ftdi.h
        [StructLayout(LayoutKind.Sequential)]
        internal struct ftdi_context
        {
            public IntPtr usb_ctx; // struct libusb_context *
            public IntPtr usb_dev; // struct libusb_device_handle *
            public int usb_read_timeout;
            public int usb_write_timeout;
            public int type; // enum ftdi_chip_type
            public int baudrate;
            public byte bitbang_enabled;
            public IntPtr readbuffer; // unsigned char*
            public uint readbuffer_offset;            
            public uint readbuffer_remaining;
            public uint readbuffer_chunksize;
            public uint writebuffer_chunksize;
            public uint max_packet_size;

            /* FTDI FT2232C requirecments */
            public int interf;   /* 0 or 1 */
            public int index;       /* 1 or 2 */
            /* Endpoints */
            public int in_ep;
            public int out_ep;      /* 1 or 2 */
            /** Bitbang mode. 1: (default) Normal bitbang mode, 2: FT2232C SPI bitbang mode */
            public byte bitbang_mode;
            public IntPtr eeprom; // struct ftdi_eeprom *
            public IntPtr error_str; // const char*
            public int module_detach_mode; // enum ftdi_module_detach_mode 
        };
        
        private const string FTDI_LIB = "libftdi1";

        [DllImport(FTDI_LIB)] internal static extern int ftdi_init(ref ftdi_context ftdi);
        [DllImport(FTDI_LIB)] internal static extern void ftdi_deinit(ref ftdi_context ftdi);
        [DllImport(FTDI_LIB)] internal static extern int ftdi_usb_open(ref ftdi_context ftdi, int vendor, int product);
        [DllImport(FTDI_LIB)] internal static extern int ftdi_usb_open_desc(ref ftdi_context ftdi, int vendor, int product, string description, string serial);
    }

    public class Mpsse : IMeadowTransport, IPinDefinitions
    {
        public IList<IPin> AllPins => throw new NotImplementedException();
    }

    public interface IMeadowTransport
    {

    }

    public class MeadowForSerial<TTransport> : IMeadowDevice, IApp
        where TTransport : IMeadowTransport, IPinDefinitions, new()
    {
        public TTransport Pins { get; }
        public DeviceCapabilities Capabilities { get; }
        public IPlatformOS PlatformOS { get; }


        public IDigitalInputPort CreateDigitalInputPort(IPin pin, InterruptMode interruptMode = InterruptMode.None, ResistorMode resistorMode = ResistorMode.Disabled, double debounceDuration = 0, double glitchDuration = 0)
        {
            throw new NotImplementedException();
        }

        public IDigitalOutputPort CreateDigitalOutputPort(IPin pin, bool initialState = false, OutputType initialOutputType = OutputType.PushPull)
        {
            throw new NotImplementedException();
        }

        public II2cBus CreateI2cBus(int busNumber = 0)
        {
            throw new NotImplementedException();
        }

        public II2cBus CreateI2cBus(int busNumber, Frequency frequency)
        {
            throw new NotImplementedException();
        }

        public II2cBus CreateI2cBus(IPin[] pins, Frequency frequency)
        {
            throw new NotImplementedException();
        }

        public II2cBus CreateI2cBus(IPin clock, IPin data, Frequency frequency)
        {
            throw new NotImplementedException();
        }



        public IAnalogInputPort CreateAnalogInputPort(IPin pin, int sampleCount = 5, int sampleIntervalMs = 40, float voltageReference = 3.3F)
        {
            throw new NotImplementedException();
        }

        public IBiDirectionalPort CreateBiDirectionalPort(IPin pin, bool initialState = false, InterruptMode interruptMode = InterruptMode.None, ResistorMode resistorMode = ResistorMode.Disabled, PortDirectionType initialDirection = PortDirectionType.Input, double debounceDuration = 0, double glitchDuration = 0, OutputType output = OutputType.PushPull)
        {
            throw new NotImplementedException();
        }

        public IPwmPort CreatePwmPort(IPin pin, float frequency = 100, float dutyCycle = 0.5F, bool invert = false)
        {
            throw new NotImplementedException();
        }

        public ISerialMessagePort CreateSerialMessagePort(SerialPortName portName, byte[] suffixDelimiter, bool preserveDelimiter, int baudRate = 9600, int dataBits = 8, Parity parity = Parity.None, StopBits stopBits = StopBits.One, int readBufferSize = 512)
        {
            throw new NotImplementedException();
        }

        public ISerialMessagePort CreateSerialMessagePort(SerialPortName portName, byte[] prefixDelimiter, bool preserveDelimiter, int messageLength, int baudRate = 9600, int dataBits = 8, Parity parity = Parity.None, StopBits stopBits = StopBits.One, int readBufferSize = 512)
        {
            throw new NotImplementedException();
        }

        public ISerialPort CreateSerialPort(SerialPortName portName, int baudRate = 9600, int dataBits = 8, Parity parity = Parity.None, StopBits stopBits = StopBits.One, int readBufferSize = 1024)
        {
            throw new NotImplementedException();
        }

        public ISpiBus CreateSpiBus(IPin clock, IPin mosi, IPin miso, SpiClockConfiguration config)
        {
            throw new NotImplementedException();
        }

        public ISpiBus CreateSpiBus(IPin clock, IPin mosi, IPin miso, Frequency speed)
        {
            throw new NotImplementedException();
        }

        public void Initialize()
        {
            throw new NotImplementedException();
        }

        public void OnWake()
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

        public void SetClock(DateTime dateTime)
        {
            throw new NotImplementedException();
        }

        public void WatchdogEnable(TimeSpan timeout)
        {
            throw new NotImplementedException();
        }

        public void WatchdogReset()
        {
            throw new NotImplementedException();
        }

        public void WillReset()
        {
            throw new NotImplementedException();
        }

        public void WillSleep()
        {
            throw new NotImplementedException();
        }
    }
}
