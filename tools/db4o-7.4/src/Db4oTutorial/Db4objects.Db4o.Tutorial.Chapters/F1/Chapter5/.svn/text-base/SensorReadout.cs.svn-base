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

namespace Db4objects.Db4o.Tutorial.F1.Chapter5
{   
    public abstract class SensorReadout
    {
        DateTime _time;
        Car _car;
        string _description;
        SensorReadout _next;
        
        protected SensorReadout(DateTime time, Car car, string description)
        {
            _time = time;
            _car = car;
            _description = description;
            _next = null;            
        }
        
        public Car Car
        {
            get
            {
                return _car;
            }
        }
        
        public DateTime Time
        {
            get
            {
                return _time;
            }
        }
        
        public SensorReadout Next
        {
            get
            {
                return _next;
            }
        }
        
        public void Append(SensorReadout sensorReadout)
        {
            if (_next == null)
            {
                _next = sensorReadout;
            }
            else
            {
                _next.Append(sensorReadout);
            }
        }
        
        public int CountElements()
        {
            return (_next == null ? 1 : _next.CountElements() + 1);
        }
        
        override public string ToString()
        {
            return string.Format("{0} : {1} : {2}", _car, _time, _description);
        }
    }
}
