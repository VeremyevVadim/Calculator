using Calculator.Domain;
using Calculator.Infrastructure;
using Calculator.Presentation;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Calculator
{
    public class CalculatorLifetimeScope : LifetimeScope
    {
        [SerializeField] private CalculatorWindowView _view;
        [SerializeField] private CalculatorSettings _settings;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_settings).AsImplementedInterfaces().AsSelf();

            builder.Register<ICalculatorRepository, PlayerPrefsRepository>(Lifetime.Singleton);
            builder.Register<IEquationValidator, EquationValidator>(Lifetime.Singleton);
            builder.Register<ICalculatorOperation, AdditionOperation>(Lifetime.Singleton);

            builder.Register<PerformCalculationModel>(Lifetime.Singleton);

            builder.RegisterComponent(_view).As<ICalculatorView>();

            builder.RegisterEntryPoint<CalculatorPresenter>();
        }
    }
}
