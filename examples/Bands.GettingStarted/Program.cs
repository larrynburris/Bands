using Bands.Output;
using Bands.Output.ConsoleWriter;
using System;

namespace Bands.GettingStarted
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Getting started with Bands!");
            var payload = new CounterPayload();
            var innerConsoleBand = new ConsoleWriterBand<CounterPayload>(CallAddTwo);
            var incrementableBand = new IncrementableBand<CounterPayload>(innerConsoleBand);
            var outerConsoleBand = new ConsoleWriterBand<CounterPayload>(incrementableBand);
            outerConsoleBand.Enter(payload);
            Console.WriteLine("Kinda awesome, right?");
            Console.Read();
        }

        public static void CallAddTwo(CounterPayload payload)
        {
            Console.WriteLine("Calling payload.AddTwo()");
            payload.AddTwo();
        }
    }
}
