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
using Db4objects.Db4o.Foundation;
using Sharpen.Lang;

namespace Db4objects.Db4o.Foundation
{
	/// <summary>A dynamic variable is a value associated to a specific thread and scope.
	/// 	</summary>
	/// <remarks>
	/// A dynamic variable is a value associated to a specific thread and scope.
	/// The value is brought into scope with the
	/// <see cref="Db4objects.Db4o.Foundation.DynamicVariable.With">Db4objects.Db4o.Foundation.DynamicVariable.With
	/// 	</see>
	/// method.
	/// </remarks>
	public class DynamicVariable
	{
		private class ThreadSlot
		{
			public readonly Thread thread;

			public readonly object value;

			public DynamicVariable.ThreadSlot next;

			public ThreadSlot(object value_, DynamicVariable.ThreadSlot next_)
			{
				thread = Thread.CurrentThread();
				value = value_;
				next = next_;
			}
		}

		private readonly Type _expectedType;

		private DynamicVariable.ThreadSlot _values = null;

		public DynamicVariable() : this(null)
		{
		}

		public DynamicVariable(Type expectedType)
		{
			_expectedType = expectedType;
		}

		public virtual object Value
		{
			get
			{
				Thread current = Thread.CurrentThread();
				lock (this)
				{
					DynamicVariable.ThreadSlot slot = _values;
					while (null != slot)
					{
						if (slot.thread == current)
						{
							return slot.value;
						}
						slot = slot.next;
					}
				}
				return DefaultValue();
			}
		}

		protected virtual object DefaultValue()
		{
			return null;
		}

		public virtual object With(object value, IClosure4 block)
		{
			Validate(value);
			DynamicVariable.ThreadSlot slot = PushValue(value);
			try
			{
				return block.Run();
			}
			finally
			{
				PopValue(slot);
			}
		}

		public virtual void With(object value, IRunnable block)
		{
			With(value, new _IClosure4_69(block));
		}

		private sealed class _IClosure4_69 : IClosure4
		{
			public _IClosure4_69(IRunnable block)
			{
				this.block = block;
			}

			public object Run()
			{
				block.Run();
				return null;
			}

			private readonly IRunnable block;
		}

		private void Validate(object value)
		{
			if (value == null || _expectedType == null)
			{
				return;
			}
			if (_expectedType.IsInstanceOfType(value))
			{
				return;
			}
			throw new ArgumentException("Expecting instance of '" + _expectedType + "' but got '"
				 + value + "'");
		}

		private void PopValue(DynamicVariable.ThreadSlot slot)
		{
			lock (this)
			{
				if (slot == _values)
				{
					_values = _values.next;
					return;
				}
				DynamicVariable.ThreadSlot previous = _values;
				DynamicVariable.ThreadSlot current = _values.next;
				while (current != null)
				{
					if (current == slot)
					{
						previous.next = current.next;
						return;
					}
					previous = current;
					current = current.next;
				}
			}
		}

		private DynamicVariable.ThreadSlot PushValue(object value)
		{
			lock (this)
			{
				DynamicVariable.ThreadSlot slot = new DynamicVariable.ThreadSlot(value, _values);
				_values = slot;
				return slot;
			}
		}
	}
}
