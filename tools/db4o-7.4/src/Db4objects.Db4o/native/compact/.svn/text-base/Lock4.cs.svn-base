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
using System.Threading;

namespace Db4objects.Db4o.Foundation
{
#if CF
    public class Lock4
    {
        private volatile Thread lockedByThread;

        private volatile Thread waitReleased;
        private volatile Thread closureReleased;

        AutoResetEvent waitEvent = new AutoResetEvent(false);
        AutoResetEvent closureEvent = new AutoResetEvent(false);

        public Object Run(IClosure4 closure4)
        {
            EnterClosure();
            try
            {
                return closure4.Run();
            }
            finally
            {
                AwakeClosure();
            }
        }

        public void Snooze(long l)
        {
            AwakeClosure();
            WaitWait();
            EnterClosure();
        }

        public void Awake()
        {
            AwakeWait();
        }

        private void AwakeWait()
        {
            lock (this)
            {
                waitReleased = Thread.CurrentThread;
                waitEvent.Set();
                Thread.Sleep(0);
                if (waitReleased == Thread.CurrentThread)
                {
                    waitEvent.Reset();
                }
            }
        }

        private void AwakeClosure()
        {
            lock (this)
            {
                RemoveLock();
                closureReleased = Thread.CurrentThread;
                closureEvent.Set();
                Thread.Sleep(0);
                if (closureReleased == Thread.CurrentThread)
                {
                    closureEvent.Reset();
                }
            }
        }

        private void WaitWait()
        {
            waitEvent.WaitOne();
            waitReleased = Thread.CurrentThread;
        }

        private void WaitClosure()
        {
            closureEvent.WaitOne();
            closureReleased = Thread.CurrentThread;
        }

        private void EnterClosure()
        {
            while (lockedByThread != Thread.CurrentThread)
            {
                while (!SetLock())
                {
                    WaitClosure();
                }
            }
        }

        private bool SetLock()
        {
            lock (this)
            {
                if (lockedByThread == null)
                {
                    lockedByThread = Thread.CurrentThread;
                    return true;
                }
                return false;
            }
        }

        private void RemoveLock()
        {
            lock (this)
            {
                if (lockedByThread == Thread.CurrentThread)
                {
                    lockedByThread = null;
                }
            }
        }
    }
#endif
}