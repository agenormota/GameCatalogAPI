using Microsoft.AspNetCore.Mvc;
using System;
using System.Text;
using System.Threading.Tasks;

namespace GameCatalogAPI.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class LifeCycleIDController : ControllerBase
    {
        public readonly ISingletonExample _exemploSingleton1;
        public readonly ISingletonExample _exemploSingleton2;

        public readonly IScopedExample _exemploScoped1;
        public readonly IScopedExample _exemploScoped2;

        public readonly ITransientExample _exemploTransient1;
        public readonly ITransientExample _exemploTransient2;

        public LifeCycleIDController(ISingletonExample exemploSingleton1,
                                       ISingletonExample exemploSingleton2,
                                       IScopedExample exemploScoped1,
                                       IScopedExample exemploScoped2,
                                       ITransientExample exemploTransient1,
                                       ITransientExample exemploTransient2)
        {
            _exemploSingleton1 = exemploSingleton1;
            _exemploSingleton2 = exemploSingleton2;
            _exemploScoped1 = exemploScoped1;
            _exemploScoped2 = exemploScoped2;
            _exemploTransient1 = exemploTransient1;
            _exemploTransient2 = exemploTransient2;
        }

        [HttpGet]
        public Task<string> Get()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"Singleton 1: {_exemploSingleton1.Id}");
            stringBuilder.AppendLine($"Singleton 2: {_exemploSingleton2.Id}");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine($"Scoped 1: {_exemploScoped1.Id}");
            stringBuilder.AppendLine($"Scoped 2: {_exemploScoped2.Id}");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine($"Transient 1: {_exemploTransient1.Id}");
            stringBuilder.AppendLine($"Transient 2: {_exemploTransient2.Id}");

            return Task.FromResult(stringBuilder.ToString());
        }

    }

    public interface IGeneralExample
    {
        public Guid Id { get; }
    }

    public interface ISingletonExample :IGeneralExample
    { }

    public interface IScopedExample : IGeneralExample
    { }

    public interface ITransientExample : IGeneralExample
    { }

    public class LifeCycleExample : ISingletonExample, IScopedExample, ITransientExample
    {
        private readonly Guid _guid;

        public LifeCycleExample()
        {
            _guid = Guid.NewGuid();
        }

        public Guid Id => _guid;
    }

}
