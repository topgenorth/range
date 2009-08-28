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
using Db4objects.Db4o.Internal;
using Db4objects.Db4o.Internal.Query.Processor;

namespace Db4objects.Db4o.Internal.Query.Processor
{
	/// <exclude></exclude>
	public class QConEvaluation : QCon
	{
		[System.NonSerialized]
		private object i_evaluation;

		public byte[] i_marshalledEvaluation;

		public int i_marshalledID;

		public QConEvaluation()
		{
		}

		public QConEvaluation(Transaction a_trans, object a_evaluation) : base(a_trans)
		{
			// C/S only
			i_evaluation = a_evaluation;
		}

		internal override void EvaluateEvaluationsExec(QCandidates a_candidates, bool rereadObject
			)
		{
			if (rereadObject)
			{
				a_candidates.Traverse(new _IVisitor4_31());
			}
			a_candidates.Filter(this);
		}

		private sealed class _IVisitor4_31 : IVisitor4
		{
			public _IVisitor4_31()
			{
			}

			public void Visit(object a_object)
			{
				((QCandidate)a_object).UseField(null);
			}
		}

		internal override void Marshall()
		{
			base.Marshall();
			if (!Platform4.UseNativeSerialization())
			{
				MarshallUsingDb4oFormat();
			}
			else
			{
				try
				{
					i_marshalledEvaluation = Platform4.Serialize(i_evaluation);
				}
				catch (Exception)
				{
					MarshallUsingDb4oFormat();
				}
			}
		}

		private void MarshallUsingDb4oFormat()
		{
			SerializedGraph serialized = Serializer.Marshall(Container(), i_evaluation);
			i_marshalledEvaluation = serialized._bytes;
			i_marshalledID = serialized._id;
		}

		internal override void Unmarshall(Transaction a_trans)
		{
			if (i_trans == null)
			{
				base.Unmarshall(a_trans);
				if (i_marshalledID > 0 || !Platform4.UseNativeSerialization())
				{
					i_evaluation = Serializer.Unmarshall(Container(), i_marshalledEvaluation, i_marshalledID
						);
				}
				else
				{
					i_evaluation = Platform4.Deserialize(i_marshalledEvaluation);
				}
			}
		}

		public override void Visit(object obj)
		{
			QCandidate candidate = (QCandidate)obj;
			// force activation outside the try block
			// so any activation errors bubble up
			ForceActivation(candidate);
			try
			{
				Platform4.EvaluationEvaluate(i_evaluation, candidate);
			}
			catch (Exception)
			{
				candidate.Include(false);
			}
			// TODO: implement Exception callback for the user coder
			// at least for test cases
			if (!candidate._include)
			{
				DoNotInclude(candidate.GetRoot());
			}
		}

		private void ForceActivation(QCandidate candidate)
		{
			candidate.GetObject();
		}

		internal virtual bool SupportsIndex()
		{
			return false;
		}
	}
}
