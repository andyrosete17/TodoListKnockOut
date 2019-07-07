using System;

namespace TDL.TestUtilities.BaseImplementation
{
    public abstract class BaseTestHelper<TSut>
    {
        public abstract TSut CreateSut();

        public TSut CreateSut(Action<TSut> expression)
        {
            TSut sut = this.CreateSut();

            // Apply the local settings if there are any.
            expression?.Invoke(sut);

            return sut;
        }
    }
}
