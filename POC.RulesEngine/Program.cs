using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using Ninject.Extensions.Conventions;
using Ninject.Modules;

namespace POC.RulesEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            var kernel = new StandardKernel();
            kernel.Load<RulesEngineModule>();

            var executer = kernel.Get<IExecuter>();
            executer.Execute("Find");
        }
    }

    public interface IExecuter
    {
        void Execute(string actionName);
    }

    public class Executer : IExecuter
    {
        private readonly IEnumerable<IAction> _availableActions;

        public Executer(IEnumerable<IAction> availableActions)
        {
            _availableActions = availableActions;
        }

        public void Execute(string actionName)
        {
            var action = _availableActions.SingleOrDefault(a => a.DoesMatch(actionName));
            action.Execute();
        }
    }

    public interface IAction
    {
        bool DoesMatch(string actionName);
        object Execute();
    }

    class AddAction : IAction
    {
        public bool DoesMatch(string actionName)
        {
            return actionName == "Add";
        }

        public object Execute()
        {
            throw new NotImplementedException();
        }
    }

    class FindAction : IAction
    {
        public bool DoesMatch(string actionName)
        {
            return actionName == "Find";
        }

        public object Execute()
        {
            throw new NotImplementedException();
        }
    }

    public class RulesEngineModule :NinjectModule
    {
        public override void Load()
        {
            Bind<IExecuter>().To<Executer>();
            Bind<IAction>().To<FindAction>();
        }
    }
}
