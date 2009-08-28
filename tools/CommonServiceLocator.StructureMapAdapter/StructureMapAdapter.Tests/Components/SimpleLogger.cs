using System;

namespace StructureMapAdapter.Tests.Components
{
    public class SimpleLogger : ILogger
    {
        public void Log(string msg)
        {
            Console.WriteLine(msg);
        }
    }
}