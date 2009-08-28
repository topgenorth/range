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
using System;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace Db4oTool.Core
{
	class MethodEditor
	{
		private readonly CilWorker _worker;
		private readonly MethodBody _body;

		public MethodEditor(MethodDefinition method)
		{	
			_body = method.Body;
			_worker = method.Body.CilWorker;
		}

		public Instruction Create(OpCode opcode, VariableDefinition variable)
		{
			return _worker.Create(opcode, variable);
		}

		public void InsertBefore(Instruction target, Instruction instruction)
		{
			_worker.InsertBefore(target, instruction);
			UpdateInstructionReferences(target, instruction);
		}

		private void UpdateInstructionReferences(Instruction oldTarget, Instruction newTarget)
		{
			UpdateInstructionReferences(_body.Instructions, oldTarget, newTarget);
			UpdateInstructionReferences(_body.ExceptionHandlers, oldTarget, newTarget);
		}

		private static void UpdateInstructionReferences(InstructionCollection collection, Instruction oldTarget, Instruction newTarget)
		{
			foreach (Instruction instr in collection)
			{
				if (instr.OpCode == OpCodes.Switch)
				{
					Instruction[] labels = (Instruction[])instr.Operand;
					ReplaceAll(labels, oldTarget, newTarget);
				}
				else if (instr.Operand == oldTarget)
				{
					instr.Operand = newTarget;
				}
			}
		}

		private static void UpdateInstructionReferences(ExceptionHandlerCollection handlers, Instruction oldTarget, Instruction newTarget)
		{
			foreach (ExceptionHandler handler in handlers)
			{
				if (handler.TryEnd == oldTarget)
				{
					handler.TryEnd = newTarget;
				}
				if (handler.TryStart == oldTarget)
				{
					handler.TryStart = newTarget;
				}
				if (handler.HandlerStart == oldTarget)
				{
					handler.HandlerStart = newTarget;
				}
				if (handler.FilterStart == oldTarget)
				{
					handler.FilterStart = newTarget;
				}
			}
		}

		private static void ReplaceAll(Instruction[] labels, Instruction oldTarget, Instruction newTarget)
		{
			for (int i = 0; i < labels.Length; ++i)
			{
				if (labels[i] == oldTarget)
				{
					labels[i] = newTarget;
				}
			}
		}

		internal Instruction Create(OpCode opCode, ParameterDefinition parameterDefinition)
		{
			return _worker.Create(opCode, parameterDefinition);
		}

		public Instruction Create(OpCode opCode)
		{
			return _worker.Create(opCode);
		}

		internal Instruction Create(OpCode opCode, int value)
		{
			return _worker.Create(opCode, value);
		}

		public Instruction Create(OpCode opCode, MethodReference reference)
		{
			return _worker.Create(opCode, reference);
		}

		public VariableDefinition AddVariable(TypeReference type)
		{
			_body.InitLocals = true;

			VariableDefinition variable = new VariableDefinition(type);
			_body.Variables.Add(variable);

			return variable;
		}
	}
}
