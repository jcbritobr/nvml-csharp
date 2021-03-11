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
        public void ApiRestictionsTest()
        {
            try
            {
                NvGpu.NvmlInitV2();
                var device = IntPtr.Zero;
                device = NvGpu.NvmlDeviceGetHandleByIndex(0);
                var state = NvGpu.NvmlDeviceGetAPIRestriction(device, NvmlRestrictedAPI.NVML_RESTRICTED_API_SET_APPLICATION_CLOCKS);

                NvGpu.NvmlShutdown();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        [Test]
        public void RetrieveProcessListTest()
        {
            try
            {
                NvGpu.NvmlInitV2();
                var device = IntPtr.Zero;
                device = NvGpu.NvmlDeviceGetHandleByIndex(0);
                var (list, count) = NvGpu.NvmlDeviceGetComputeRunningProcesses(device);
                NvGpu.NvmlShutdown();
            }
            catch (Exception e)
            {
                Assert.Fail(e.ToString());
            }
        }

        [Test]
        public void RetrieveProcessNameTest()
        {
            try
            {
                NvGpu.NvmlInitV2();
                var device = IntPtr.Zero;
                device = NvGpu.NvmlDeviceGetHandleByIndex(0);
                var (list, count) = NvGpu.NvmlDeviceGetComputeRunningProcesses(device);
                if (count > 0)
                {
                    TestContext.Progress.WriteLine(">> Testing NvmlSystemGetProcessName as we have processes in gpu");
                    string processName = NvGpu.NvmlSystemGetProcessName(list[0].Pid, 30);
                }
                else
                {
                    TestContext.Progress.WriteLine(">> Testing NvmlSystemGetProcessName with inexistent pid 0");
                    string processName = NvGpu.NvmlSystemGetProcessName(0, 30);
                }
                NvGpu.NvmlShutdown();
            }
            catch (Exception e)
            {
                if (e.Message.Equals("NVML_ERROR_NOT_FOUND"))
                {
                    Assert.Pass();
                }
                Assert.Fail(e.ToString());
            }
        }

        [Test]
        public void RetrieveNvmlVersionTest()
        {
            try
            {
                NvGpu.NvmlInitV2();
                string version = NvGpu.nvmlSystemGetNVMLVersion(10);
                if (version.Length == 0 || version == null)
                {
                    Assert.Fail("Something fail to acquire nvml version.");
                }
                NvGpu.NvmlShutdown();
            }
            catch (Exception e)
            {
                Assert.Fail(e.ToString());
            }
        }

        [Test]
        public void RetrieveDriverVersionTest()
        {
            try
            {
                NvGpu.NvmlInitV2();
                string driverVersion = NvGpu.nvmlSystemGetDriverVersion(10);
                if (driverVersion.Length == 0 || driverVersion == null)
                {
                    Assert.Fail("Something fail to acquire driver version.");
                }
                NvGpu.NvmlShutdown();
            }
            catch (Exception e)
            {
                Assert.Fail(e.ToString());
            }
        }

        [Test]
        public void RetriveCudaDriverVersionTest()
        {
            try
            {
                NvGpu.NvmlInitV2();
                int version = NvGpu.NvmlSystemGetCudaDriverVersion();
                int major = NvGpu.CudaDriverVersionMajor(version);
                NvGpu.NvmlShutdown();
                NvGpu.NvmlInitV2();
                version = NvGpu.NvmlSystemGetCudaDriverVersionV2();
                major = NvGpu.CudaDriverVersionMajor(version);
                NvGpu.NvmlShutdown();
            }
            catch (Exception e)
            {
                Assert.Fail(e.ToString());
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
        public void RecoverDeviceTest()
        {
            try
            {
                var device = IntPtr.Zero;
                NvGpu.NvmlInitV2();
                device = NvGpu.NvmlDeviceGetHandleByIndex(0);
                if (IntPtr.Zero == device)
                {
                    Assert.Fail("Device cant be IntPtr.Zero.");
                }
                NvGpu.NvmlShutdown();
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [Test]
        public void GetGpuTemperatureTest()
        {
            try
            {
                var device = IntPtr.Zero;
                NvGpu.NvmlInitV2();
                device = NvGpu.NvmlDeviceGetHandleByIndex(0);
                var temperature = NvGpu.NvmlDeviceGetTemperature(device, NvmlTemperatureSensor.NVML_TEMPERATURE_GPU);
                if (!(temperature >= 40 && temperature <= 80))
                {
                    Assert.Fail("Cant get temperature.");
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