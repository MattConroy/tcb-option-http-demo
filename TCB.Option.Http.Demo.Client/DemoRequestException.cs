using System;

namespace TCB.Option.Http.Demo.Client
{
    internal class DemoRequestException : Exception
    {
        public DemoRequestException(ErrorMessage errorMessage) : base($"'{errorMessage}'") { }
    }
}