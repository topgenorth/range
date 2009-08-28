/* Copyright (C) 2004 - 2008  db4objects Inc.  http://www.db4o.com

This file is part of the db4o open source object database.

db4o is free software; you can redistribute it and/or modify it under
the terms of version 2 of the GNU General Public License as published
by the Free Software Foundation and as clarified by db4objects' GPL 
interpretation policy, available at
http://www.db4o.com/about/company/legalpolicies/gplinterpretation/
Alternatively you can write to db4objects, Inc., 1900 S Norfolk Street,
Suite 350, San Mateo, CA 94403, USA.

db4o is distributed in the hope that it will be useful, but WITHOUT ANY
WARRANTY; without even the implied warranty of MERCHANTABILITY or
FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License
for more details.

You should have received a copy of the GNU General Public License along
with this program; if not, write to the Free Software Foundation, Inc.,
59 Temple Place - Suite 330, Boston, MA  02111-1307, USA. */
using Db4oTool.Core;
using Db4oUnit;
using Mono.Cecil.Cil;
using Mono.Cecil;

namespace Db4oTool.Tests.Core
{
	public class ILPatternTestCase : ITestCase
	{
		public void TestSequenceBackwardsMatch()
		{
			ILPattern sequence = ILPattern.Sequence(OpCodes.Stsfld, OpCodes.Ldsfld);

			MethodDefinition method = CreateTestMethod(TestSequence1);
			Instruction lastInstruction = LastInstruction(method);
			ILPattern.MatchContext context = sequence.BackwardsMatch(lastInstruction);
			Assert.IsTrue(context.Success);
			Assert.AreSame(method.Body.Instructions[0], context.Instruction);
		}

		public void TestSequenceIsBackwardsMatch()
		{
			ILPattern sequence = ILPattern.Sequence(OpCodes.Stsfld, OpCodes.Ldsfld);

			Instruction lastInstruction = CreateTestMethodAndReturnLastInstruction(TestSequence1);
			Assert.IsTrue(sequence.IsBackwardsMatch(lastInstruction));

			sequence = ILPattern.Sequence(OpCodes.Ldsfld, OpCodes.Stsfld);
			Assert.IsTrue(!sequence.IsBackwardsMatch(lastInstruction));
		}
		
		public void TestComplexSequenceIsBackwardsMatch()
		{
			ILPattern sequence = ILPattern.Sequence(
				ILPattern.Optional(OpCodes.Ret),
				ILPattern.Instruction(OpCodes.Stsfld),
				ILPattern.Alternation(OpCodes.Ldfld, OpCodes.Ldsfld));

			Instruction lastInstruction = CreateTestMethodAndReturnLastInstruction(TestSequence1);
			Assert.IsTrue(sequence.IsBackwardsMatch(lastInstruction));

			lastInstruction = CreateTestMethodAndReturnLastInstruction(TestSequence2);
			Assert.IsTrue(sequence.IsBackwardsMatch(lastInstruction));
		}

		delegate void CilWorkerAction(CilWorker worker);

		private static Instruction CreateTestMethodAndReturnLastInstruction(CilWorkerAction action)
		{
			return LastInstruction(CreateTestMethod(action));
		}

		private static Instruction LastInstruction(MethodDefinition method)
		{
			return method.Body.Instructions[method.Body.Instructions.Count - 1];
		}

		static MethodDefinition CreateTestMethod(CilWorkerAction action)
		{
			MethodDefinition test = new MethodDefinition("Test", MethodAttributes.Public, null);
			action(test.Body.CilWorker);
			return test;
		}

		private static void TestSequence1(CilWorker worker)
		{
			FieldDefinition blank = new FieldDefinition("Test", null, FieldAttributes.Public);
			worker.Emit(OpCodes.Nop);
			worker.Emit(OpCodes.Ldsfld, blank);
			worker.Emit(OpCodes.Stsfld, blank);
			worker.Emit(OpCodes.Ret);
		}

		private static void TestSequence2(CilWorker worker)
		{
			FieldDefinition blank = new FieldDefinition("Test", null, FieldAttributes.Public);
			worker.Emit(OpCodes.Nop);
			worker.Emit(OpCodes.Ldfld, blank);
			worker.Emit(OpCodes.Stsfld, blank);
			worker.Emit(OpCodes.Ret);
			worker.Emit(OpCodes.Nop);
		}
	}
}
