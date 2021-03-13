using System;
using System.Text;
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
        public void SetDeviceComputeMode()
        {
            try
            {
                NvGpu.NvmlInitV2();
                IntPtr device = NvGpu.NvmlDeviceGetHandleByIndex(0);
                var original = NvGpu.NvmlDeviceGetComputeMode(device);
                NvGpu.NvmlDeviceSetComputeMode(device, NvmlComputeMode.NVML_COMPUTEMODE_PROHIBITED);
                var current = NvGpu.NvmlDeviceGetComputeMode(device);
                if (NvmlComputeMode.NVML_COMPUTEMODE_PROHIBITED != current)
                {
                    Assert.Fail("Must return NVML_COMPUTEMODE_PROHIBITED mode");
                }

                NvGpu.NvmlDeviceSetComputeMode(device, original);
                current = NvGpu.NvmlDeviceGetComputeMode(device);
                if (original != current)
                {
                    Assert.Fail("Must return original mode");
                }

                NvGpu.NvmlShutdown();
            }
            catch (Exception e)
            {
                if (e.Message.Equals("NVML_ERROR_NO_PERMISSION"))
                {
                    TestContext.Progress.WriteLine("SetDeviceComputeMode >> In order to this test pass, the test case must be running with admin permission");
                } 
                else if (e.Message.Equals("NVML_ERROR_NOT_SUPPORTED"))
                {
                    TestContext.Progress.WriteLine("SetDeviceComputeMode >> This mode isn't supported by this device");
                    Assert.Pass();
                }
                Assert.Fail(e.ToString());
            }
        }

        [Test]
        public void RetrieveDeviceComputeMode()
        {
            try
            {
                NvGpu.NvmlInitV2();
                IntPtr device = NvGpu.NvmlDeviceGetHandleByIndex(0);
                var computeMode = NvGpu.NvmlDeviceGetComputeMode(device);
                NvGpu.NvmlShutdown();
            }
            catch (Exception e)
            {
                Assert.Fail(e.ToString());
            }
        }

        [Test]
        public void RetrieveDeviceCount()
        {
            try
            {
                NvGpu.NvmlInitV2();
                var count = NvGpu.NvmlDeviceGetCountV2();
                NvGpu.NvmlShutdown();
            }
            catch (Exception e)
            {
                Assert.Fail(e.ToString());
            }
        }

        [Test]
        public void RetrieveDevicePciInfo()
        {
            try
            {
                NvGpu.NvmlInitV2();
                var device = NvGpu.NvmlDeviceGetHandleByIndex(0);
                var info = NvGpu.NvmlDeviceGetPciInfoV3(device);
                byte[] busIdData = Array.ConvertAll(info.busId, (a) => (byte)a);
                byte[] busIdLegacyData = Array.ConvertAll(info.busIdLegacy, (a) => (byte)a);
                string busId = Encoding.Default.GetString(busIdData);
                string busIdLegacy = Encoding.Default.GetString(busIdLegacyData);
                NvGpu.NvmlShutdown();
            }
            catch (Exception e)
            {
                Assert.Fail(e.ToString());
            }
        }

        [Test]
        public void RetrieveDeviceName()
        {
            try
            {
                NvGpu.NvmlInitV2();
                var device = NvGpu.NvmlDeviceGetHandleByIndex(0);
                string name = NvGpu.NvmlDeviceGetName(device);
                NvGpu.NvmlShutdown();
            }
            catch (Exception e)
            {
                Assert.Fail(e.ToString());
            }
        }

        [Test]
        public void RetrieveDeviceBoardPartNumberTest()
        {
            try
            {
                NvGpu.NvmlInitV2();
                var device = NvGpu.NvmlDeviceGetHandleByIndex(0);
                var partnumber = NvGpu.NvmlDeviceGetBoardPartNumber(device, 20);

                NvGpu.NvmlShutdown();
            }
            catch (Exception e)
            {
                if (e.Message.Equals("NVML_ERROR_NOT_SUPPORTED"))
                {
                    Assert.Pass("NVML_ERROR_NOT_SUPPORTED means vbios fields have not been filled");
                }
                Assert.Fail(e.ToString());
            }
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
                    TestContext.Progress.WriteLine("RetrieveProcessNameTest >> Testing NvmlSystemGetProcessName as we have processes in gpu");
                    string processName = NvGpu.NvmlSystemGetProcessName(list[0].Pid, 30);
                }
                else
                {
                    TestContext.Progress.WriteLine("RetrieveProcessNameTest >> Testing NvmlSystemGetProcessName with inexistent pid 0");
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