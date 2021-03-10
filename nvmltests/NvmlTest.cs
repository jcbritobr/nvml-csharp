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
        public void NvmlInitializationTest()
        {
            try
            {
                var device = IntPtr.Zero;
                NvGpu.NvmlInitV2();
                NvGpu.NvmlShutdown();
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}