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

namespace Db4objects.Db4o.Tutorial.F1.Chapter4
{   
    public class Car
    {
        string _model;
        Pilot _pilot;
        IList _history;
        
        public Car(string model)
        {
            _model = model;
            _pilot = null;
            _history = new ArrayList();
        }
        
        public Pilot Pilot
        {
            get
            {
                return _pilot;
            }
            
            set
            {
                _pilot = value;
            }
        }
        
        public string Model
        {
            get
            {
                return _model;
            }
        }
        
        public SensorReadout[] GetHistory()
        {
            SensorReadout[] history = new SensorReadout[_history.Count];
            _history.CopyTo(history, 0);
            return history;
        }
        
        public void Snapshot()
        {
            _history.Add(new TemperatureSensorReadout(DateTime.Now, this, "oil", PollOilTemperature()));
            _history.Add(new TemperatureSensorReadout(DateTime.Now, this, "water", PollWaterTemperature()));
            _history.Add(new PressureSensorReadout(DateTime.Now, this, "oil", PollOilPressure()));
        }
        
        protected double PollOilTemperature()
        {
            return 0.1*_history.Count;
        }
        
        protected double PollWaterTemperature()
        {
            return 0.2*_history.Count;
        }
        
        protected double PollOilPressure()
        {
            return 0.3*_history.Count;
        }
        
        override public string ToString()
        {
			return string.Format("{0}[{1}]/{2}", _model, _pilot, _history.Count);
        }
    }
}
