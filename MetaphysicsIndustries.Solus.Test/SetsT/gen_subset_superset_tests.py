types={
"MathObjects": ["Sets","AllFunctions", "Tensors", "Booleans", "Reals", "Strings", "Intervals", "Expressions",],
    "Sets": [],
    "AllFunctions": ["Functions"],
        "Functions": [],
    "Tensors": ["AllVectors", "AllMatrices",],
        "AllVectors": ["Vectors",],
            "Vectors": [],
        "AllMatrices": ["Matrices",],
            "Matrices": [],
    "Reals": [],
    "Strings": [],
    "Intervals": [],
    "Booleans": [],
    "Expressions": ["Literals", "ComponentAccesses", "FunctionCalls", "IntervalExpressions", "TensorExpressions", "VariableAccesses",],
        "Literals": [],
        "ComponentAccesses": [],
        "FunctionCalls": [],
        "IntervalExpressions": [],
        "TensorExpressions": ["MatrixExpressions", "VectorExpressions",],
            "MatrixExpressions": [],
            "VectorExpressions": [],
        "VariableAccesses": [],
        
        }

all_types = list(types.keys())

class MathType:
    def __init__(self, name):
        self.name = name
        self.subtypes = []
        self.tests = []
        self.fields = []
        self.display_name = name

    def is_ancestor_of(self, t):
        if self is t:
            return True
        for st in self.subtypes:
            if st.is_ancestor_of(t):
                return True
        return False

types_by_name = {}
def get_type_by_name(name):
    if name not in types_by_name:
        t = MathType(name)
        types_by_name[name]=t
        for name2 in types[name]:
            t.subtypes.append(get_type_by_name(name2))
    return types_by_name[name]

get_type_by_name("MathObjects")
all_types = [get_type_by_name(name) for name in types.keys()]
for type in all_types:
    type.fields = ['Value']
get_type_by_name("Vectors").fields = ["R2", "R3"]
get_type_by_name("Matrices").fields = ["M2x2", "M3x3"]
get_type_by_name("Functions").fields = ["RealsToReals"]
get_type_by_name("Sets").display_name = 'Sets.Sets'
get_type_by_name("Functions").display_name = 'Sets.Functions'
get_type_by_name("Expressions").display_name = 'Sets.Expressions'

def gen_test(type1, type2, super_vs_sub, is_x_of):
    lines = []
    not_ = ''
    exclam = ''
    if not is_x_of:
        not_ = 'Not'
        exclam = '!'
    type2_title = type2.name
    if type1 is type2:
        type2_title = 'Self'
    super_sub = 'Super'
    if not super_vs_sub:
        super_sub = 'Sub'
    lines.append(
f'''        [Test]
        public void Test{type1.name}Is{not_}{super_sub}setOf{type2_title}()
        {{
            // expect
''')
    for f1 in type1.fields:
        for f2 in type2.fields:
            lines.append(
f'''            Assert.That({exclam}{type1.display_name}.{f1}.Is{super_sub}setOf({type2.display_name}.{f2}));
''')
    lines.append(
'''        }
''')
    return ''.join(lines)

def gen_self_test(type1, super_vs_sub):
    lines = []
    not_ = ''
    type2_title = type2.name
    if type1 is type2:
        type2_title = 'Self'
    super_sub = 'Super'
    if not super_vs_sub:
        super_sub = 'Sub'
    lines.append(
f'''        [Test]
        public void Test{type1.name}Is{super_sub}setOf{type2_title}()
        {{
            // expect
''')
    for f1 in type1.fields:
        for f2 in type2.fields:
            if f1 == f2:
                lines.append(
f'''            Assert.That({type1.display_name}.{f1}.Is{super_sub}setOf({type2.display_name}.{f2}));
''')
            else:
                lines.append(
f'''            Assert.That(!{type1.display_name}.{f1}.Is{super_sub}setOf({type2.display_name}.{f2}));
''')

    lines.append(
'''        }
''')
    return ''.join(lines)

for type1 in all_types:
    for type2 in all_types:
        type2_title = type2.display_name
        if type2 is type1:
            # is superset of self
            type1.tests.append(gen_self_test(type1, True))
            type1.tests.append(gen_self_test(type1, False))
        elif type1.is_ancestor_of(type2):
            # type1 is superset of type2
            type1.tests.append(gen_test(type1, type2, True, True))
            # type1 is not subset of type2
            type1.tests.append(gen_test(type1, type2, False, False))
        elif type2.is_ancestor_of(type1):
            # type1 is not superset of type2
            type1.tests.append(gen_test(type1, type2, True, False))
            # type1 is subset of type2
            type1.tests.append(gen_test(type1, type2, False, True))
        else:
            # type1 is not superset of type2
            type1.tests.append(gen_test(type1, type2, True, False))
            # type1 is not subset of type2
            type1.tests.append(gen_test(type1, type2, False, False))
    with open(f'{type1.name}T/SupersetAndSubsetTest.cs', 'w') as f:
        f.write(f'''
/*
 *  MetaphysicsIndustries.Solus
 *  Copyright (C) 2006-2022 Metaphysics Industries, Inc., Richard Sartor
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

using MetaphysicsIndustries.Solus.Sets;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.SetsT.{type1.name}T
{{
    [TestFixture]
    public class SupersetAndSubsetTest
    {{
''')
        first = True
        for test in type1.tests:
            if not first:
                f.write('\n')
            first = False
            f.write(test)
        f.write('''    }
}
''')
