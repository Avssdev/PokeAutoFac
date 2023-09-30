using AutoFacTest.Models;
using System.Diagnostics;

namespace AutoFacTest.Commands
{
    public interface ICommand
    {
        void Execute(object parameter);
    }

    public class SaveCommand : ICommand
    {
        public void Execute(object parameter)
        {
            Pokemon pokemon = parameter as Pokemon;
            Debug.WriteLine("Saving command...");
        }
    }

    public class OpenCommand : ICommand
    {
        public void Execute(object parameter)
        {
            Pokemon pokemon = parameter as Pokemon;
            Debug.WriteLine("Opening command...");
        }
    }

    public class ToolbarButton
    {
        readonly ICommand _command;
        public string Name { get; private set; }

        public ToolbarButton(ICommand command, string name)
        {
            _command = command;
            Name = name;
        }

        public void Click(object parameter)
        {
            _command.Execute(parameter);
        }
    }
}
