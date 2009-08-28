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
using Db4objects.Db4o.Activation;
using Db4objects.Db4o.TA;

namespace Db4objects.Db4o.Tutorial.F1.Chapter7
{
    public class SensorReadout : IActivatable
    {
        private readonly DateTime _time;
        private readonly Car _car;
        private readonly String _description;
        private SensorReadout _next;

        [Transient] 
        private IActivator _activator;

        protected SensorReadout(DateTime time, Car car, String description)
        {
            this._time = time;
            this._car = car;
            this._description = description;
            this._next = null;
        }

        public Car Car
        {
            get
            {
				Activate(ActivationPurpose.Read);
                return _car;
            }
        }

        public DateTime Time
        {
            get
            {
                Activate(ActivationPurpose.Read);
                return _time;
            }
        }

        public String Description
        {
            get
            {
				Activate(ActivationPurpose.Read);
                return _description;
            }
        }

        public SensorReadout Next
        {
            get
            {
				Activate(ActivationPurpose.Read);
                return _next;
            }
        }

        public void Append(SensorReadout readout)
        {
            Activate(ActivationPurpose.Write);
            if (_next == null)
            {
                _next = readout;
            }
            else
            {
                _next.Append(readout);
            }
        }

        public int CountElements()
        {
			Activate(ActivationPurpose.Read);
            return (_next == null ? 1 : _next.CountElements() + 1);
        }

        public override String ToString()
        {
			Activate(ActivationPurpose.Read);
            return String.Format("{0} : {1} : {2}", _car, _time, _description);
        }

        public void Activate(ActivationPurpose purpose)
        {
            if (_activator != null)
            {
                _activator.Activate(purpose);
            }
        }

        public void Bind(IActivator activator)
        {
            if (_activator == activator)
            {
                return;
            }
            if (activator != null && null != _activator)
            {
                throw new System.InvalidOperationException();
            }
            _activator = activator;
        }
    }
}