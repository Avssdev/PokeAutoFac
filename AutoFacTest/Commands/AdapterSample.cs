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
            Debug.WriteLine("Save command has been clicked.");
        }
    }

    public class OpenCommand : ICommand
    {
        public void Execute(object parameter)
        {
            Pokemon pokemon = parameter as Pokemon;
            Debug.WriteLine("Open command has been clicked.");
        }
    }

    public interface IToolbarButton
    {
        string Name { get; set; }
        void Click(object parameter);
    }

    public class ToolbarButton : IToolbarButton
    {
        readonly ICommand _command;
        public string Name { get; set; }

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
