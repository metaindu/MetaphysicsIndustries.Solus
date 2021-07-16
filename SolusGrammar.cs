
/*
 *  MetaphysicsIndustries.Solus
 *  Copyright (C) 2006-2021 Metaphysics Industries, Inc., Richard Sartor
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

using System;
using MetaphysicsIndustries.Giza;

namespace MetaphysicsIndustries.Solus
{
    public class SolusGrammar : NGrammar
    {
        public static readonly SolusGrammar Instance = new SolusGrammar();

        public NDefinition def__0024_implicit_0020_char_0020_class_0020__002B__002D_ = new NDefinition("$implicit char class +-");
        public NDefinition def__0024_implicit_0020_literal_0020__0028_ = new NDefinition("$implicit literal (");
        public NDefinition def__0024_implicit_0020_literal_0020__0029_ = new NDefinition("$implicit literal )");
        public NDefinition def__0024_implicit_0020_literal_0020__002C_ = new NDefinition("$implicit literal ,");
        public NDefinition def_binop = new NDefinition("binop");
        public NDefinition def_expr = new NDefinition("expr");
        public NDefinition def_float_002D_number = new NDefinition("float-number");
        public NDefinition def_function_002D_call = new NDefinition("function-call");
        public NDefinition def_identifier = new NDefinition("identifier");
        public NDefinition def_number = new NDefinition("number");
        public NDefinition def_paren = new NDefinition("paren");
        public NDefinition def_string = new NDefinition("string");
        public NDefinition def_subexpr = new NDefinition("subexpr");
        public NDefinition def_unary_002D_op = new NDefinition("unary-op");
        public NDefinition def_unicodechar = new NDefinition("unicodechar");
        public NDefinition def_varref = new NDefinition("varref");

        public CharNode node__0024_implicit_0020_char_0020_class_0020__002B__002D__0_;
        public CharNode node__0024_implicit_0020_literal_0020__0028__0_;
        public CharNode node__0024_implicit_0020_literal_0020__0029__0_;
        public CharNode node__0024_implicit_0020_literal_0020__002C__0_;
        public CharNode node_binop_0__0025__0026__002A__002B__002D__002F__005E__007C_;
        public DefRefNode node_expr_0_subexpr;
        public DefRefNode node_expr_1_binop;
        public DefRefNode node_expr_2_subexpr;
        public CharNode node_float_002D_number_0__002B__002D_;
        public CharNode node_float_002D_number_1__005C_d;
        public CharNode node_float_002D_number_2__002E_;
        public CharNode node_float_002D_number_3__005C_d;
        public CharNode node_float_002D_number_4_Ee;
        public CharNode node_float_002D_number_5__002B__002D_;
        public CharNode node_float_002D_number_6__005C_d;
        public DefRefNode node_function_002D_call_0_name;
        public DefRefNode node_function_002D_call_1__0028_;
        public DefRefNode node_function_002D_call_2_arg;
        public DefRefNode node_function_002D_call_3__002C_;
        public DefRefNode node_function_002D_call_4_arg;
        public DefRefNode node_function_002D_call_5__0029_;
        public CharNode node_identifier_0__005C_l;
        public CharNode node_identifier_1__005C_l_005C_d_005F_;
        public CharNode node_number_0_0b;
        public CharNode node_number_1_0b;
        public CharNode node_number_2_01;
        public CharNode node_number_3_0o;
        public CharNode node_number_4_0o;
        public CharNode node_number_5_01234567;
        public CharNode node_number_6_0x;
        public CharNode node_number_7_0x;
        public CharNode node_number_8__005C_dabcdef;
        public DefRefNode node_number_9_float_002D_number;
        public DefRefNode node_paren_0__0028_;
        public DefRefNode node_paren_1_expr;
        public DefRefNode node_paren_2__0029_;
        public CharNode node_string_0__0027_;
        public CharNode node_string_1__005E__0027__005C__005C_;
        public CharNode node_string_2__005C_;
        public CharNode node_string_3__0027__005C__005C_nrt;
        public DefRefNode node_string_4_unicodechar;
        public CharNode node_string_5__0027_;
        public CharNode node_string_6__0022_;
        public CharNode node_string_7__005E__0022__005C__005C_;
        public CharNode node_string_8__005C_;
        public CharNode node_string_9__0022__005C__005C_nrt;
        public DefRefNode node_string_10_unicodechar;
        public CharNode node_string_11__0022_;
        public DefRefNode node_subexpr_0_paren;
        public DefRefNode node_subexpr_1_function_002D_call;
        public DefRefNode node_subexpr_2_number;
        public DefRefNode node_subexpr_3_string;
        public DefRefNode node_subexpr_4_unary_002D_op;
        public DefRefNode node_subexpr_5_varref;
        public DefRefNode node_unary_002D_op_0__002B__002D_;
        public DefRefNode node_unary_002D_op_1_paren;
        public DefRefNode node_unary_002D_op_2_function_002D_call;
        public DefRefNode node_unary_002D_op_3_string;
        public DefRefNode node_unary_002D_op_4_unary_002D_op;
        public DefRefNode node_unary_002D_op_5_varref;
        public CharNode node_unicodechar_0__005C_x;
        public CharNode node_unicodechar_1__005C_x;
        public CharNode node_unicodechar_2__005C_dabcdef;
        public CharNode node_unicodechar_3__005C_dabcdef;
        public CharNode node_unicodechar_4__005C_dabcdef;
        public CharNode node_unicodechar_5__005C_dabcdef;
        public DefRefNode node_varref_0_identifier;

        public SolusGrammar()
        {
            Definitions.Add(def__0024_implicit_0020_char_0020_class_0020__002B__002D_);
            Definitions.Add(def__0024_implicit_0020_literal_0020__0028_);
            Definitions.Add(def__0024_implicit_0020_literal_0020__0029_);
            Definitions.Add(def__0024_implicit_0020_literal_0020__002C_);
            Definitions.Add(def_binop);
            Definitions.Add(def_expr);
            Definitions.Add(def_float_002D_number);
            Definitions.Add(def_function_002D_call);
            Definitions.Add(def_identifier);
            Definitions.Add(def_number);
            Definitions.Add(def_paren);
            Definitions.Add(def_string);
            Definitions.Add(def_subexpr);
            Definitions.Add(def_unary_002D_op);
            Definitions.Add(def_unicodechar);
            Definitions.Add(def_varref);

            def__0024_implicit_0020_char_0020_class_0020__002B__002D_.Directives.Add(DefinitionDirective.Token);
            def__0024_implicit_0020_char_0020_class_0020__002B__002D_.Directives.Add(DefinitionDirective.Atomic);
            def__0024_implicit_0020_char_0020_class_0020__002B__002D_.Directives.Add(DefinitionDirective.MindWhitespace);
            node__0024_implicit_0020_char_0020_class_0020__002B__002D__0_ = new CharNode(CharClass.FromUndelimitedCharClassText("+-"), "");
            def__0024_implicit_0020_char_0020_class_0020__002B__002D_.Nodes.Add(node__0024_implicit_0020_char_0020_class_0020__002B__002D__0_);
            def__0024_implicit_0020_char_0020_class_0020__002B__002D_.StartNodes.Add(node__0024_implicit_0020_char_0020_class_0020__002B__002D__0_);
            def__0024_implicit_0020_char_0020_class_0020__002B__002D_.EndNodes.Add(node__0024_implicit_0020_char_0020_class_0020__002B__002D__0_);

            def__0024_implicit_0020_literal_0020__0028_.Directives.Add(DefinitionDirective.Token);
            def__0024_implicit_0020_literal_0020__0028_.Directives.Add(DefinitionDirective.Atomic);
            def__0024_implicit_0020_literal_0020__0028_.Directives.Add(DefinitionDirective.MindWhitespace);
            node__0024_implicit_0020_literal_0020__0028__0_ = new CharNode(CharClass.FromUndelimitedCharClassText("("), "");
            def__0024_implicit_0020_literal_0020__0028_.Nodes.Add(node__0024_implicit_0020_literal_0020__0028__0_);
            def__0024_implicit_0020_literal_0020__0028_.StartNodes.Add(node__0024_implicit_0020_literal_0020__0028__0_);
            def__0024_implicit_0020_literal_0020__0028_.EndNodes.Add(node__0024_implicit_0020_literal_0020__0028__0_);

            def__0024_implicit_0020_literal_0020__0029_.Directives.Add(DefinitionDirective.Token);
            def__0024_implicit_0020_literal_0020__0029_.Directives.Add(DefinitionDirective.Atomic);
            def__0024_implicit_0020_literal_0020__0029_.Directives.Add(DefinitionDirective.MindWhitespace);
            node__0024_implicit_0020_literal_0020__0029__0_ = new CharNode(CharClass.FromUndelimitedCharClassText(")"), "");
            def__0024_implicit_0020_literal_0020__0029_.Nodes.Add(node__0024_implicit_0020_literal_0020__0029__0_);
            def__0024_implicit_0020_literal_0020__0029_.StartNodes.Add(node__0024_implicit_0020_literal_0020__0029__0_);
            def__0024_implicit_0020_literal_0020__0029_.EndNodes.Add(node__0024_implicit_0020_literal_0020__0029__0_);

            def__0024_implicit_0020_literal_0020__002C_.Directives.Add(DefinitionDirective.Token);
            def__0024_implicit_0020_literal_0020__002C_.Directives.Add(DefinitionDirective.Atomic);
            def__0024_implicit_0020_literal_0020__002C_.Directives.Add(DefinitionDirective.MindWhitespace);
            node__0024_implicit_0020_literal_0020__002C__0_ = new CharNode(CharClass.FromUndelimitedCharClassText(","), "");
            def__0024_implicit_0020_literal_0020__002C_.Nodes.Add(node__0024_implicit_0020_literal_0020__002C__0_);
            def__0024_implicit_0020_literal_0020__002C_.StartNodes.Add(node__0024_implicit_0020_literal_0020__002C__0_);
            def__0024_implicit_0020_literal_0020__002C_.EndNodes.Add(node__0024_implicit_0020_literal_0020__002C__0_);

            def_binop.Directives.Add(DefinitionDirective.Token);
            def_binop.Directives.Add(DefinitionDirective.Atomic);
            def_binop.Directives.Add(DefinitionDirective.MindWhitespace);
            node_binop_0__0025__0026__002A__002B__002D__002F__005E__007C_ = new CharNode(CharClass.FromUndelimitedCharClassText("%&*+-/^|"), "%&*+-/^|");
            def_binop.Nodes.Add(node_binop_0__0025__0026__002A__002B__002D__002F__005E__007C_);
            def_binop.StartNodes.Add(node_binop_0__0025__0026__002A__002B__002D__002F__005E__007C_);
            def_binop.EndNodes.Add(node_binop_0__0025__0026__002A__002B__002D__002F__005E__007C_);

            node_expr_0_subexpr = new DefRefNode(def_subexpr, "subexpr");
            node_expr_1_binop = new DefRefNode(def_binop, "binop");
            node_expr_2_subexpr = new DefRefNode(def_subexpr, "subexpr");
            def_expr.Nodes.Add(node_expr_0_subexpr);
            def_expr.Nodes.Add(node_expr_1_binop);
            def_expr.Nodes.Add(node_expr_2_subexpr);
            def_expr.StartNodes.Add(node_expr_0_subexpr);
            def_expr.EndNodes.Add(node_expr_2_subexpr);
            def_expr.EndNodes.Add(node_expr_0_subexpr);
            node_expr_0_subexpr.NextNodes.Add(node_expr_1_binop);
            node_expr_1_binop.NextNodes.Add(node_expr_2_subexpr);
            node_expr_2_subexpr.NextNodes.Add(node_expr_1_binop);

            def_float_002D_number.Directives.Add(DefinitionDirective.IgnoreCase);
            def_float_002D_number.Directives.Add(DefinitionDirective.Subtoken);
            def_float_002D_number.Directives.Add(DefinitionDirective.MindWhitespace);
            node_float_002D_number_0__002B__002D_ = new CharNode(CharClass.FromUndelimitedCharClassText("+-"), "+-");
            node_float_002D_number_1__005C_d = new CharNode(CharClass.FromUndelimitedCharClassText("\\d"), "\\d");
            node_float_002D_number_2__002E_ = new CharNode(CharClass.FromUndelimitedCharClassText("."), ".");
            node_float_002D_number_3__005C_d = new CharNode(CharClass.FromUndelimitedCharClassText("\\d"), "\\d");
            node_float_002D_number_4_Ee = new CharNode(CharClass.FromUndelimitedCharClassText("Ee"), "Ee");
            node_float_002D_number_5__002B__002D_ = new CharNode(CharClass.FromUndelimitedCharClassText("+-"), "+-");
            node_float_002D_number_6__005C_d = new CharNode(CharClass.FromUndelimitedCharClassText("\\d"), "\\d");
            def_float_002D_number.Nodes.Add(node_float_002D_number_0__002B__002D_);
            def_float_002D_number.Nodes.Add(node_float_002D_number_1__005C_d);
            def_float_002D_number.Nodes.Add(node_float_002D_number_2__002E_);
            def_float_002D_number.Nodes.Add(node_float_002D_number_3__005C_d);
            def_float_002D_number.Nodes.Add(node_float_002D_number_4_Ee);
            def_float_002D_number.Nodes.Add(node_float_002D_number_5__002B__002D_);
            def_float_002D_number.Nodes.Add(node_float_002D_number_6__005C_d);
            def_float_002D_number.StartNodes.Add(node_float_002D_number_0__002B__002D_);
            def_float_002D_number.StartNodes.Add(node_float_002D_number_1__005C_d);
            def_float_002D_number.EndNodes.Add(node_float_002D_number_6__005C_d);
            def_float_002D_number.EndNodes.Add(node_float_002D_number_3__005C_d);
            def_float_002D_number.EndNodes.Add(node_float_002D_number_1__005C_d);
            node_float_002D_number_0__002B__002D_.NextNodes.Add(node_float_002D_number_1__005C_d);
            node_float_002D_number_1__005C_d.NextNodes.Add(node_float_002D_number_1__005C_d);
            node_float_002D_number_1__005C_d.NextNodes.Add(node_float_002D_number_2__002E_);
            node_float_002D_number_1__005C_d.NextNodes.Add(node_float_002D_number_4_Ee);
            node_float_002D_number_2__002E_.NextNodes.Add(node_float_002D_number_3__005C_d);
            node_float_002D_number_3__005C_d.NextNodes.Add(node_float_002D_number_3__005C_d);
            node_float_002D_number_3__005C_d.NextNodes.Add(node_float_002D_number_4_Ee);
            node_float_002D_number_4_Ee.NextNodes.Add(node_float_002D_number_5__002B__002D_);
            node_float_002D_number_4_Ee.NextNodes.Add(node_float_002D_number_6__005C_d);
            node_float_002D_number_5__002B__002D_.NextNodes.Add(node_float_002D_number_6__005C_d);
            node_float_002D_number_6__005C_d.NextNodes.Add(node_float_002D_number_6__005C_d);

            node_function_002D_call_0_name = new DefRefNode(def_identifier, "name");
            node_function_002D_call_1__0028_ = new DefRefNode(def__0024_implicit_0020_literal_0020__0028_, "(");
            node_function_002D_call_2_arg = new DefRefNode(def_expr, "arg");
            node_function_002D_call_3__002C_ = new DefRefNode(def__0024_implicit_0020_literal_0020__002C_, ",");
            node_function_002D_call_4_arg = new DefRefNode(def_expr, "arg");
            node_function_002D_call_5__0029_ = new DefRefNode(def__0024_implicit_0020_literal_0020__0029_, ")");
            def_function_002D_call.Nodes.Add(node_function_002D_call_0_name);
            def_function_002D_call.Nodes.Add(node_function_002D_call_1__0028_);
            def_function_002D_call.Nodes.Add(node_function_002D_call_2_arg);
            def_function_002D_call.Nodes.Add(node_function_002D_call_3__002C_);
            def_function_002D_call.Nodes.Add(node_function_002D_call_4_arg);
            def_function_002D_call.Nodes.Add(node_function_002D_call_5__0029_);
            def_function_002D_call.StartNodes.Add(node_function_002D_call_0_name);
            def_function_002D_call.EndNodes.Add(node_function_002D_call_5__0029_);
            node_function_002D_call_0_name.NextNodes.Add(node_function_002D_call_1__0028_);
            node_function_002D_call_1__0028_.NextNodes.Add(node_function_002D_call_2_arg);
            node_function_002D_call_1__0028_.NextNodes.Add(node_function_002D_call_5__0029_);
            node_function_002D_call_2_arg.NextNodes.Add(node_function_002D_call_3__002C_);
            node_function_002D_call_2_arg.NextNodes.Add(node_function_002D_call_5__0029_);
            node_function_002D_call_3__002C_.NextNodes.Add(node_function_002D_call_4_arg);
            node_function_002D_call_4_arg.NextNodes.Add(node_function_002D_call_3__002C_);
            node_function_002D_call_4_arg.NextNodes.Add(node_function_002D_call_5__0029_);

            def_identifier.Directives.Add(DefinitionDirective.Token);
            def_identifier.Directives.Add(DefinitionDirective.Atomic);
            def_identifier.Directives.Add(DefinitionDirective.MindWhitespace);
            node_identifier_0__005C_l = new CharNode(CharClass.FromUndelimitedCharClassText("\\l"), "\\l");
            node_identifier_1__005C_l_005C_d_005F_ = new CharNode(CharClass.FromUndelimitedCharClassText("\\l\\d_"), "\\l\\d_");
            def_identifier.Nodes.Add(node_identifier_0__005C_l);
            def_identifier.Nodes.Add(node_identifier_1__005C_l_005C_d_005F_);
            def_identifier.StartNodes.Add(node_identifier_0__005C_l);
            def_identifier.EndNodes.Add(node_identifier_1__005C_l_005C_d_005F_);
            def_identifier.EndNodes.Add(node_identifier_0__005C_l);
            node_identifier_0__005C_l.NextNodes.Add(node_identifier_1__005C_l_005C_d_005F_);
            node_identifier_1__005C_l_005C_d_005F_.NextNodes.Add(node_identifier_1__005C_l_005C_d_005F_);

            def_number.Directives.Add(DefinitionDirective.IgnoreCase);
            def_number.Directives.Add(DefinitionDirective.Token);
            def_number.Directives.Add(DefinitionDirective.Atomic);
            def_number.Directives.Add(DefinitionDirective.MindWhitespace);
            node_number_0_0b = new CharNode(CharClass.FromUndelimitedCharClassText("0"), "0b");
            node_number_1_0b = new CharNode(CharClass.FromUndelimitedCharClassText("b"), "0b");
            node_number_2_01 = new CharNode(CharClass.FromUndelimitedCharClassText("01"), "01");
            node_number_3_0o = new CharNode(CharClass.FromUndelimitedCharClassText("0"), "0o");
            node_number_4_0o = new CharNode(CharClass.FromUndelimitedCharClassText("o"), "0o");
            node_number_5_01234567 = new CharNode(CharClass.FromUndelimitedCharClassText("01234567"), "01234567");
            node_number_6_0x = new CharNode(CharClass.FromUndelimitedCharClassText("0"), "0x");
            node_number_7_0x = new CharNode(CharClass.FromUndelimitedCharClassText("x"), "0x");
            node_number_8__005C_dabcdef = new CharNode(CharClass.FromUndelimitedCharClassText("\\dabcdef"), "\\dabcdef");
            node_number_9_float_002D_number = new DefRefNode(def_float_002D_number, "float-number");
            def_number.Nodes.Add(node_number_0_0b);
            def_number.Nodes.Add(node_number_1_0b);
            def_number.Nodes.Add(node_number_2_01);
            def_number.Nodes.Add(node_number_3_0o);
            def_number.Nodes.Add(node_number_4_0o);
            def_number.Nodes.Add(node_number_5_01234567);
            def_number.Nodes.Add(node_number_6_0x);
            def_number.Nodes.Add(node_number_7_0x);
            def_number.Nodes.Add(node_number_8__005C_dabcdef);
            def_number.Nodes.Add(node_number_9_float_002D_number);
            def_number.StartNodes.Add(node_number_0_0b);
            def_number.StartNodes.Add(node_number_3_0o);
            def_number.StartNodes.Add(node_number_6_0x);
            def_number.StartNodes.Add(node_number_9_float_002D_number);
            def_number.EndNodes.Add(node_number_2_01);
            def_number.EndNodes.Add(node_number_5_01234567);
            def_number.EndNodes.Add(node_number_8__005C_dabcdef);
            def_number.EndNodes.Add(node_number_9_float_002D_number);
            node_number_0_0b.NextNodes.Add(node_number_1_0b);
            node_number_1_0b.NextNodes.Add(node_number_2_01);
            node_number_2_01.NextNodes.Add(node_number_2_01);
            node_number_3_0o.NextNodes.Add(node_number_4_0o);
            node_number_4_0o.NextNodes.Add(node_number_5_01234567);
            node_number_5_01234567.NextNodes.Add(node_number_5_01234567);
            node_number_6_0x.NextNodes.Add(node_number_7_0x);
            node_number_7_0x.NextNodes.Add(node_number_8__005C_dabcdef);
            node_number_8__005C_dabcdef.NextNodes.Add(node_number_8__005C_dabcdef);

            node_paren_0__0028_ = new DefRefNode(def__0024_implicit_0020_literal_0020__0028_, "(");
            node_paren_1_expr = new DefRefNode(def_expr, "expr");
            node_paren_2__0029_ = new DefRefNode(def__0024_implicit_0020_literal_0020__0029_, ")");
            def_paren.Nodes.Add(node_paren_0__0028_);
            def_paren.Nodes.Add(node_paren_1_expr);
            def_paren.Nodes.Add(node_paren_2__0029_);
            def_paren.StartNodes.Add(node_paren_0__0028_);
            def_paren.EndNodes.Add(node_paren_2__0029_);
            node_paren_0__0028_.NextNodes.Add(node_paren_1_expr);
            node_paren_1_expr.NextNodes.Add(node_paren_2__0029_);

            def_string.Directives.Add(DefinitionDirective.Token);
            def_string.Directives.Add(DefinitionDirective.Atomic);
            def_string.Directives.Add(DefinitionDirective.MindWhitespace);
            node_string_0__0027_ = new CharNode(CharClass.FromUndelimitedCharClassText("'"), "'");
            node_string_1__005E__0027__005C__005C_ = new CharNode(CharClass.FromUndelimitedCharClassText("^'\\\\"), "^'\\\\");
            node_string_2__005C_ = new CharNode(CharClass.FromUndelimitedCharClassText("\\\\"), "\\");
            node_string_3__0027__005C__005C_nrt = new CharNode(CharClass.FromUndelimitedCharClassText("'\\\\nrt"), "'\\\\nrt");
            node_string_4_unicodechar = new DefRefNode(def_unicodechar, "unicodechar");
            node_string_5__0027_ = new CharNode(CharClass.FromUndelimitedCharClassText("'"), "'");
            node_string_6__0022_ = new CharNode(CharClass.FromUndelimitedCharClassText("\""), "\"");
            node_string_7__005E__0022__005C__005C_ = new CharNode(CharClass.FromUndelimitedCharClassText("^\"\\\\"), "^\"\\\\");
            node_string_8__005C_ = new CharNode(CharClass.FromUndelimitedCharClassText("\\\\"), "\\");
            node_string_9__0022__005C__005C_nrt = new CharNode(CharClass.FromUndelimitedCharClassText("\"\\\\nrt"), "\"\\\\nrt");
            node_string_10_unicodechar = new DefRefNode(def_unicodechar, "unicodechar");
            node_string_11__0022_ = new CharNode(CharClass.FromUndelimitedCharClassText("\""), "\"");
            def_string.Nodes.Add(node_string_0__0027_);
            def_string.Nodes.Add(node_string_1__005E__0027__005C__005C_);
            def_string.Nodes.Add(node_string_2__005C_);
            def_string.Nodes.Add(node_string_3__0027__005C__005C_nrt);
            def_string.Nodes.Add(node_string_4_unicodechar);
            def_string.Nodes.Add(node_string_5__0027_);
            def_string.Nodes.Add(node_string_6__0022_);
            def_string.Nodes.Add(node_string_7__005E__0022__005C__005C_);
            def_string.Nodes.Add(node_string_8__005C_);
            def_string.Nodes.Add(node_string_9__0022__005C__005C_nrt);
            def_string.Nodes.Add(node_string_10_unicodechar);
            def_string.Nodes.Add(node_string_11__0022_);
            def_string.StartNodes.Add(node_string_0__0027_);
            def_string.StartNodes.Add(node_string_6__0022_);
            def_string.EndNodes.Add(node_string_5__0027_);
            def_string.EndNodes.Add(node_string_11__0022_);
            node_string_0__0027_.NextNodes.Add(node_string_1__005E__0027__005C__005C_);
            node_string_0__0027_.NextNodes.Add(node_string_2__005C_);
            node_string_0__0027_.NextNodes.Add(node_string_4_unicodechar);
            node_string_1__005E__0027__005C__005C_.NextNodes.Add(node_string_1__005E__0027__005C__005C_);
            node_string_1__005E__0027__005C__005C_.NextNodes.Add(node_string_2__005C_);
            node_string_1__005E__0027__005C__005C_.NextNodes.Add(node_string_4_unicodechar);
            node_string_1__005E__0027__005C__005C_.NextNodes.Add(node_string_5__0027_);
            node_string_2__005C_.NextNodes.Add(node_string_3__0027__005C__005C_nrt);
            node_string_3__0027__005C__005C_nrt.NextNodes.Add(node_string_1__005E__0027__005C__005C_);
            node_string_3__0027__005C__005C_nrt.NextNodes.Add(node_string_2__005C_);
            node_string_3__0027__005C__005C_nrt.NextNodes.Add(node_string_4_unicodechar);
            node_string_3__0027__005C__005C_nrt.NextNodes.Add(node_string_5__0027_);
            node_string_4_unicodechar.NextNodes.Add(node_string_1__005E__0027__005C__005C_);
            node_string_4_unicodechar.NextNodes.Add(node_string_2__005C_);
            node_string_4_unicodechar.NextNodes.Add(node_string_4_unicodechar);
            node_string_4_unicodechar.NextNodes.Add(node_string_5__0027_);
            node_string_6__0022_.NextNodes.Add(node_string_7__005E__0022__005C__005C_);
            node_string_6__0022_.NextNodes.Add(node_string_8__005C_);
            node_string_6__0022_.NextNodes.Add(node_string_10_unicodechar);
            node_string_7__005E__0022__005C__005C_.NextNodes.Add(node_string_7__005E__0022__005C__005C_);
            node_string_7__005E__0022__005C__005C_.NextNodes.Add(node_string_8__005C_);
            node_string_7__005E__0022__005C__005C_.NextNodes.Add(node_string_10_unicodechar);
            node_string_7__005E__0022__005C__005C_.NextNodes.Add(node_string_11__0022_);
            node_string_8__005C_.NextNodes.Add(node_string_9__0022__005C__005C_nrt);
            node_string_9__0022__005C__005C_nrt.NextNodes.Add(node_string_7__005E__0022__005C__005C_);
            node_string_9__0022__005C__005C_nrt.NextNodes.Add(node_string_8__005C_);
            node_string_9__0022__005C__005C_nrt.NextNodes.Add(node_string_10_unicodechar);
            node_string_9__0022__005C__005C_nrt.NextNodes.Add(node_string_11__0022_);
            node_string_10_unicodechar.NextNodes.Add(node_string_7__005E__0022__005C__005C_);
            node_string_10_unicodechar.NextNodes.Add(node_string_8__005C_);
            node_string_10_unicodechar.NextNodes.Add(node_string_10_unicodechar);
            node_string_10_unicodechar.NextNodes.Add(node_string_11__0022_);

            node_subexpr_0_paren = new DefRefNode(def_paren, "paren");
            node_subexpr_1_function_002D_call = new DefRefNode(def_function_002D_call, "function-call");
            node_subexpr_2_number = new DefRefNode(def_number, "number");
            node_subexpr_3_string = new DefRefNode(def_string, "string");
            node_subexpr_4_unary_002D_op = new DefRefNode(def_unary_002D_op, "unary-op");
            node_subexpr_5_varref = new DefRefNode(def_varref, "varref");
            def_subexpr.Nodes.Add(node_subexpr_0_paren);
            def_subexpr.Nodes.Add(node_subexpr_1_function_002D_call);
            def_subexpr.Nodes.Add(node_subexpr_2_number);
            def_subexpr.Nodes.Add(node_subexpr_3_string);
            def_subexpr.Nodes.Add(node_subexpr_4_unary_002D_op);
            def_subexpr.Nodes.Add(node_subexpr_5_varref);
            def_subexpr.StartNodes.Add(node_subexpr_0_paren);
            def_subexpr.StartNodes.Add(node_subexpr_1_function_002D_call);
            def_subexpr.StartNodes.Add(node_subexpr_2_number);
            def_subexpr.StartNodes.Add(node_subexpr_3_string);
            def_subexpr.StartNodes.Add(node_subexpr_4_unary_002D_op);
            def_subexpr.StartNodes.Add(node_subexpr_5_varref);
            def_subexpr.EndNodes.Add(node_subexpr_0_paren);
            def_subexpr.EndNodes.Add(node_subexpr_1_function_002D_call);
            def_subexpr.EndNodes.Add(node_subexpr_2_number);
            def_subexpr.EndNodes.Add(node_subexpr_3_string);
            def_subexpr.EndNodes.Add(node_subexpr_4_unary_002D_op);
            def_subexpr.EndNodes.Add(node_subexpr_5_varref);

            node_unary_002D_op_0__002B__002D_ = new DefRefNode(def__0024_implicit_0020_char_0020_class_0020__002B__002D_, "+-");
            node_unary_002D_op_1_paren = new DefRefNode(def_paren, "paren");
            node_unary_002D_op_2_function_002D_call = new DefRefNode(def_function_002D_call, "function-call");
            node_unary_002D_op_3_string = new DefRefNode(def_string, "string");
            node_unary_002D_op_4_unary_002D_op = new DefRefNode(def_unary_002D_op, "unary-op");
            node_unary_002D_op_5_varref = new DefRefNode(def_varref, "varref");
            def_unary_002D_op.Nodes.Add(node_unary_002D_op_0__002B__002D_);
            def_unary_002D_op.Nodes.Add(node_unary_002D_op_1_paren);
            def_unary_002D_op.Nodes.Add(node_unary_002D_op_2_function_002D_call);
            def_unary_002D_op.Nodes.Add(node_unary_002D_op_3_string);
            def_unary_002D_op.Nodes.Add(node_unary_002D_op_4_unary_002D_op);
            def_unary_002D_op.Nodes.Add(node_unary_002D_op_5_varref);
            def_unary_002D_op.StartNodes.Add(node_unary_002D_op_0__002B__002D_);
            def_unary_002D_op.EndNodes.Add(node_unary_002D_op_1_paren);
            def_unary_002D_op.EndNodes.Add(node_unary_002D_op_2_function_002D_call);
            def_unary_002D_op.EndNodes.Add(node_unary_002D_op_3_string);
            def_unary_002D_op.EndNodes.Add(node_unary_002D_op_4_unary_002D_op);
            def_unary_002D_op.EndNodes.Add(node_unary_002D_op_5_varref);
            node_unary_002D_op_0__002B__002D_.NextNodes.Add(node_unary_002D_op_1_paren);
            node_unary_002D_op_0__002B__002D_.NextNodes.Add(node_unary_002D_op_2_function_002D_call);
            node_unary_002D_op_0__002B__002D_.NextNodes.Add(node_unary_002D_op_3_string);
            node_unary_002D_op_0__002B__002D_.NextNodes.Add(node_unary_002D_op_4_unary_002D_op);
            node_unary_002D_op_0__002B__002D_.NextNodes.Add(node_unary_002D_op_5_varref);

            def_unicodechar.Directives.Add(DefinitionDirective.IgnoreCase);
            def_unicodechar.Directives.Add(DefinitionDirective.Subtoken);
            def_unicodechar.Directives.Add(DefinitionDirective.MindWhitespace);
            node_unicodechar_0__005C_x = new CharNode(CharClass.FromUndelimitedCharClassText("\\\\"), "\\x");
            node_unicodechar_1__005C_x = new CharNode(CharClass.FromUndelimitedCharClassText("x"), "\\x");
            node_unicodechar_2__005C_dabcdef = new CharNode(CharClass.FromUndelimitedCharClassText("\\dabcdef"), "\\dabcdef");
            node_unicodechar_3__005C_dabcdef = new CharNode(CharClass.FromUndelimitedCharClassText("\\dabcdef"), "\\dabcdef");
            node_unicodechar_4__005C_dabcdef = new CharNode(CharClass.FromUndelimitedCharClassText("\\dabcdef"), "\\dabcdef");
            node_unicodechar_5__005C_dabcdef = new CharNode(CharClass.FromUndelimitedCharClassText("\\dabcdef"), "\\dabcdef");
            def_unicodechar.Nodes.Add(node_unicodechar_0__005C_x);
            def_unicodechar.Nodes.Add(node_unicodechar_1__005C_x);
            def_unicodechar.Nodes.Add(node_unicodechar_2__005C_dabcdef);
            def_unicodechar.Nodes.Add(node_unicodechar_3__005C_dabcdef);
            def_unicodechar.Nodes.Add(node_unicodechar_4__005C_dabcdef);
            def_unicodechar.Nodes.Add(node_unicodechar_5__005C_dabcdef);
            def_unicodechar.StartNodes.Add(node_unicodechar_0__005C_x);
            def_unicodechar.EndNodes.Add(node_unicodechar_5__005C_dabcdef);
            node_unicodechar_0__005C_x.NextNodes.Add(node_unicodechar_1__005C_x);
            node_unicodechar_1__005C_x.NextNodes.Add(node_unicodechar_2__005C_dabcdef);
            node_unicodechar_2__005C_dabcdef.NextNodes.Add(node_unicodechar_3__005C_dabcdef);
            node_unicodechar_3__005C_dabcdef.NextNodes.Add(node_unicodechar_4__005C_dabcdef);
            node_unicodechar_4__005C_dabcdef.NextNodes.Add(node_unicodechar_5__005C_dabcdef);

            node_varref_0_identifier = new DefRefNode(def_identifier, "identifier");
            def_varref.Nodes.Add(node_varref_0_identifier);
            def_varref.StartNodes.Add(node_varref_0_identifier);
            def_varref.EndNodes.Add(node_varref_0_identifier);

        }
    }
}

