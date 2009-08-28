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

public class Car : IActivatable 
{
    private readonly String _model;
    private Pilot _pilot;
    private SensorReadout _history;
    
    [Transient]
    private IActivator _activator;

    public Car(String model) 
    {
        this._model=model;
        this._pilot=null;
        this._history=null;
    }

    public Pilot Pilot
    {
        get
        {
			Activate(ActivationPurpose.Read);
            return _pilot;
        }

        set
        {
			Activate(ActivationPurpose.Write);
            this._pilot = value;
        }
    }
    
    public Pilot PilotWithoutActivation
    {
        get { return _pilot; }
    }

    public String Model
    {
        get
        {
			Activate(ActivationPurpose.Read);
            return _model;
        }
    }
    
    public SensorReadout History
    {
        get
        {
			Activate(ActivationPurpose.Read);
            return _history;
        }
    }
    
    public void snapshot() 
    {   
        AppendToHistory(new TemperatureSensorReadout(DateTime.Now,this,"oil", PollOilTemperature()));
        AppendToHistory(new TemperatureSensorReadout(DateTime.Now, this, "water", PollWaterTemperature()));
        AppendToHistory(new PressureSensorReadout(DateTime.Now, this, "oil", PollOilPressure()));
    }

    protected double PollOilTemperature() 
    {
	    return 0.1* CountHistoryElements();
    }

    protected double PollWaterTemperature() 
    {
        return 0.2* CountHistoryElements();
    }

    protected double PollOilPressure() 
    {
        return 0.3* CountHistoryElements();
    }

    public override String ToString() 
    {
		Activate(ActivationPurpose.Read);
        return string.Format("{0}[{1}]/{2}", _model, _pilot, CountHistoryElements());
    }
    
    private int CountHistoryElements() 
    {
		Activate(ActivationPurpose.Read);
        return (_history==null ? 0 : _history.CountElements());
    }
    
    private void AppendToHistory(SensorReadout readout) 
    {
		Activate(ActivationPurpose.Write);
        if(_history==null) 
        {
            _history=readout;
        }
        else 
        {
            _history.Append(readout);
        }
    }

    public void Activate(ActivationPurpose purpose) 
    {
        if(_activator != null) 
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