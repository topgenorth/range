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
using Db4oTool.Core;
using Db4objects.Db4o.TA;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace Db4oTool.TA
{
	class BindMethodEmitter : MethodEmitter
	{
		public BindMethodEmitter(InstrumentationContext context, FieldReference field) : base(context, field)
		{
		}

		public MethodDefinition Emit()
		{
			MethodDefinition bind = NewExplicitMethod(typeof(IActivatable).GetMethod("Bind"));
			CilWorker cil = bind.Body.CilWorker;

			Instruction activatorSetting = cil.Create(OpCodes.Ldarg_0);

			// if (_activator == activator) {
			//   return;
			// }
			LoadActivatorField(cil);
			cil.Emit(OpCodes.Ldarg_1);

			Instruction isParameterNullInstruction = cil.Create(OpCodes.Ldarg_1);

			cil.Emit(OpCodes.Bne_Un, isParameterNullInstruction);
			cil.Emit(OpCodes.Ret);

			// if (activator != null && _activator != null) {
			//   throw new InvalidOperationException();
			// }
			cil.Append(isParameterNullInstruction);
			cil.Emit(OpCodes.Brfalse, activatorSetting);
			LoadActivatorField(cil);
			cil.Emit(OpCodes.Brfalse, activatorSetting);

			cil.Emit(OpCodes.Newobj, _context.Import(typeof(InvalidOperationException).GetConstructor(new Type[0])));
			cil.Emit(OpCodes.Throw);
			
			// _activator = activator;
			cil.Append(activatorSetting);
			cil.Emit(OpCodes.Ldarg_1);
			cil.Emit(OpCodes.Stfld, _activatorField);

			cil.Emit(OpCodes.Ret);

			return bind;
		}

		private void LoadActivatorField(CilWorker cil)
		{
			cil.Emit(OpCodes.Ldarg_0);
			cil.Emit(OpCodes.Ldfld, _activatorField);
		}
	}
}
