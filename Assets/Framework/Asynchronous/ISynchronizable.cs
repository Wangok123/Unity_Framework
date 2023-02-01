using System;

namespace GameCore.Framework.Asynchronous
{
    public interface ISynchronizable
    {
        bool WaitForDone();

        object WaitForResult(int millisecondsTimeout = 0);

        object WaitForResult(TimeSpan timeout);
    }
}