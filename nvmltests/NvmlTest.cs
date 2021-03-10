using System;
using NUnit.Framework;
using Nvidia.Nvml;

namespace NvlmTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void RetriveCudaDriverVersion()
        {
            try
            {
                NvGpu.NvmlInitV2();
                int version = NvGpu.NvmlSystemGetCudaDriverVersion();
                int major = NvGpu.CudaDriverVersionMajor(version);
                NvGpu.NvmlShutdown();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        [Test]
        public void InitializationTest()
        {
            try
            {
                NvGpu.NvmlInit();
                NvGpu.NvmlShutdown();
                NvGpu.NvmlInitV2();
                NvGpu.NvmlShutdown();
                NvGpu.NvmlInitWithFlags(NvGpu.NVML_INIT_FLAG_NO_ATTACH);
                NvGpu.NvmlShutdown();
                NvGpu.NvmlInitWithFlags(NvGpu.NVML_INIT_FLAG_NO_GPUS);
                NvGpu.NvmlShutdown();
                NvGpu.NvmlInitWithFlags(NvGpu.NVML_INIT_FLAG_NO_GPUS | NvGpu.NVML_INIT_FLAG_NO_ATTACH);
                NvGpu.NvmlShutdown();
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [Test]
        public void RecoverDevice()
        {
            try
            {
                var device = IntPtr.Zero;
                NvGpu.NvmlInitV2();
                device = NvGpu.NvmlDeviceGetHandleByIndex(0);
                if (IntPtr.Zero == device)
                {
                    Assert.Fail("Device cant be IntPtr.Zero");
                }
                NvGpu.NvmlShutdown();
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [Test]
        public void GetGpuTemperature()
        {
            try
            {
                var device = IntPtr.Zero;
                NvGpu.NvmlInitV2();
                device = NvGpu.NvmlDeviceGetHandleByIndex(0);
                var temperature = NvGpu.NvmlDeviceGetTemperature(device, NvmlTemperatureSensor.NVML_TEMPERATURE_GPU);
                if (!(temperature >= 40 && temperature <= 80))
                {
                    Assert.Fail("Cant get temperature");
                }
                NvGpu.NvmlShutdown();
            }
            catch (Exception e)
            {
                Assert.Fail(e.ToString());
            }

        }
    }
}