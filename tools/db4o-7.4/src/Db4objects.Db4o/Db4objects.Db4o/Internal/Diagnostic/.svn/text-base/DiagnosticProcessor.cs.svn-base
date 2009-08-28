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
using System.Collections;
using Db4objects.Db4o.Diagnostic;
using Db4objects.Db4o.Foundation;
using Db4objects.Db4o.Internal;
using Db4objects.Db4o.Query;

namespace Db4objects.Db4o.Internal.Diagnostic
{
	/// <exclude>FIXME: remove me from the core and make me a facade over Events</exclude>
	public class DiagnosticProcessor : IDiagnosticConfiguration, IDeepClone
	{
		private Collection4 _listeners;

		public DiagnosticProcessor()
		{
		}

		private DiagnosticProcessor(Collection4 listeners)
		{
			_listeners = listeners;
		}

		public virtual void AddListener(IDiagnosticListener listener)
		{
			if (_listeners == null)
			{
				_listeners = new Collection4();
			}
			_listeners.Add(listener);
		}

		public virtual void CheckClassHasFields(ClassMetadata classMetadata)
		{
			if ((!classMetadata.AspectsAreNull()) && classMetadata.DeclaredAspectCount() == 0
				)
			{
				string name = classMetadata.GetName();
				string[] ignoredPackages = new string[] { "java.util." };
				for (int i = 0; i < ignoredPackages.Length; i++)
				{
					if (name.IndexOf(ignoredPackages[i]) == 0)
					{
						return;
					}
				}
				if (IsDb4oClass(classMetadata))
				{
					return;
				}
				OnDiagnostic(new ClassHasNoFields(name));
			}
		}

		public virtual void CheckUpdateDepth(int depth)
		{
			if (depth > 1)
			{
				OnDiagnostic(new UpdateDepthGreaterOne(depth));
			}
		}

		public virtual object DeepClone(object context)
		{
			return new Db4objects.Db4o.Internal.Diagnostic.DiagnosticProcessor(CloneListeners
				());
		}

		public virtual void DeletionFailed()
		{
			OnDiagnostic(new Db4objects.Db4o.Diagnostic.DeletionFailed());
		}

		public virtual void DefragmentRecommended(DefragmentRecommendation.DefragmentRecommendationReason
			 reason)
		{
			OnDiagnostic(new DefragmentRecommendation(reason));
		}

		private Collection4 CloneListeners()
		{
			return _listeners != null ? new Collection4(_listeners) : null;
		}

		public virtual bool Enabled()
		{
			return _listeners != null;
		}

		private bool IsDb4oClass(ClassMetadata yc)
		{
			return Platform4.IsDb4oClass(yc.GetName());
		}

		public virtual void LoadedFromClassIndex(ClassMetadata yc)
		{
			if (IsDb4oClass(yc))
			{
				return;
			}
			OnDiagnostic(new Db4objects.Db4o.Diagnostic.LoadedFromClassIndex(yc.GetName()));
		}

		public virtual void DescendIntoTranslator(ClassMetadata parent, string fieldName)
		{
			OnDiagnostic(new Db4objects.Db4o.Diagnostic.DescendIntoTranslator(parent.GetName(
				), fieldName));
		}

		public virtual void NativeQueryUnoptimized(Predicate predicate, Exception exception
			)
		{
			OnDiagnostic(new NativeQueryNotOptimized(predicate, exception));
		}

		public virtual void NativeQueryOptimizerNotLoaded(int reason, Exception e)
		{
			OnDiagnostic(new Db4objects.Db4o.Diagnostic.NativeQueryOptimizerNotLoaded(reason, 
				e));
		}

		public virtual void OnDiagnostic(IDiagnostic d)
		{
			if (_listeners == null)
			{
				return;
			}
			IEnumerator i = _listeners.GetEnumerator();
			while (i.MoveNext())
			{
				((IDiagnosticListener)i.Current).OnDiagnostic(d);
			}
		}

		public virtual void RemoveAllListeners()
		{
			_listeners = null;
		}
	}
}
