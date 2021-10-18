using System.Collections.Generic;
using MetaphysicsIndustries.Solus.Commands;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.CommandSetT
{
    public class CommandSetTest
    {
        [Test]
        public void ConstructorInstantiatesObject()
        {
            // when
            var result = new CommandSet();
            // then
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<CommandSet>(result);
            Assert.AreEqual(0, result.CountCommands());
            Assert.AreEqual(new List<string>(),
                new List<string>(result.GetCommandNames()));
        }

        class DummyCommand : Command
        {
            public DummyCommand(string name = "dummy")
            {
                Name = name;
            }

            public override void Execute(string input, SolusEnvironment env,
                ICommandData data)
            {
                throw new System.NotImplementedException();
            }

            public override string Name { get; }
        }

        [Test]
        public void AddCommandAddsCommands()
        {
            // given
            var cs = new CommandSet();
            // precondition
            Assert.AreEqual(0, cs.CountCommands());
            Assert.AreEqual(new List<string>(),
                new List<string>(cs.GetCommandNames()));
            // when
            cs.AddCommand(new DummyCommand());
            // then
            Assert.AreEqual(1, cs.CountCommands());
            Assert.AreEqual(new List<string> {"dummy"},
                new List<string>(cs.GetCommandNames()));
        }

        [Test]
        public void GetCommandGetsCommand()
        {
            // given
            var cs = new CommandSet();
            var dc = new DummyCommand();
            cs.AddCommand(dc);
            // precondition
            Assert.AreEqual(1, cs.CountCommands());
            Assert.AreEqual(new List<string> {"dummy"},
                new List<string>(cs.GetCommandNames()));
            // when
            var result = cs.GetCommand("dummy");
            // then
            Assert.AreSame(dc, result);
        }

        [Test]
        public void GetCommandNamesGetsAllCommandsNames()
        {
            // given
            var cs = new CommandSet();
            var dc1 = new DummyCommand();
            var dc2 = new DummyCommand("dummy2");
            cs.AddCommand(dc1);
            cs.AddCommand(dc2);
            // precondition
            Assert.AreEqual(2, cs.CountCommands());
            // when
            var result = new HashSet<string>(cs.GetCommandNames());
            // then
            Assert.AreEqual(new HashSet<string> {"dummy", "dummy2"},
                result);
        }

        [Test]
        public void SetCommandAddsCommandByName()
        {
            // given
            var cs = new CommandSet();
            var dc = new DummyCommand();
            // precondition
            Assert.AreEqual("dummy", dc.Name);
            Assert.AreEqual(0, cs.CountCommands());
            // when
            cs.SetCommand("something", dc);
            // then
            Assert.AreEqual(1, cs.CountCommands());
            Assert.AreEqual(new HashSet<string> {"something"},
                new HashSet<string>(cs.GetCommandNames()));
        }

        [Test]
        public void WarningSetCommandWillReuseObject()
        {
            // given
            var cs = new CommandSet();
            var dc = new DummyCommand();
            cs.AddCommand(dc);
            // precondition
            Assert.AreEqual("dummy", dc.Name);
            Assert.AreEqual(1, cs.CountCommands());
            Assert.AreEqual(new HashSet<string> {"dummy"},
                new HashSet<string>(cs.GetCommandNames()));
            // when
            cs.SetCommand("something", dc);
            // then
            Assert.AreEqual(2, cs.CountCommands());
            Assert.AreEqual(new HashSet<string> {"dummy", "something"},
                new HashSet<string>(cs.GetCommandNames()));
            // and
            Assert.AreSame(cs.GetCommand("dummy"),
                cs.GetCommand("something"));
        }

        [Test]
        public void ContainsCommandMissingCommandNameYieldsFalse()
        {
            // given
            var cs = new CommandSet();
            // precondition
            Assert.AreEqual(0, cs.CountCommands());
            Assert.AreEqual(new HashSet<string>(),
                new HashSet<string>(cs.GetCommandNames()));
            // when
            var result = cs.ContainsCommand("something");
            // then
            Assert.IsFalse(result);
        }

        [Test]
        public void ContainsCommandWithCommandPresentYieldsTrue()
        {
            // given
            var cs = new CommandSet();
            cs.AddCommand(new DummyCommand());
            // precondition
            Assert.AreEqual(1, cs.CountCommands());
            Assert.AreEqual(new HashSet<string> {"dummy"},
                new HashSet<string>(cs.GetCommandNames()));
            // when
            var result = cs.ContainsCommand("dummy");
            // then
            Assert.IsTrue(result);
        }

        [Test]
        public void ContainsCommandMatchesAssignedNameNotNameProperty()
        {
            // given
            var cs = new CommandSet();
            var dc = new DummyCommand("dummy");
            cs.SetCommand("something", new DummyCommand());
            // precondition
            Assert.AreEqual(1, cs.CountCommands());
            Assert.AreEqual(new HashSet<string> {"something"},
                new HashSet<string>(cs.GetCommandNames()));
            // expect
            Assert.IsFalse(cs.ContainsCommand("dummy"));
            Assert.IsTrue(cs.ContainsCommand("something"));
        }

        [Test]
        public void RemoveCommandRemovesCommand()
        {
            // given
            var cs = new CommandSet();
            var dc = new DummyCommand();
            cs.AddCommand(dc);
            // precondition
            Assert.AreEqual("dummy", dc.Name);
            Assert.AreEqual(1, cs.CountCommands());
            Assert.AreEqual(new HashSet<string> {"dummy"},
                new HashSet<string>(cs.GetCommandNames()));
            // when
            cs.RemoveCommand("dummy");
            // then
            Assert.AreEqual(0, cs.CountCommands());
            Assert.AreEqual(new HashSet<string>(),
                new HashSet<string>(cs.GetCommandNames()));
        }

        [Test]
        public void RemoveCommandUnknownCommandYieldsNoChange()
        {
            // given
            var cs = new CommandSet();
            var dc = new DummyCommand();
            cs.AddCommand(dc);
            // precondition
            Assert.AreEqual("dummy", dc.Name);
            Assert.AreEqual(1, cs.CountCommands());
            Assert.AreEqual(new HashSet<string> {"dummy"},
                new HashSet<string>(cs.GetCommandNames()));
            Assert.IsFalse(cs.ContainsCommand("something"));
            // when
            cs.RemoveCommand("something");
            // then
            Assert.AreEqual(1, cs.CountCommands());
            Assert.AreEqual(new HashSet<string> {"dummy"},
                new HashSet<string>(cs.GetCommandNames()));
        }
    }
}
