
/*
 *  MetaphysicsIndustries.Solus
 *  Copyright (C) 2006-2025 Metaphysics Industries, Inc., Richard Sartor
 *
 *  This library is free software; you can redistribute it and/or
 *  modify it under the terms of the GNU Lesser General Public
 *  License as published by the Free Software Foundation; either
 *  version 3 of the License, or (at your option) any later version.
 *
 *  This library is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 *  Lesser General Public License for more details.
 *
 *  You should have received a copy of the GNU Lesser General Public
 *  License along with this library; if not, write to the Free Software
 *  Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301
 *  USA
 *
 */

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
            Assert.That(result.CountCommands(), Is.EqualTo(0));
            Assert.That(new List<string>(result.GetCommandNames()),
                Is.EqualTo(new List<string>()));
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
            Assert.That(cs.CountCommands(), Is.EqualTo(0));
            Assert.That(new List<string>(cs.GetCommandNames()),
                Is.EqualTo(new List<string>()));
            // when
            cs.AddCommand(new DummyCommand());
            // then
            Assert.That(cs.CountCommands(), Is.EqualTo(1));
            Assert.That(new List<string>(cs.GetCommandNames()),
                Is.EqualTo(new List<string> {"dummy"}));
        }

        [Test]
        public void GetCommandGetsCommand()
        {
            // given
            var cs = new CommandSet();
            var dc = new DummyCommand();
            cs.AddCommand(dc);
            // precondition
            Assert.That(cs.CountCommands(), Is.EqualTo(1));
            Assert.That(new List<string>(cs.GetCommandNames()),
                Is.EqualTo(new List<string> {"dummy"}));
            // when
            var result = cs.GetCommand("dummy");
            // then
            Assert.That(result, Is.SameAs(dc));
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
            Assert.That(cs.CountCommands(), Is.EqualTo(2));
            // when
            var result = new HashSet<string>(cs.GetCommandNames());
            // then
            Assert.That(result,
                Is.EqualTo(new HashSet<string> {"dummy", "dummy2"}));
        }

        [Test]
        public void SetCommandAddsCommandByName()
        {
            // given
            var cs = new CommandSet();
            var dc = new DummyCommand();
            // precondition
            Assert.That(dc.Name, Is.EqualTo("dummy"));
            Assert.That(cs.CountCommands(), Is.EqualTo(0));
            // when
            cs.SetCommand("something", dc);
            // then
            Assert.That(cs.CountCommands(), Is.EqualTo(1));
            Assert.That(new HashSet<string>(cs.GetCommandNames()),
                Is.EqualTo(new HashSet<string> {"something"}));
        }

        [Test]
        public void WarningSetCommandWillReuseObject()
        {
            // given
            var cs = new CommandSet();
            var dc = new DummyCommand();
            cs.AddCommand(dc);
            // precondition
            Assert.That(dc.Name, Is.EqualTo("dummy"));
            Assert.That(cs.CountCommands(), Is.EqualTo(1));
            Assert.That(new HashSet<string>(cs.GetCommandNames()),
                Is.EqualTo(new HashSet<string> {"dummy"}));
            // when
            cs.SetCommand("something", dc);
            // then
            Assert.That(cs.CountCommands(), Is.EqualTo(2));
            Assert.That(new HashSet<string>(cs.GetCommandNames()),
                Is.EqualTo(new HashSet<string> {"dummy", "something"}));
            // and
            Assert.That(cs.GetCommand("something"),
                Is.SameAs(cs.GetCommand("dummy")));
        }

        [Test]
        public void ContainsCommandMissingCommandNameYieldsFalse()
        {
            // given
            var cs = new CommandSet();
            // precondition
            Assert.That(cs.CountCommands(), Is.EqualTo(0));
            Assert.That(new HashSet<string>(cs.GetCommandNames()),
                Is.EqualTo(new HashSet<string>()));
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
            Assert.That(cs.CountCommands(), Is.EqualTo(1));
            Assert.That(new HashSet<string>(cs.GetCommandNames()),
                Is.EqualTo(new HashSet<string> {"dummy"}));
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
            Assert.That(cs.CountCommands(), Is.EqualTo(1));
            Assert.That(new HashSet<string>(cs.GetCommandNames()),
                Is.EqualTo(new HashSet<string> {"something"}));
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
            Assert.That(dc.Name, Is.EqualTo("dummy"));
            Assert.That(cs.CountCommands(), Is.EqualTo(1));
            Assert.That(new HashSet<string>(cs.GetCommandNames()),
                Is.EqualTo(new HashSet<string> {"dummy"}));
            // when
            cs.RemoveCommand("dummy");
            // then
            Assert.That(cs.CountCommands(), Is.EqualTo(0));
            Assert.That(new HashSet<string>(cs.GetCommandNames()),
                Is.EqualTo(new HashSet<string>()));
        }

        [Test]
        public void RemoveCommandUnknownCommandYieldsNoChange()
        {
            // given
            var cs = new CommandSet();
            var dc = new DummyCommand();
            cs.AddCommand(dc);
            // precondition
            Assert.That(dc.Name, Is.EqualTo("dummy"));
            Assert.That(cs.CountCommands(), Is.EqualTo(1));
            Assert.That(new HashSet<string>(cs.GetCommandNames()),
                Is.EqualTo(new HashSet<string> {"dummy"}));
            Assert.IsFalse(cs.ContainsCommand("something"));
            // when
            cs.RemoveCommand("something");
            // then
            Assert.That(cs.CountCommands(), Is.EqualTo(1));
            Assert.That(new HashSet<string>(cs.GetCommandNames()),
                Is.EqualTo(new HashSet<string> {"dummy"}));
        }
    }
}
