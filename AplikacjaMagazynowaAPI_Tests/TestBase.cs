using Autofac.Extras.Moq;

namespace AplikacjaMagazynowaAPI_Tests
{
    internal class TestBase<T> where T : class
    {
        protected AutoMock AutoMock;
        protected T SystemUnderTest { get; set; }

        [TearDown]
        public virtual void Cleanup()
        {
            AutoMock.Dispose();
        }

        [SetUp]
        public virtual void Setup()
        {
            AutoMock = AutoMock.GetLoose();
            SystemUnderTest = AutoMock.Create<T>();
        }
    }
}