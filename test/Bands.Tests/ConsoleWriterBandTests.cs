using System;
using Bands.Output;
using Moq;
using NUnit.Framework;
using System.IO;
using System.Linq;

namespace Bands.Tests
{
    [TestFixture]
    public class ConsoleWriterBandTests
    {
        public class NewWritablePayload : WritablePayload
        {
            public bool ExecutedPayloadHandler { get; set; }
            
            public override string ToString()
            {
                return string.Format("0");
            }
        }

        NewWritablePayload Payload;
        MemoryStream Stream;
        StreamWriter ConsoleStream;

        [SetUp]
        public void SetUp()
        {
            Stream = new MemoryStream();
            ConsoleStream = new StreamWriter(Stream);
            Console.SetOut(ConsoleStream);
        }

        public string GetStreamAsString()
        {
            ConsoleStream.Flush();
            Stream.Position = 0;
            var sr = new StreamReader(Stream);
            return sr.ReadToEnd();
        }

        [TearDown]
        public void TearDown()
        {
            ConsoleStream.Dispose();
            Stream.Dispose();
        }


        [Test]
        public void TestConsoleWriterBand_ExecutedPayloadHandler()
        {
            Payload = new NewWritablePayload();
            var band = new ConsoleWriterBand<NewWritablePayload>(PayloadHandler);
            band.Enter(Payload);
            var streamString = GetStreamAsString();
            var consoleWriteCount = streamString.Count(l => l == '0');
            Assert.IsTrue(Payload.ExecutedPayloadHandler = true);
            Assert.IsTrue(consoleWriteCount == 2);
        }

        [Test]
        public void TestConsoleWriterBand_2BandsExecutedPayloadHandler()
        {
            Payload = new NewWritablePayload();
            
            var innerConsoleBand = new ConsoleWriterBand<NewWritablePayload>(PayloadHandler);
            var outerConsoleBand = new ConsoleWriterBand<NewWritablePayload>(innerConsoleBand);
            outerConsoleBand.Enter(Payload);
            var streamString = GetStreamAsString();
            var consoleWriteCount = streamString.Count(l => l == '0');
            Assert.IsTrue(Payload.ExecutedPayloadHandler = true);
            Assert.IsTrue(consoleWriteCount == 4);
        }
        
        public void PayloadHandler(NewWritablePayload payload)
        {
            payload.ExecutedPayloadHandler = true;
        }
    }
}
