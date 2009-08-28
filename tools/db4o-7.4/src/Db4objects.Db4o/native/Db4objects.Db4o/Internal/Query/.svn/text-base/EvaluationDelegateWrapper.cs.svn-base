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
using System.Reflection;
using Db4objects.Db4o;
using Db4objects.Db4o.Query;

namespace Db4objects.Db4o.Internal.Query
{
    // TODO: Use DelegateEnvelope to build a generic delegate translator
    internal class DelegateEnvelope
    {
        System.Type _delegateType;
        object _target;
        System.Type _type;
        string _method;

        [NonSerialized]
        Delegate _content;

        public DelegateEnvelope()
        {
        }

        public DelegateEnvelope(Delegate content)
        {
            _content = content;
            Marshal();
        }

        protected Delegate GetContent()
        {
            if (null == _content)
            {
                _content = Unmarshal();
            }
            return _content;
        }

        private void Marshal()
        {
            _delegateType = _content.GetType();
#if !CF
            _target = _content.Target;
            _method = _content.Method.Name;
            _type = _content.Method.DeclaringType;
#endif
        }

        private Delegate Unmarshal()
        {
#if CF
            throw new NotSupportedException();
#else
            return (null == _target)
                       ? System.Delegate.CreateDelegate(_delegateType, _type, _method)
                       : System.Delegate.CreateDelegate(_delegateType, _target, _method);
#endif
        }
    }

    internal class EvaluationDelegateWrapper : DelegateEnvelope, IEvaluation
    {	
        public EvaluationDelegateWrapper()
        {
        }
		
        public EvaluationDelegateWrapper(EvaluationDelegate evaluation) : base(evaluation)
        {	
        }
		
        EvaluationDelegate GetEvaluationDelegate()
        {
            return (EvaluationDelegate)GetContent();
        }
		
        public void Evaluate(ICandidate candidate)
        {
            // use starting _ for PascalCase conversion purposes
            EvaluationDelegate _evaluation = GetEvaluationDelegate();
            _evaluation(candidate);
        }
    }
}