using System.Collections.Generic;

namespace MetaphysicsIndustries.Solus.Commands
{
    public class CommandSet
    {
        private readonly Dictionary<string, Command> _commands =
            new Dictionary<string, Command>();

        public void AddCommand(Command command) =>
            SetCommand(command.Name, command);

        public Command GetCommand(string name) =>
            _commands.ContainsKey(name) ? _commands[name] : null;

        public void SetCommand(string name, Command value) =>
            _commands[name] = value;

        public bool ContainsCommand(string name) =>
            _commands.ContainsKey(name);

        public void RemoveCommand(string name) => _commands.Remove(name);
        public int CountCommands() => _commands.Count;
        public IEnumerable<string> GetCommandNames() => _commands.Keys;
    }
}